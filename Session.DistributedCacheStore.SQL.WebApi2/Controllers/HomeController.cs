using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Session.DistributedCacheStore.SQL.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /*
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("UserId", 123456);
            HttpContext.Session.SetString("UserName", "info@dotnettutorials.net");
            return View();
        }
        public IActionResult Privacy()
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = sessionUserName;
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = sessionUserId;
            return View();
        }
        /*
         <div class="text-left">
            <b>User Name:</b>@ViewBag.UserName
            <br />
            <b>User Id:</b> @ViewBag.UserId
        </div>
         */
        [HttpGet("set-session")]
        public IActionResult SetSession()
        {
            HttpContext.Session.SetInt32("_UserId", 1234567);
            HttpContext.Session.SetString("_UserName", "Mr. John Doe");
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
