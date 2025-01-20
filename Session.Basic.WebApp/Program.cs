var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Security option
    options.Cookie.IsEssential = true; // Make the cookie essential
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


app.UseSession(); // Middleware for using session



app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();




app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        // Setting session data
        context.Session.SetString("UserName", "John Doe");

        await context.Response.WriteAsync("Session data has been set.");
    });

    endpoints.MapGet("/get", async context =>
    {
        // Retrieving session data
        var userName = context.Session.GetString("UserName");

        await context.Response.WriteAsync($"Session data retrieved: {userName}");
    });
});




app.Run();
