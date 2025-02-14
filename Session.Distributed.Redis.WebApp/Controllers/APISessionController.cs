using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Session.Distributed.Redis.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APISessionController : ControllerBase
    {
        [HttpPost("set")]
        public IActionResult SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
            return Ok("Session value set.");
        }

        [HttpGet("get")]
        public IActionResult GetSession(string key)
        {
            var value = HttpContext.Session.GetString(key);
            return Ok(new { Key = key, Value = value });
        }
    }
}
