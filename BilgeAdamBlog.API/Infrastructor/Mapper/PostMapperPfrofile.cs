using AutoMapper;
using BilgeAdamBlog.API.Infrastructor.Extensions;
using BilgeAdamBlog.Common.DTOs.Post;
using BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.API.Infrastructor.Mapper
{
    public class PostMapperPfrofile : Profile
    {
        public PostMapperPfrofile()
        {
            CreateMap<Post, PostRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + src.LastName));
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<Post, PostResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

        }
    }
}