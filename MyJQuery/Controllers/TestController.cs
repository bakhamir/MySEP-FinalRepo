using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyJQuery.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace MyJQuery.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        List<City> lst;
        public TestController()
        {
            lst = new List<City>()
            {
                new City{Id=1, Name="Астана"},
                new City{Id=2, Name="NY"},
                new City{Id=3, Name="Москва"}
            };
        }
        [HttpGet("SayHello/{name}")]
        public ActionResult SayHello(string name)
        {
            return Ok("Hello " + name);
        }

        [HttpGet("getCityAll")]
        public ActionResult getCityAll()
        {
            using (SqlConnection db = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                List<City> lst = db.Query<City>("pGetAllCities",commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return Ok(lst);
        }
        [HttpPost("createCity")]
        public ActionResult createCity(City city)
        {
            lst.Add(city);
            return Ok("ok");
        }
    }
}
