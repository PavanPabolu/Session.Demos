using Microsoft.AspNetCore.Mvc;
using Session.Basic.WebApp.Models;
using System.Diagnostics;

namespace Session.Basic.WebApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






        // Action to set session values
        [HttpGet("set-session")]
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("UserName", "John Doe");
            HttpContext.Session.SetInt32("UserAge", 30);

            return Content("Session data has been set.");
        }

        // Action to get session values
        [HttpGet("get-session")]
        public IActionResult GetSession()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userAge = HttpContext.Session.GetInt32("UserAge");

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
            if (HttpContext.Session.TryGetValue("UserName", out var _))
            {
                return Content("UserName session key exists.");
            }
            return Content("UserName session key does not exist.");
        }
    }
}
