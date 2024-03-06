using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonFromclass.Models
{
     public class city
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  countryId { get; set;}

     }
    public class country
    {
        public int id { get; set; } 
        public string name { get; set; }
        public List<city> city { get; set; }
    }
}
