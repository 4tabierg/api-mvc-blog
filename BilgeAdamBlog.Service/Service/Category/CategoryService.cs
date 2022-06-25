using BilgeAdamBlog.Model.Context;
using BilgeAdamBlog.Service.Service.Base;
using EF = BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.Service.Service.Category
{
    public class CategoryService : BaseService<EF.Category>, ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context) :base(context)
        {
            _context = context;
        }
    }
}
