var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


//c.com/c/678e4058-48d0-8005-88f3-b8aa58d41674

//To Use Distributed Sessions with Redis
//Install-package Microsoft.Extensions.Caching.StackExchangeRedis

// Add Redis distributed cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Replace with your Redis server connection string
    options.InstanceName = "MyApp_"; // Optional prefix for Redis keys
});

// Configure session to use Redis
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
