using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using ParentChildrenSeasons.Models;
using System.Data;

namespace ParentChildrenSeasons.Controllers
{
    public class Test : Controller
    {
        private readonly string connectionString;

        public Test(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("api/children/{parentId}")]
        public IActionResult GetChildrenByParentId(int parentId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var children = connection.Query<Children>("usp_GetChildrenByParentId", new { ParentId = parentId }, commandType: CommandType.StoredProcedure);
                if (children.Any())
                {
                    return Ok(children);
                }
                else
                {
                    return Ok("NO CHILDREN");
                }
            }
        }

        [HttpGet("api/youngestchild/{parentId}")]
        public IActionResult GetYoungestChildAndCount(int parentId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { ParentId = parentId };
                var result = connection.QueryFirstOrDefault<Children>("usp_GetYoungestChildAndCount", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
        }

        [HttpPost("api/season")]
        public IActionResult GetSeason([FromBody] List<string> months)
        {
            var monthsString = string.Join(",", months);
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Months = monthsString };
                var result = connection.QueryFirstOrDefault<string>("usp_GetSeason", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
        }
    }
}
