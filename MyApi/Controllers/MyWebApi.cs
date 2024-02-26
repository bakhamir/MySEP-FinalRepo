using Microsoft.AspNetCore.Http;   
using Microsoft.AspNetCore.Mvc; 
 
namespace MyWebApi.Controllers 
{ 
    [Route("MyWebApi")]                 
    [ApiController]  
    public class MyWebApi : ControllerBase 
    { 
        [Route("index")] 
        public ActionResult Index()       
        { 
            return Ok("hello step");
        }
        [Route("index2")]
        public ActionResult Index2(string name)
        { 
            return Ok("hello step" + name);
        }
    }
} 
