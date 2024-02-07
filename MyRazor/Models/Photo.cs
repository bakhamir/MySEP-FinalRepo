using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyRazor.Models
{
    public class Photo
    {
        public int Id { get; set; }

        //[Required(ErrorMessage ="Поле Name обязательно для заполнения")]
        //[Remote("MyCheck", "Name", ErrorMessage = "Name is not valid.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Extension обязательно для заполнения")]
        [MaxLength(10, ErrorMessage= "длина не должна превышать 10 символов")]
        [MinLength(3, ErrorMessage = "длина не должна быть меньше 3 символов")]
        public string Extension { get; set; }

        [Range(1,2000,ErrorMessage ="Недопустимый размер")]
        [Required(ErrorMessage = "Поле Width обязательно для заполнения")]
        public int Width { get; set; }

        [Required(ErrorMessage = "Поле Height обязательно для заполнения")]
        [Compare("Width", ErrorMessage = "Width и Height не совпадают")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Поле DateCreated обязательно для заполнения")]
        public DateTime DateCreated { get; set; }
    }

    public class Status
    {
        public StatusEnum status { get; set; }
        public string result { get; set; }
        public string error { get; set; }
    }

    public enum StatusEnum
    {
        OK = 1,
        ERROR = 0,
        CRITICAL_ERROR = -1
    }
}
