using Microsoft.AspNetCore.Mvc;
using Session.Distributed.Redis.WebApp.Models;
using System.Diagnostics;

namespace Session.Distributed.Redis.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("set-session")]
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("UserName", "John Doe");
            HttpContext.Session.SetInt32("UserAge", 30);
            return Content("Session data has been set.");
        }

        [HttpGet("get-session")]
        public IActionResult GetSession()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userAge = HttpContext.Session.GetInt32("UserAge");

            if (userName == null || userAge == null)
            {
                return Content("No session data found.");
            }

            return Content($"UserName: {userName}, UserAge: {userAge}");
        }

        [HttpGet("clear-session")]
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return Content("Session data has been cleared.");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
