FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/FastSeat.Api/FastSeat.Api.csproj", "API/FastSeat.Api/"]
COPY ["Common/FastSeat.Common.Application/FastSeat.Common.Application.csproj", "Common/FastSeat.Common.Application/"]
COPY ["Common/FastSeat.Common.Domain/FastSeat.Common.Domain.csproj", "Common/FastSeat.Common.Domain/"]
COPY ["Common/FastSeat.Common.Infrastructure/FastSeat.Common.Infrastructure.csproj", "Common/FastSeat.Common.Infrastructure/"]
COPY ["Common/FastSeat.Common.Presentation/FastSeat.Common.Presentation.csproj", "Common/FastSeat.Common.Presentation/"]
COPY ["Modules/Attendance/FastSeat.Modules.Attendance.Application/FastSeat.Modules.Attendance.Application.csproj", "Modules/Attendance/FastSeat.Modules.Attendance.Application/"]
COPY ["Modules/Attendance/FastSeat.Modules.Attendance.Domain/FastSeat.Modules.Attendance.Domain.csproj", "Modules/Attendance/FastSeat.Modules.Attendance.Domain/"]
COPY ["Modules/Attendance/FastSeat.Modules.Attendance.Infrastructure/FastSeat.Modules.Attendance.Infrastructure.csproj", "Modules/Attendance/FastSeat.Modules.Attendance.Infrastructure/"]
COPY ["Modules/Attendance/FastSeat.Modules.Attendance.IntegrationEvents/FastSeat.Modules.Attendance.IntegrationEvents.csproj", "Modules/Attendance/FastSeat.Modules.Attendance.IntegrationEvents/"]
COPY ["Modules/Attendance/FastSeat.Modules.Attendance.Presentation/FastSeat.Modules.Attendance.Presentation.csproj", "Modules/Attendance/FastSeat.Modules.Attendance.Presentation/"]
COPY ["Modules/Events/FastSeat.Modules.Events.Application/FastSeat.Modules.Events.Application.csproj", "Modules/Events/FastSeat.Modules.Events.Application/"]
COPY ["Modules/Events/FastSeat.Modules.Events.Domain/FastSeat.Modules.Events.Domain.csproj", "Modules/Events/FastSeat.Modules.Events.Domain/"]
COPY ["Modules/Events/FastSeat.Modules.Events.Infrastructure/FastSeat.Modules.Events.Infrastructure.csproj", "Modules/Events/FastSeat.Modules.Events.Infrastructure/"]
COPY ["Modules/Events/FastSeat.Modules.Events.IntegrationEvents/FastSeat.Modules.Events.IntegrationEvents.csproj", "Modules/Events/FastSeat.Modules.Events.IntegrationEvents/"]
COPY ["Modules/Events/FastSeat.Modules.Events.Presentation/FastSeat.Modules.Events.Presentation.csproj", "Modules/Events/FastSeat.Modules.Events.Presentation/"]
COPY ["Modules/Ticketing/FastSeat.Modules.Ticketing.Application/FastSeat.Modules.Ticketing.Application.csproj", "Modules/Ticketing/FastSeat.Modules.Ticketing.Application/"]
COPY ["Modules/Ticketing/FastSeat.Modules.Ticketing.Domain/FastSeat.Modules.Ticketing.Domain.csproj", "Modules/Ticketing/FastSeat.Modules.Ticketing.Domain/"]
COPY ["Modules/Ticketing/FastSeat.Modules.Ticketing.Infrastructure/FastSeat.Modules.Ticketing.Infrastructure.csproj", "Modules/Ticketing/FastSeat.Modules.Ticketing.Infrastructure/"]
COPY ["Modules/Ticketing/FastSeat.Modules.Ticketing.IntegrationEvents/FastSeat.Modules.Ticketing.IntegrationEvents.csproj", "Modules/Ticketing/FastSeat.Modules.Ticketing.IntegrationEvents/"]
COPY ["Modules/Ticketing/FastSeat.Modules.Ticketing.Presentation/FastSeat.Modules.Ticketing.Presentation.csproj", "Modules/Ticketing/FastSeat.Modules.Ticketing.Presentation/"]
COPY ["Modules/Users/FastSeat.Modules.Users.Application/FastSeat.Modules.Users.Application.csproj", "Modules/Users/FastSeat.Modules.Users.Application/"]
COPY ["Modules/Users/FastSeat.Modules.Users.Domain/FastSeat.Modules.Users.Domain.csproj", "Modules/Users/FastSeat.Modules.Users.Domain/"]
COPY ["Modules/Users/FastSeat.Modules.Users.Infrastructure/FastSeat.Modules.Users.Infrastructure.csproj", "Modules/Users/FastSeat.Modules.Users.Infrastructure/"]
COPY ["Modules/Users/FastSeat.Modules.Users.IntegrationEvents/FastSeat.Modules.Users.IntegrationEvents.csproj", "Modules/Users/FastSeat.Modules.Users.IntegrationEvents/"]
COPY ["Modules/Users/FastSeat.Modules.Users.Presentation/FastSeat.Modules.Users.Presentation.csproj", "Modules/Users/FastSeat.Modules.Users.Presentation/"]
RUN dotnet restore "./API/FastSeat.Api/FastSeat.Api.csproj"
COPY . .
WORKDIR "/src/API/FastSeat.Api"
RUN dotnet build "./FastSeat.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FastSeat.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastSeat.Api.dll"]