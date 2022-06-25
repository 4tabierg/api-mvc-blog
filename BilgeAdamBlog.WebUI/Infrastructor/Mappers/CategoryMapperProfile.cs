using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Category;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.CategoryViewModels;
using BilgeAdamBlog.WebUI.Infrastructor.Extensions;

namespace BilgeAdamBlog.WebUI.Infrastructor.Mappers
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryViewModel, CategoryRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CategoryViewModel, CategoryResponse>()
               .ReverseMap()
               .IgnoreAllNonExisting()
               .ForAllMembers(options =>
               options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CreateCategoryViewModel, CategoryRequest>()
               .ReverseMap()
               .IgnoreAllNonExisting()
               .ForAllMembers(options =>
               options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CreateCategoryViewModel, CategoryResponse>()
               .ReverseMap()
               .IgnoreAllNonExisting()
               .ForAllMembers(options =>
               options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UpdateCategoryViewModel, CategoryRequest>()
              .ReverseMap()
              .IgnoreAllNonExisting()
              .ForAllMembers(options =>
              options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UpdateCategoryViewModel, CategoryResponse>()
               .ReverseMap()
               .IgnoreAllNonExisting()
               .ForAllMembers(options =>
               options.Condition((src, dest, srcmember) => srcmember != null));
        }
    }
}
