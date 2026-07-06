# Product Management API

A comprehensive RESTful API for managing products in an e-commerce system, built with ASP.NET Core 8.0 Web API.

## ?? Features

- ? Full CRUD operations for products
- ? Search products by category
- ? Entity Framework Core with SQL Server
- ? Code-First database migrations
- ? Dependency Injection with service layer
- ? Comprehensive logging
- ? Model validation
- ? Swagger/OpenAPI documentation
- ? Async/await throughout
- ? RESTful API design

## ??? Technology Stack

- **Framework:** ASP.NET Core 8.0
- **Database:** SQL Server (LocalDB)
- **ORM:** Entity Framework Core 8.0.11
- **Documentation:** Swagger/OpenAPI
- **Language:** C# 12

## ?? Prerequisites

- .NET 8.0 SDK or later
- SQL Server LocalDB (comes with Visual Studio)
- Visual Studio 2022 or VS Code

## ??? Project Structure

```
ProductManagement.API/
??? Controllers/
?   ??? ProductsController.cs      # API endpoints
??? Data/
?   ??? ApplicationDbContext.cs    # EF Core DbContext
??? Models/
?   ??? Product.cs                 # Product entity
??? Services/
?   ??? IProductService.cs         # Service interface
?   ??? ProductService.cs          # Service implementation
??? Migrations/                    # EF Core migrations
??? Program.cs                     # Application entry point
??? appsettings.json              # Configuration
```

## ??? Database Schema

### Products Table

| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Identity |
| Name | nvarchar(200) | Required |
| Category | nvarchar(100) | Required |
| Price | decimal(18,2) | Required, > 0 |
| Stock | int | Required, ? 0 |
| CreatedDate | datetime2 | Required |
| UpdatedDate | datetime2 | Nullable |

## ?? Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/sangya25-cg/Product_Management.API.git
cd ProductManagement
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Update Database

```bash
dotnet ef database update --project ProductManagement.API
```

### 4. Run the Application

```bash
dotnet run --project ProductManagement.API
```

### 5. Access Swagger UI

Navigate to: **https://localhost:7154/swagger**

## ?? API Endpoints

### Products

| Method | Endpoint | Description | Status Codes |
|--------|----------|-------------|--------------|
| GET | `/api/products` | Get all products | 200 |
| GET | `/api/products/{id}` | Get product by ID | 200, 404 |
| POST | `/api/products` | Create new product | 201, 400 |
| PUT | `/api/products/{id}` | Update product | 200, 400, 404 |
| DELETE | `/api/products/{id}` | Delete product | 204, 404 |
| GET | `/api/products/search/category/{category}` | Search by category | 200, 400, 404 |

## ?? Usage Examples

### Create a Product

**POST** `/api/products`

```json
{
  "name": "Laptop",
  "category": "Electronics",
  "price": 999.99,
  "stock": 50
}
```

**Response:** `201 Created`

```json
{
  "id": 1,
  "name": "Laptop",
  "category": "Electronics",
  "price": 999.99,
  "stock": 50,
  "createdDate": "2026-01-05T12:00:00Z",
  "updatedDate": null
}
```

### Get All Products

**GET** `/api/products`

**Response:** `200 OK`

```json
[
  {
    "id": 1,
    "name": "Laptop",
    "category": "Electronics",
    "price": 999.99,
    "stock": 50,
    "createdDate": "2026-01-05T12:00:00Z",
    "updatedDate": null
  }
]
```

### Update a Product

**PUT** `/api/products/1`

```json
{
  "id": 1,
  "name": "Gaming Laptop",
  "category": "Electronics",
  "price": 1299.99,
  "stock": 30
}
```

**Response:** `200 OK`

### Delete a Product

**DELETE** `/api/products/1`

**Response:** `204 No Content`

### Search by Category

**GET** `/api/products/search/category/Electronics`

**Response:** `200 OK` - Returns all products in the "Electronics" category

## ? Validation Rules

### Product Model

- **Name:** Required
- **Category:** Required
- **Price:** Must be greater than 0
- **Stock:** Must be greater than or equal to 0

Invalid data will return `400 Bad Request` with validation errors.

## ?? Logging

The application logs the following events:

- Product created (with ID and Name)
- Product updated (with ID and Name)
- Product deleted (with ID and Name)
- Product retrieval operations
- Search operations
- Warnings for not found scenarios

Logs are written to the console and can be configured in `appsettings.json`.

## ?? Configuration

### Connection String

Edit `appsettings.json` to change the database connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductManagementDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Logging Level

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## ?? Testing with Swagger

1. Run the application
2. Navigate to `https://localhost:7154/swagger`
3. Expand any endpoint
4. Click "Try it out"
5. Enter request data
6. Click "Execute"
7. View the response

## ?? NuGet Packages

- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.11)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.11)
- `Swashbuckle.AspNetCore` (6.6.2)

## ??? Architecture

The application follows these architectural patterns:

- **Repository Pattern:** Via Entity Framework Core
- **Service Layer Pattern:** Business logic separated in `ProductService`
- **Dependency Injection:** Built-in ASP.NET Core DI
- **RESTful Design:** Proper HTTP verbs and status codes
- **Async/Await:** All database operations are asynchronous

## ?? Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Swagger/OpenAPI](https://swagger.io/specification/)

## ?? Contributing

This is an assessment project. For educational purposes only.

## ?? License

This project is part of an ASP.NET Core Web API assessment.

## ????? Author

Created as part of the ASP.NET Core Web API Hands-On Assessment

---

## ?? Assessment Completion

All 8 tasks completed successfully:

- [x] Task 1: Create Project
- [x] Task 2: Product Model with Validation
- [x] Task 3: Database Configuration
- [x] Task 4: CRUD APIs
- [x] Task 5: Search API
- [x] Task 6: Dependency Injection
- [x] Task 7: Logging
- [x] Task 8: Swagger Documentation

**Status:** ? Production Ready

---

**Last Updated:** January 2026
