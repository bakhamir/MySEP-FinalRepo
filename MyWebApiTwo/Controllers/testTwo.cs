using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiTwo.Model;
using System.Collections.Generic;

namespace MyWebApiTwo.Controllers
{
    [Route("stepapi/[controller]")]
    [ApiController]
    public class testTwo : ControllerBase
    {
        List<Student> lst;
        public testTwo()
        {
            {
                lst = new List<Student>()
                {
                    new Student { Id = 1, FirstName = "Artem", LastName = "Kan", DateBirth = DateTime.Now },
                    new Student { Id = 2, FirstName = "Vasya", LastName = "Pupkin", DateBirth = DateTime.Now },
                    new Student { Id = 3, FirstName = "Anton", LastName = "Smeshnyakov", DateBirth = DateTime.Now },
                };
            }

        }
        //awooo 56709
        [HttpPost("Post1")]
        public ActionResult Post1(List<Student> list)
        {
            //return NotFound("Страница n не найдена");
            //return BadRequest();
            List<string> surnames = new List<string>();
            foreach (var item in lst)
            {
                surnames.Add(item.LastName);
            }

            return Ok(surnames);
        }
        [HttpPost("Post2")]
        public ActionResult Post2([FromQuery] QueryById request)
        {
            var result = lst.Where(z => z.Id == int.Parse(request.id));
            return Ok(result);

        }

        [HttpPost("Post3")]
        public ActionResult Post3([FromBody]  string id )
        {
            var result = lst.Where(z => z.Id == int.Parse(id));
            return Ok(result);
        }


        [HttpPost("Post4")]
        public ActionResult Post4([FromBody] string req)
        {
            List<string> res = new List<string>();
            foreach (var item in lst)
            {
                string itemName = item.LastName + " " + item.FirstName;
                if (itemName.Contains(req))
                    res.Add(itemName); 
            }
            return Ok(res);
        }
        [HttpPost("Post5")]
        public ActionResult Post5()
        {
            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4
            };
            var OddNums = numbers.Where(z => z % 2 != 0);

            int sum = 0;
            foreach (int item in OddNums)
            {
                sum += item;
            }
            return Ok(sum);
        }
        [HttpPut("Put1/{id}")]
        public ActionResult Put1(string id, Student request)
        {
            lst.Add(request); 
            return Ok(lst.Where(z=>z.Id == request.Id));
        }
        [HttpPost("Post6")]
        public  ActionResult Post6([FromHeader(Name ="User-Agent")] string Agent)
        {
            return Ok(Agent);
        }
        [HttpPost("uploadFile")]
        public ActionResult uploadFile(IFormFile file)
        {
            if (file.FileName.EndsWith(".txt"))
            { 
                byte[] byteFile = System.IO.File.ReadAllBytes(file.FileName);
                return Ok(System.Text.Encoding.Default.GetString(byteFile));
            }
            else
                return BadRequest("Only .txt files are allowed ");
        } 
        [HttpPost("downloadFile")]
        public ActionResult downloadFile(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Position = 0;
            return File(ms, "application/png", "Sample.png");
        }
    } 

}
