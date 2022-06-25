using BilgeAdamBlog.Common.Clients.Enums;
using BilgeAdamBlog.Common.DTOs.Comment;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.CategoryViewModels;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            //Comments = new HashSet<CommentViewModel>();
        }

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
        [Required(ErrorMessage = "Resim yolu boş olamaz!...")]
        [Display(Name = "Resim")]
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Status Status { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; }

        public Guid UserId { get; set; }
        public virtual UserViewModel User { get; set; }

       // public virtual ICollection<CommentViewModel> Comments { get; set; }
    }
}
