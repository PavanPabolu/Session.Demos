var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


//c.com/c/678e4058-48d0-8005-88f3-b8aa58d41674
//p.ai/search/fix-the-error-sql-cache-the-te-wBj6pAK6QgaE8WroTIW4Hw

/*
//To use SQL Server as a distributed session store
1.Install Required Package
Install-package Microsoft.AspNetCore.Session.SqlServer
Install-package Microsoft.Extensions.Caching.SqlServer

2.Create Session Table: Run the following command to create the required table
dotnet sql-cache create "Server=localhost\\SQLEXPRESS;Database=Demo;Trusted_Connection=True;" dbo Session

*/

//3.Configure SQL Server
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=Demo;Trusted_Connection=True;";
    options.SchemaName = "dbo";
    options.TableName = "Session";
});

// Configure session to use sql
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Security setting
    options.Cookie.IsEssential = true; // Essential for GDPR compliance
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.UseAuthorization();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseSession(); // Enable session middleware
app.MapDefaultControllerRoute();



app.Run();
