# Clinic Supply Tracker

A C# web application for recording clinic supply requests and tracking whether they have been fulfilled. This student project demonstrates CRUD operations, form validation, and relational database persistence.

## Stack

- .NET 10 and ASP.NET Core Razor Pages
- Entity Framework Core 10.0.12
- SQLite
- HTML, CSS, Bootstrap, and bundled JavaScript libraries

## Features

- Create and list supply requests with item name, quantity, and department.
- Edit request details and delete requests through a confirmation page.
- Mark requests as fulfilled.
- Filter the list by pending or fulfilled status; the default view shows all requests.
- Require item name and department, and validate quantity from 1 to 1,000 on the server.
- Persist requests in a local SQLite database.

## Run locally

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) and Git. The SDK is required to build; the runtime alone is insufficient.

```sh
git clone https://github.com/arijeetT/ClinicSupplyTracker.git
cd ClinicSupplyTracker
dotnet restore
dotnet tool install --global dotnet-ef --version 10.0.12
dotnet ef database update
dotnet run --launch-profile http
```

If `dotnet-ef` is already installed, check `dotnet ef --version` and use a compatible 10.0 tool version. Open the localhost address printed in the terminal (the included HTTP profile uses `http://localhost:5157`). Stop the app with Ctrl+C.

Run commands from the project directory. The connection string in `Program.cs` is `Data Source=clinic.db`, so database commands create the local database there. The repository includes the initial migration, but excludes the local database and its data. Run `dotnet ef database update` before first use; the app does not automatically apply migrations on startup.

For the optional HTTPS launch profile, trust the ASP.NET Core development certificate with `dotnet dev-certs https --trust`, then run `dotnet run --launch-profile https`.

## Project structure

- `Program.cs`: registers Razor Pages and the SQLite-backed database context, then configures the request pipeline.
- `Data/ClinicContext.cs`: EF Core context exposing `SupplyRequests`.
- `Pages/Models/SupplyRequest.cs`: request properties and validation attributes.
- `Pages/Index.cshtml` and its page model: list, status filters, and fulfillment handler.
- `Pages/Create`, `Pages/Edit`, and `Pages/Delete`: Razor views and handlers for managing requests.
- `Migrations/`: versioned database schema.
- `wwwroot/`: styles, scripts, and bundled frontend libraries with their license files.

Page models receive `ClinicContext` through constructor injection. GET handlers load data; POST handlers validate or update data, call `SaveChanges()`, and redirect. Edit copies only the allowed request fields into the existing record. Delete requires a POST after confirmation.

## Build and manual checks

```sh
dotnet build
```

There is no automated test suite in the supplied project. To check behavior manually:

1. Create a request and confirm it appears as pending.
2. Submit missing required fields or a quantity outside 1–1,000 and confirm validation prevents saving.
3. Edit the request and verify the saved values.
4. Mark it fulfilled and check all three list filters.
5. Restart the app and confirm the request persists.
6. Open delete confirmation, cancel, then delete through confirmation and verify removal.
7. Visit an edit or delete URL with a nonexistent numeric ID and check for a 404 response.

## Scope

This is a learning project, with no login or role-based access control. Use sample supply data when demonstrating it. Hosting source on GitHub does not deploy a live application. Production hosting, access control, and automated tests would be separate work.

Local databases, build output, editor settings, local environment files, and secrets are excluded through `.gitignore`. The committed `appsettings.json` contains logging settings and the allowed-hosts setting.
