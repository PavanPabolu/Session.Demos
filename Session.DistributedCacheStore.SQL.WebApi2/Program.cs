

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Tools
//Install-Package Microsoft.EntityFrameworkCore.Tools

//Install-Package Microsoft.Extensions.Caching.SqlServer
//Install-Package Microsoft.EntityFrameworkCore.SqlServer
//Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore

//dotnet sql-cache create "Data Source=YOUR_SERVER;Initial Catalog=SessionDB;Integrated Security=True;TrustServerCertificate=True" dbo MySessions
//dotnet sql-cache create "Data Source=localhost\\SQLEXPRESS;Initial Catalog=SessionDB;Integrated Security=True;TrustServerCertificate=True" dbo MySessions



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
