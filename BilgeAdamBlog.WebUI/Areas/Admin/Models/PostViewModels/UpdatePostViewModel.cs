using BilgeAdamBlog.Common.Clients.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels
{
    public class UpdatePostViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Başlık adı boş olamaz!...")]
        [MaxLength(200, ErrorMessage = "200 karakten fazla yazamazsınız!...")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Gönderi detayı boş olamaz!...")]
        [Display(Name = "İçerik")]
        public string PostDetail { get; set; }
        [Required(ErrorMessage = "Etiket alanı boş olamaz!...")]
        [Display(Name = "Etiket")]
        public string Tags { get; set; }
        [Display(Name = "Resim")]
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }

        public Status Status { get; set; }

        public Guid CategoryId { get; set; }

        public Guid UserId { get; set; }
    }
}
