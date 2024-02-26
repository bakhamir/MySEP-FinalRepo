using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyWebApi : ControllerBase
    {
        [Route("index")]
        public ActionResult Index()
        {
            return Ok("hello step");

        }
        public ActionResult Index2()
        {
            return Ok("hello step");

        }
    }
}
