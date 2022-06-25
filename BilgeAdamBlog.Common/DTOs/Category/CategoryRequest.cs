using BilgeAdamBlog.Common.DTOs.Base;

namespace BilgeAdamBlog.Common.DTOs.Category
{
    public class CategoryRequest : BaseDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
