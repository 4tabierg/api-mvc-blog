using BilgeAdamBlog.Model.Context;
using BilgeAdamBlog.Service.Service.Base;
using EF = BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.Service.Service.Comment
{
    public class CommentService : BaseService<EF.Comment>, ICommentService
    {
        private readonly DataContext _context;
        public CommentService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
