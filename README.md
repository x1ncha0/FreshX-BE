# FreshX Backend API

Backend service for the **FreshX system**, built with **ASP.NET Core
(.NET 8)**.\
This API provides authentication, user management, and business logic
for the FreshX platform.

------------------------------------------------------------------------

## Tech Stack

-   Framework: ASP.NET Core (.NET 8)
-   Language: C#
-   Database: SQL Server
-   ORM: Entity Framework Core
-   Authentication: JWT (JSON Web Token)
-   Mapping: AutoMapper
-   Environment Variables: DotNetEnv

------------------------------------------------------------------------

## Project Structure

    FreshX-BE
    │
    ├── FreshX.API              # API Layer (Controllers, Middleware)
    ├── FreshX.Application      # Business Logic Layer
    ├── FreshX.Domain           # Entities & Domain Models
    ├── FreshX.Infrastructure   # Database, Repositories, External Services
    │
    └── FreshX.sln              # Solution File

------------------------------------------------------------------------

## Prerequisites

Before running the project, make sure you have installed:

-   .NET 8 SDK
-   Visual Studio 2022
-   SQL Server

------------------------------------------------------------------------

## Setup & Run

> **Note:** This project uses **.NET 8.x**

### 1. Clone the repository

``` bash
git clone https://github.com/your-repo/freshx-be.git
```

### 2. Configure Environment Variables

Download the `.env` file and place it in the **same directory as the
`Program.cs` file**.

Example:

    FreshX.API
    │
    ├── Program.cs
    ├── .env

------------------------------------------------------------------------

### 3. Open Solution

Open the `.sln` file using **Visual Studio 2022**.

------------------------------------------------------------------------

### 4. Restore NuGet Packages

If there are missing packages, open **Package Manager Console** and run:

``` bash
dotnet restore
```

------------------------------------------------------------------------

### 5. Run the Application

Start the project from **Visual Studio** or run:

``` bash
dotnet run
```

------------------------------------------------------------------------

## Environment Variables Example

Example `.env` file:

    DB_CONNECTION=your_connection_string
    JWT_SECRET=your_secret_key
    JWT_ISSUER=FreshX
    JWT_AUDIENCE=FreshXUsers

------------------------------------------------------------------------

## API Documentation

If Swagger is enabled, access it at:

    https://localhost:xxxx/swagger

------------------------------------------------------------------------

## Author

Developed by **FreshX**
