# ProjectMgt

Simple project management backend (ASP.NET Core + EF Core) that implements a Clean Architecture style with CQRS patterns.

## Overview
This repository contains an ASP.NET Core Web API (server) and a layered solution with:
- `Domain` — core entities and value objects
- `Application` — application services, MediatR handlers, DTOs, AutoMapper profiles, FluentValidation
- `Persistence` — EF Core `DbContext` and data access
- `API` — ASP.NET Core startup and controllers (Swagger enabled)

A frontend (example origin `http://localhost:4200`) is expected to call the API.

## Tech stack
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- MediatR (CQRS)
- AutoMapper
- FluentValidation
- Swagger / OpenAPI

## Prerequisites
- .NET 9 SDK
- (Optional) `dotnet-ef` tool for migrations: `dotnet tool install --global dotnet-ef`
- A SQL Server / PostgreSQL / SQLite instance (configure in `API/appsettings.json`)

## Quick start

1. Clone the repo
   - git clone <repo-url>

2. Configure the connection string
   - Edit `API/appsettings.json` and set the `ConnectionStrings:DefaultConnection` value.

3. Apply EF Core migrations (if using EF Core migrations)
   - From the solution root:
     - `dotnet ef migrations add InitialCreate -p Persistence -s API`
     - `dotnet ef database update -p Persistence -s API`

4. Build and run the API
   - `dotnet build`
   - `cd API`
   - `dotnet run`
   - API will be available at `https://localhost:5001` (or as shown in console)
   - Swagger UI: `https://localhost:5001/swagger`

5. Run tests (if tests exist)
   - `dotnet test`

## Project layout (high level)
- `API/` — Web host, controllers, middleware
- `Application/`
  - `Common/Interfaces` — `IApplicationDbContext`, `ICurrentUserService`, etc.
  - `Common/Models` — DTOs and shared models
  - `Common/Mappings` — AutoMapper profiles
  - `Features/` — grouped commands/queries (CQRS)
  - `DependencyInjection.cs` — registers application services
- `Persistence/` — `PASDbContext`, migrations, EF configuration
- `Domain/` — domain entities, enums, base entity types

## Developer notes
- Application layer uses MediatR, AutoMapper and FluentValidation. Keep validators colocated with commands/queries for cohesion.
- `IApplicationDbContext` should avoid referencing EF Core types if you want strict separation (prefer `IQueryable<T>` instead of `DbSet<T>`).
- `API/Program.cs` enables CORS for `http://localhost:4200` and Swagger for local testing.

## Contributing
- Follow the existing folder conventions (features grouped by domain area).
- Add unit tests for new handlers and validators.
- Open a PR and add a clear description of changes.

## License
- See `LICENSE` (add a license file if one is not present).

If you want, I can generate a checklist for onboarding, CI pipeline sample, or a simple `docker-compose` file for running the database locally.
