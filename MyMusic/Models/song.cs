namespace MyMusic.Models
{
    public class song
    {
        public int id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int categoryid { get; set; }
        public string duration { get; set; }
        public int typeid { get; set; }

      
    }
    public class type
    {
        public int id { get; set; }
        public string extension { get; set; }
 
    }
    public class category
    {
        public int id { get; set; }
        public string genre { get; set; }
 
    }
    public class user
    {
        public int id { get; set; }
        public string login { get; set; }
        public int role_id { get; set; }
        public byte[] password { get; set; }

   
    }
    public class roles
    {
        public int id { get; set; }
        public string name { get; set; }

 
    }
}
