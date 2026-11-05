# Lumo.Api

A production-style ASP.NET Core Web API for an e-commerce backend, built ticket by ticket the way a real team ships features.

## Features

- List customers with **pagination** and an optional **city filter**
- Get a single customer by id (`404` when not found)
- Create a customer (`201 Created` with a `Location` header)
- **Input validation** with field-level `400` errors
- **Centralized error handling** with standard `ProblemDetails` responses
- **Swagger / OpenAPI** documentation for every status code

## Tech stack

- .NET 10, ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server (LocalDB for development)
- Swagger (Swashbuckle)

## Architecture

```
HTTP request
   │
   ▼
CustomersController   → HTTP only: binds input, picks the status code
   │
   ▼
ICustomerService      → business logic, maps entities to DTOs
   │
   ▼
LumoDbContext (EF Core) → SQL Server
```

Unhandled exceptions bubble up to a single `GlobalExceptionHandler` registered in the middleware pipeline. Controllers contain no `try/catch`.

## Engineering decisions

- **Separate request and response DTOs.** Clients can only send the fields they are allowed to set (preventing over-posting), and internal entity fields are never exposed in responses.
- **Centralized exception handling.** Stack traces, file paths and cookies never leak to the client; errors are logged in one place with the request method and path.
- **Deterministic pagination.** Results are ordered by `Id` before `Skip`/`Take`, so a customer can never appear on two pages or be skipped entirely.
- **Scoped services.** `DbContext` is scoped and not thread-safe; a service that outlived it (e.g. a singleton) would share one context across all requests and cause concurrency errors (captive dependency).

## Getting started

**Prerequisites:** .NET 10 SDK, SQL Server LocalDB (installed with Visual Studio)

1. Clone the repository
   ```
   git clone https://github.com/Teocelik/Lumo.Api.git
   ```
2. Create a database named `TrainingDb` on `(localdb)\mssqllocaldb`
3. Run `database/schema.sql` against it to create the tables and sample data
4. Check the connection string in `appsettings.Development.json`
5. Run the API
   ```
   dotnet run
   ```
6. Open `/swagger` in your browser

## API endpoints

| Method | Endpoint | Description | Responses |
|---|---|---|---|
| GET | `/api/customers?page=1&pageSize=20&city=Antalya` | Paged list, optional city filter | 200 |
| GET | `/api/customers/{id}` | Single customer | 200, 404 |
| POST | `/api/customers` | Create a customer | 201, 400 |
