using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace MyBaseAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class Test : ControllerBase
    {
        [Authorize]
        [HttpGet("SayHello")]
        public ActionResult SayHello(string name)
        {
            return Ok($"Hello {name} {User.Identity.Name} {User.FindFirstValue("psw")}");
            //yopta debug 
        }
        [Authorize]
        [HttpGet("Download")]
        public ActionResult Download(string name)
        {
            string fileContent = $"Hello {name} {User.Identity.Name} {User.FindFirstValue("psw")}";
             
            byte[] byteArray = Encoding.UTF8.GetBytes(fileContent);
             
            string type = "text/plain";
             
            string fileName = "Task.txt";
            Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
            return File(byteArray, type, fileName);

        }
    }
}
