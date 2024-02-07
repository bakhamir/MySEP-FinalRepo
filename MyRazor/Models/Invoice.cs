using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyRazor.Models
{
    public class Invoice
    {
        [Required(ErrorMessage ="Name is required")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Sum is required")]
        [Range(1000,100000,ErrorMessage ="Sum is either too small or too big")]
        public string Sum { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [Remote("DateCheck","Date",ErrorMessage ="Date is too far from today")]
        public DateTime  Date { get; set; }
        [MaxLength(20,ErrorMessage ="email no longer than 20 symbols")]
        [EmailAddress]
        public string Sender { get; set; }
        [EmailAddress]
        [MaxLength(20, ErrorMessage = "email no longer than 20 symbols")]
        [Compare("Sender",ErrorMessage ="Emails dont match")]
        public string SenderConfirm { get; set; }
        [EmailAddress]
        [MaxLength(20, ErrorMessage = "email no longer than 20 symbols")]
        public string Receiver { get; set; }
    }
}
