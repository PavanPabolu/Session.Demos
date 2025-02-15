using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();




builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => //Implicit reference "Microsoft.AspNetCore.Session" //
{
    options.Cookie.Name = ".MyExample.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(310);//Session timeout, Sets the duration for which the session can remain idle
    options.IOTimeout = TimeSpan.FromSeconds(10);   //Sets the maximum time allowed for the session middleware for input/output operations
    options.Cookie.HttpOnly = true;     //Security option, Determines if the cookie is accessible only through HTTP (not via client-side scripts like JavaScript)
    options.Cookie.IsEssential = true;  //Make the cookie essential for the application to function correctly
    options.Cookie.Path = "/";        //By setting the path to /, the cookie is available on all pages of the application.
    //options.Cookie.Path = "/Home";      //To restrict it to a specific part of the application (limits the cookie to the specified path).
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;//(cookie sent using the same protocol as the request).
});





var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


//It should and must be configured after UseRouting and before MapControllerRoute
app.UseSession();



app.MapControllers();
app.Run();
