using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Session.InMemory.WebApi.Controllers
{
    enum Session { }
    
    public static class MySession
    {
        public const string UserId = "_UserId";
        public const string UserName = "_UserName";
        public const string UserAge = "_UserAge";
    }


    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        //const string SessionUserId = "_UserId";
        //const string SessionUserName = "_UserName";


        // Action to set session values
        [HttpGet("set-session")]
        public IActionResult SetSession()
        {
            HttpContext.Session.SetInt32("_UserId", 1234567);
            HttpContext.Session.SetString("_UserName", "John Doe");
            HttpContext.Session.SetInt32("_UserAge", 30);

            return Content("Session data has been set.");
        }

        // Action to get session values
        [HttpGet("get-session")]
        public IActionResult GetSession()
        {
            var userId = HttpContext.Session.GetInt32(MySession.UserId);
            var userName = HttpContext.Session.GetString(MySession.UserName);
            var userAge = HttpContext.Session.GetInt32(MySession.UserAge);

            if (userName == null || userAge == null)
            {
                return Content("Session data is not available.");
            }

            return Content($"UserName: {userName}, UserAge: {userAge}");
        }

        // Action to clear session values
        [HttpGet("clear-session")]
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();

            return Content("Session data has been cleared.");
        }

        // Example of checking if a session key exists
        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            if (HttpContext.Session.TryGetValue(MySession.UserName, out var _))
            {
                return Content("UserName session key exists.");
            }
            return Content("UserName session key does not exist.");
        }
    }
}

/*
 @using Microsoft.AspNetCore.Http;
@inject IHttpContextAccessor HttpContextAccessor
@{
    ViewData["Title"] = "Home Page";
}
<div class="text-left">
    <b>User Name:</b> @HttpContextAccessor?.HttpContext?.Session.GetString("_UserName")
    <br />
    <b>User Id:</b> @HttpContextAccessor?.HttpContext?.Session.GetInt32("_UserId")
</div>
//....

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

 */
