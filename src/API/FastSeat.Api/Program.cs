using System.Reflection;
using FastSeat.Api.Extensions;
using FastSeat.Api.Middleware;
using FastSeat.Api.OpenTelemetry;
using FastSeat.Common.Application;
using FastSeat.Common.Infrastructure;
using FastSeat.Common.Infrastructure.Configuration;
using FastSeat.Common.Presentation.Endpoints;
using FastSeat.Modules.Attendance.Infrastructure;
using FastSeat.Modules.Events.Infrastructure;
using FastSeat.Modules.Ticketing.Infrastructure;
using FastSeat.Modules.Users.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

Assembly[] moduleApplicationAssemblies = [
    FastSeat.Modules.Users.Application.AssemblyReference.Assembly,
    FastSeat.Modules.Events.Application.AssemblyReference.Assembly,
    FastSeat.Modules.Ticketing.Application.AssemblyReference.Assembly,
    FastSeat.Modules.Attendance.Application.AssemblyReference.Assembly];

builder.Services.AddApplication(moduleApplicationAssemblies);

string databaseConnectionString = builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnectionString = builder.Configuration.GetConnectionStringOrThrow("Cache");

builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [
        EventsModule.ConfigureConsumers(redisConnectionString),
        TicketingModule.ConfigureConsumers,
        AttendanceModule.ConfigureConsumers
    ],
    databaseConnectionString,
    redisConnectionString);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddKeyCloak(keyCloakHealthUrl);

builder.Configuration.AddModuleConfiguration(["users", "events", "ticketing", "attendance"]);

builder.Services.AddEventsModule(builder.Configuration);

builder.Services.AddUsersModule(builder.Configuration);

builder.Services.AddTicketingModule(builder.Configuration);

builder.Services.AddAttendanceModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContext();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();

public partial class Program;
