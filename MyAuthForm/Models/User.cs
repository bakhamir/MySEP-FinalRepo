using System.ComponentModel.DataAnnotations;

namespace MyAuthForm.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role {  get; set; }
    }
    public class Roles
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
