using BilgeAdamBlog.Model.Context;
using BilgeAdamBlog.Service.Service.Base;
using EF = BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.Service.Service.User
{
    public class UserService : BaseService<EF.User>, IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
