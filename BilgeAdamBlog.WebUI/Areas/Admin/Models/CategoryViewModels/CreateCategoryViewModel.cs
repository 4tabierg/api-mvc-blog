using BilgeAdamBlog.Common.Clients.Enums;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Models.CategoryViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Kategori adı boş olamaz!...")]
        [MaxLength(50, ErrorMessage = "50 karakten fazla yazamazsınız!...")]
        [Display(Name = "Kategoriadı")]
        public string CategoryName { get; set; }
        [MaxLength(255, ErrorMessage = "255 karakten fazla yazamazsınız!...")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
