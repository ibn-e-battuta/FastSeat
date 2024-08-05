# FastSeat

FastSeat is a robust, modular event ticketing system built using .NET 8 and following clean architecture principles. It provides a comprehensive solution for managing events, ticket sales, and attendee check-ins.

## Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
3. [Modules](#modules)
4. [Technologies Used](#technologies-used)
5. [Getting Started](#getting-started)
6. [Configuration](#configuration)
7. [API Documentation](#api-documentation)
8. [Testing](#testing)
9. [Deployment](#deployment)
10. [Contributing](#contributing)
11. [License](#license)

## Project Overview

FastSeat is designed as a modular monolith, allowing for easy maintenance and potential future separation into microservices. It provides features such as:

- Event creation and management
- Ticket type configuration
- Ticket sales and order management
- Attendee check-in
- User registration and profile management

## Architecture

The project follows clean architecture principles, with each module structured into the following layers:

- **Domain**: Contains the core business logic and entities
- **Application**: Implements use cases and orchestrates the domain logic
- **Infrastructure**: Handles external concerns such as databases and third-party services
- **Presentation**: Exposes the API endpoints

The system also implements patterns such as:

- Command Query Responsibility Segregation (CQRS)
- Domain-Driven Design (DDD)
- Outbox pattern for reliable event publishing
- Idempotent event handling

## Modules

FastSeat consists of the following modules:

1. **Attendance**: Manages attendee check-ins and event statistics
2. **Events**: Handles event creation, management, and categorization
3. **Ticketing**: Manages ticket types, sales, and orders
4. **Users**: Handles user registration, authentication, and profile management

## Technologies Used

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Redis
- MassTransit
- Keycloak
- Docker
- Serilog
- Swagger / OpenAPI
- OpenTelemetry
- Quartz.NET

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker and Docker Compose
- PostgreSQL
- Redis

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/FastSeat.git
   ```

2. Navigate to the project directory:

   ```bash
   cd FastSeat
   ```

3. Run the Docker Compose file to start the required services:

   ```docker
   docker-compose up -d
   ```

4. Apply database migrations:

   ```dotnetcli
   dotnet ef database update --project src/API/FastSeat.Api
   ```

5. Run the application:

   ```dotnetcli
   dotnet run --project src/API/FastSeat.Api
   ```

The application should now be running on `http://localhost:5000`.

## Configuration

Configuration files are located in the `src/API/FastSeat.Api` directory:

- `appsettings.json`: Base configuration file
- `appsettings.Development.json`: Development-specific settings
- `modules.*.json`: Module-specific configurations

Sensitive information should be stored in user secrets or environment variables.

## API Documentation

API documentation is available via Swagger UI. Once the application is running, navigate to:

```
http://localhost:5000/swagger
```

## Testing

The project includes unit tests, integration tests, and architecture tests. To run the tests:

```
dotnet test
```

## Deployment

The project includes Docker support for easy deployment. To build and run the Docker image:

1. Build the Docker image:

   ```
   docker build -t fastseat .
   ```

2. Run the container:

   ```
   docker run -p 8080:80 fastseat
   ```

For production deployment, consider using container orchestration tools like Kubernetes.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

---

This README provides a comprehensive overview of your FastSeat project, including its architecture, features, and setup instructions. You may want to adjust some details based on your specific implementation and preferences.
