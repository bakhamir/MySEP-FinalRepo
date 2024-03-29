﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiTwo.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace MyWebApiTwo.Controllers
{
    [Route("MyWebApi")]
    [ApiController]
    public class MyWebApi : ControllerBase
    {
        List<Student> list;
        public MyWebApi()
        {
            {
                list = new List<Student>()
                {
                    new Student { Id = 1, FirstName = "Artem", LastName = "Kan", DateBirth = DateTime.Now },
                    new Student { Id = 2, FirstName = "Vasya", LastName = "Pupkin", DateBirth = DateTime.Now },
                    new Student { Id = 3, FirstName = "Anton", LastName = "Smeshnyakov", DateBirth = DateTime.Now },
                };
            }

        }

        [HttpGet, Route("index")]
        public ActionResult Index()
        {
            return Ok("hello step");
        }
        [HttpGet, Route("index2")]
        public ActionResult Index2(string name)
        {
            return Ok("hello step" + name);
        }
        [HttpGet, Route("getStudentById/{id}")]
        public ActionResult getStudentById(int id)
        {
            return Ok(list.Where(z => z.Id == id).FirstOrDefault());
        }


        [HttpGet, Route("getStudentByIdFull/{id}")]
        public ActionResult getStudentByIdFull(int id)
        {
            var result = list.Where(z => z.Id == id).FirstOrDefault();

            getStudentByIdFullResponse response = new getStudentByIdFullResponse
            {
                Status = new ResponseResult
                {
                    Result = result == null ? "no data" : "ok",
                    Status = result != null ? StatusEnum.OK : StatusEnum.ERROR
                },
                Student = result
            };
            return Ok(response);
        }

        [HttpPost,Route("GetStudentViaPost/{id}")]
        public ActionResult GetStudentViaPost(GetStudentViaPostRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Id.ToString())) 
                return Ok(list);
            return Ok(list.Where(z => z.Id == request.Id).FirstOrDefault());
        }
        [HttpGet, Route("GetAllStudents")]
        public IEnumerable<Student> GetAllStudents()
        {
            using (SqlConnection db = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                return db.Query<Student>("pGetAllStudents", commandType: CommandType.StoredProcedure);
            }
        }
        [HttpPost, Route("DeleteStudent/{id}")]
        public int DeleteStudent(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (SqlConnection db = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                return db.Execute("DeleteStudentProcedure", parameters, commandType: CommandType.StoredProcedure);
            }
        }
 


    }
}
