using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-Posta adresi gereklidir.")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola gereklidir.")]
        [Display(Name = "Parola")]
        public string Password { get; set; }
    }
}
