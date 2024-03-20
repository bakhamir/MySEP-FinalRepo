using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicCRUD.Models;
using System.Data.SqlClient;
using Dapper;
namespace MyMusicCRUD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IConfiguration config;
        public TestController(IConfiguration config) 
        {
            this.config = config;
        }

        [HttpGet("GetSongs")]
        public IActionResult GetAllSongs()
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                var res = db.Query<Music>("GetAllSongs", commandType: System.Data.CommandType.StoredProcedure).ToList();
                return Ok(res);
            }
        }
        [HttpPost("AddSong")]
        public IActionResult AddSong(Music song)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                var res = db.Query<Music>("GetAllSongs", commandType: System.Data.CommandType.StoredProcedure).ToList();
                return Ok(res);
            }
        }
    }
}
