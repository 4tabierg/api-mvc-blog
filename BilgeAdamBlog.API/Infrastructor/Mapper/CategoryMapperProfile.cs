using AutoMapper;
using BilgeAdamBlog.API.Infrastructor.Extensions;
using BilgeAdamBlog.Common.DTOs.Category;
using BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.API.Infrastructor.Mapper
{
    public class CategoryMapperProfile :  Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + src.LastName));
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<Category, CategoryResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

        }
    }
}
