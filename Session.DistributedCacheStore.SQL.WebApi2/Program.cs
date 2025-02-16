
using Microsoft.EntityFrameworkCore;
using Session.DistributedCacheStore.SQL.WebApi2.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//--------------------------------------------------------------------------------------
//1. Install Packages
//1.a. Install-Package Microsoft.EntityFrameworkCore
//1.b. Install-Package Microsoft.EntityFrameworkCore.SqlServer
//1.c. Install-Package Microsoft.Extensions.Caching.SqlServer
////Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore

//2. Configure SQL Server for Session Storage:
//2.1. Create a database in the SQL Server database with the SessionDB. "CREATE DATABASE SessionDB;"
//2.2. To work with distributed SQL server cache,
//2.2.1. dotnet tool install --global dotnet-sql-cache //OR
//2.2.1. dotnet tool install --local dotnet-sql-cache --version 8.0.2
//2.2.1. dotnet tool install --global dotnet-sql-cache --version 8.0.2
//3. The SQL-cache create tool can be used to create the Session table in the SQL server database.
//3.1. dotnet sql-cache create "Data Source=YOUR_SERVER;Initial Catalog=SessionDB;Integrated Security=True;TrustServerCertificate=True" dbo MySessions
//3.1. dotnet sql-cache create "Data Source=SrinishaAmmu\SQLEXPRESS;Initial Catalog=SessionDB;Integrated Security=True;TrustServerCertificate=True" dbo MySessions
//--------------------------------------------------------------------------------------


// Add and configure the Entity Framework Core DbContext service
builder.Services.AddDbContext<EFCoreDbContext>(options =>
    // Configure the DbContext to use SQL Server with the connection string from configuration
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionStr_ForSession"))
);

//Add and configure the Distributed InMemory Storage service for session storage
//builder.Services.AddDistributedMemoryCache(); //This method provides the necessary in-memory storage backend for storing session data. It configures IMemoryCache, allowing us to use memory caching to store session data.

// Add and configure the Distributed SQL Server Cache service for session storage
builder.Services.AddDistributedSqlServerCache(options =>
{
    // Set the connection string for connecting to the SQL Server where session data will be stored
    options.ConnectionString = builder.Configuration.GetConnectionString("DBConnectionStr_ForSession");
    // Set the database schema where the session table will be located
    options.SchemaName = "dbo";
    // Set the table name where session data will be stored
    options.TableName = "MySessions";
});
// Add and configure the Session service
builder.Services.AddSession(options =>
{
    // Set the idle timeout for the session; the session will expire if inactive for this duration
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    // Set the HttpOnly property to true to prevent client-side scripts from accessing the session cookie
    options.Cookie.HttpOnly = true;
    // Mark the session cookie as essential, which means it will be included even if the user has not given consent for non-essential cookies
    options.Cookie.IsEssential = true;
});

//--------------------------------------------------------------------------------------





var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();


//Configuring Session Middleware in ASP.NET Core
//It should and must be configured after UseRouting and before MapControllerRoute
app.UseSession();



app.MapControllers();

app.Run();
