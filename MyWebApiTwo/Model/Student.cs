﻿namespace MyWebApiTwo.Model
{
    public class Student
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
    }
    public class ResponseResult
    { 
        public StatusEnum Status { get; set; }
        public string Result { get; set; }
        public DateTime Error { get; set; }
    }
    public enum StatusEnum { OK = 1, ERROR = 0,  CRITICAL = -1 }
    public class getStudentByIdFullResponse
    {
       public ResponseResult Status { get; set; }
        public Student Student { get; set; }
    }
    public class GetStudentViaPostRequest
    {
        public int Id { get; set; }
    }


    public class QueryById
    {
        public string id { get; set; }
    }
}         