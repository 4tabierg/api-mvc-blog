using BilgeAdamBlog.Common.Clients.Enums;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Posts = new HashSet<PostViewModel>();
        }
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Kategori adı boş olamaz!...")]
        [MaxLength(50,ErrorMessage = "50 karakten fazla yazamazsınız!...")]
        [Display(Name = "Kategoriadı")]
        public string CategoryName { get; set; }
        [MaxLength(255, ErrorMessage = "255 karakten fazla yazamazsınız!...")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<PostViewModel> Posts { get; set; }
    }
}
