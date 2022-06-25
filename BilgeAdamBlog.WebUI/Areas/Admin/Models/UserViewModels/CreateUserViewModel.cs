using BilgeAdamBlog.Common.Clients.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Models.UserViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "İsim alanı boş olamaz!...")]
        [MaxLength(50, ErrorMessage = "50 karakten fazla yazamazsınız!...")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş olamaz!...")]
        [MaxLength(150, ErrorMessage = "150 karakten fazla yazamazsınız!...")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Başlık alanı boş olamaz!...")]
        [MaxLength(50, ErrorMessage = "50 karakten fazla yazamazsınız!...")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [MaxLength(250, ErrorMessage = "250 karakten fazla yazamazsınız!...")]
        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "E-Posta alanı boş olamaz!...")]
        [MaxLength(150, ErrorMessage = "150 karakten fazla yazamazsınız!...")]
        [EmailAddress(ErrorMessage = "E-Posta formatında giriş yapınız...")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola alanı boş olamaz!...")]
        [MaxLength(12, ErrorMessage = "12 karakten fazla yazamazsınız!...")]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        public Status Status { get; set; }
    }
}
