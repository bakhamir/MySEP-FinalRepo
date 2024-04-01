using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using MyMusicCRUD.Models;

namespace MyMusicCRUD.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TestController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                var res = db.Query<Category>("GetAllCategories", commandType: System.Data.CommandType.StoredProcedure).AsList();
                return Ok(res);
            }
        }

        [HttpGet("GetSongs")]
        public IActionResult GetAllSongs()
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                var res = db.Query<Music>("GetAllSongs", commandType: System.Data.CommandType.StoredProcedure).AsList();
                return Ok(res);
            }
        }

        [HttpPost("AddSong")]
        public IActionResult AddSong(Music song)
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@title", song.title);
                p.Add("@catId", song.catId);
                p.Add("@duration", song.duration);
                var res = db.Query<Music>("AddSong", p, commandType: System.Data.CommandType.StoredProcedure).AsList();
                return Ok(res);
            }
        }

        [HttpPost("EditSong")]
        public IActionResult EditSong(Music song)
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@id", song.id);
                p.Add("@title", song.title);
                p.Add("@catId", song.catId);
                p.Add("@duration", song.duration);
                var res = db.Execute("EditSong", p, commandType: System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
        }

        [HttpPost("DelSong")]
        public IActionResult DeleteSong(int id)
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@id", id);
                var res = db.Execute("DelSong", p, commandType: System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
        }

    }
}
