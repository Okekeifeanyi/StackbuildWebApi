StackBuild API
Transactional e-commerce backend API built with .NET 8 and PostgreSQL
Repository: https://github.com/Okekeifeanyi/StackbuildWebApi.git
1. Setup Instructions
1. Clone the repository
git clone https://github.com/Okekeifeanyi/StackbuildWebApi.git
cd StackbuildWebApi
2. Configure database connection
Update appsettings.json:
{
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=stackbuilddb;Username=postgres;Password=yourpassword"
    }
}
3. Install NuGet packages
Ensure all packages are .NET 8 compatible. Use Visual Studio Manage NuGet Packages or Package Manager Console:
Install-Package Microsoft.EntityFrameworkCore -Version 8.*
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 8.*
Install-Package Microsoft.AspNetCore.Mvc.Core -Version 8.*
4. Run EF Core migrations
Add-Migration "Initial" -Project StackBuildApi.Data -StartupProject StackBuildApi
Update-Database -Project StackBuildApi.Data -StartupProject StackBuildApi
5. Run the API
dotnet run --project StackBuildApi
6. Test the API
- Swagger UI: https://localhost:7084/swagger
- Orders endpoint: POST /api/Orders
- Products endpoint: GET/POST/PUT/DELETE /api/Products
2. Assumptions
- Orders are atomic — failure in any item rolls back the entire order.
- Stock is concurrency-safe using row-level locks (FOR UPDATE) in PostgreSQL and EF Core [Timestamp] RowVersion.
- Negative stock is not allowed.
- Orders cannot include non-existent products.
- All API responses follow the standardized ApiResponse structure.
3. Tech Stack Choices
- .NET 8 / C# — modern, high-performance backend
- ASP.NET Core Web API — RESTful service
- Entity Framework Core — ORM with PostgreSQL support
- PostgreSQL — reliable relational database with transactional row-level locking
- NuGet packages — all updated to .NET 8
- DTOs + ApiResponse — consistent response structure for all endpoints
4. Important Notes
- The Product entity has a [Timestamp] RowVersion column to prevent overselling in concurrent requests.
- All stock updates and order creations are wrapped in transactions to ensure data integrity.
- Concurrency violations return HTTP 409 Conflict with descriptive messages.
- Repository methods are fully asynchronous for scalability.
- The API design allows easy extension for features like order history, stock reservation, or multi-warehouse support.
