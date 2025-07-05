# I Miss My Friends Backend

Backend for the small web app that lets you easily see how long since you've hang out with your loved ones.

## Stack

C# server using Minimal API, Entity Framework and SQLite.
Versions

- NET 9.0
- EF SQLite 9.0
- EF Core Design 9.0

## Installation

To be able to run this server, we need to install 3 packages in addition to .NET 9.0
The Database provider for SQLite, EF Core Tools and EF Core Design.
Run the following three commands to install them

```
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0
    dotnet tool install --global dotnet-ef
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0
```

## How to run

Before these steps are run, be sure you've installed Entity Framework tools

1. Add the following to appsettings.Development.json/appsettings.json

```csharp
    "ConnectionStrings": {
        "Immf": "Data Source=TheNameYouWantTheDatabaseToBe.db"
    }
```

2. Run the following command, to create SQLite database

```powershell
    dotnet ef database update
```

3. Run the services with the following command

```powershell
    dotnet run Program.cs
```
