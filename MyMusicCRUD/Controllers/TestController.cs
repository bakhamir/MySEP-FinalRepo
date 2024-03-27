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
                DynamicParameters p = new DynamicParameters();
                p.Add("@title", song.title);
                p.Add("@catId", song.catId);
                p.Add("@duration", song.duration);
                var res = db.Query<Music>("AddSong",p, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return Ok(res);
            }
        }
        [HttpPost("EditSong")]
        public IActionResult EditSong(Music song)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@title", song.title);
                p.Add("@catId", song.catId);
                p.Add("@duration", song.duration);
                var res = db.Query<Music>("EdSong", p, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return Ok(res);
            }
        }
        [HttpPost("DelSong")]
        public IActionResult DeleteSong(int id)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@id", id);
                var res = db.Execute("DelSong", p, commandType: System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
        }
    }
}
