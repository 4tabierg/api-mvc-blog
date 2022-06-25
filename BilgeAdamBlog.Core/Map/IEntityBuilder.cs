using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBlog.Core.Map
{
    public interface IEntityBuilder
    {
        void Build(ModelBuilder builder);
    }
}
