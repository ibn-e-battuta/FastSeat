using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Infrastructure.Outbox;
using FastSeat.Common.Presentation.Endpoints;
using FastSeat.Modules.Events.IntegrationEvents;
using FastSeat.Modules.Ticketing.Application.Abstractions.Authentication;
using FastSeat.Modules.Ticketing.Application.Abstractions.Data;
using FastSeat.Modules.Ticketing.Application.Abstractions.Payments;
using FastSeat.Modules.Ticketing.Application.Carts;
using FastSeat.Modules.Ticketing.Domain.Customers;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.Domain.Orders;
using FastSeat.Modules.Ticketing.Domain.Payments;
using FastSeat.Modules.Ticketing.Domain.Tickets;
using FastSeat.Modules.Ticketing.Infrastructure.Authentication;
using FastSeat.Modules.Ticketing.Infrastructure.Customers;
using FastSeat.Modules.Ticketing.Infrastructure.Database;
using FastSeat.Modules.Ticketing.Infrastructure.Events;
using FastSeat.Modules.Ticketing.Infrastructure.Inbox;
using FastSeat.Modules.Ticketing.Infrastructure.Orders;
using FastSeat.Modules.Ticketing.Infrastructure.Outbox;
using FastSeat.Modules.Ticketing.Infrastructure.Payments;
using FastSeat.Modules.Ticketing.Infrastructure.Tickets;
using FastSeat.Modules.Users.IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FastSeat.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddTicketingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserProfileUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<EventPublishedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<TicketTypePriceChangedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<EventCancellationStartedIntegrationEvent>>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketingDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

        services.AddSingleton<CartService>();
        services.AddSingleton<IPaymentService, PaymentService>();

        services.AddScoped<ICustomerContext, CustomerContext>();

        services.Configure<OutboxOptions>(configuration.GetSection("Ticketing:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Ticketing:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
