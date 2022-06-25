using BilgeAdamBlog.Model.Context;
using BilgeAdamBlog.Service.Service.Base;
using EF = BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.Service.Service.Post
{
    public class PostService : BaseService<EF.Post>, IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
