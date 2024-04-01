using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using jsonFromclass.Models;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace jsonFromclass
{
    internal class Program
    {
        static string constr = @"Server=206-4\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";
        static void Main(string[] args)
        {

            var client = new RestClient();    
            var request = new RestRequest("http://localhost:5051/test/GetAllCategories", Method.Get);
            RestResponse response =  client.Execute(request);
            Console.WriteLine(response.Content);
        }
        static void test1(string id)
        {
            using (SqlConnection db = new SqlConnection(constr))
            {
                string sql1 = $"select id , name from country where id = {id}";
                sql1 += $"select id , name , countryid from city where  countryid = {id}";
                var multi = db.QueryMultiple(sql1);
                country country = multi.Read<country>().FirstOrDefault();
                var citi = multi.Read<city>().ToList();
                //country city = new city();
                country.city = new List<city>();
                foreach (var item in citi)
                {
                   country.city.Add(item);
                }
            }
        }
 
    }
}
