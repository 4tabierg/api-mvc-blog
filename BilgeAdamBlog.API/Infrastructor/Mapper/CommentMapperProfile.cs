using AutoMapper;
using BilgeAdamBlog.API.Infrastructor.Extensions;
using BilgeAdamBlog.Common.DTOs.Comment;
using BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.API.Infrastructor.Mapper
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<Comment, CommentRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + src.LastName));
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<Comment, CommentResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

        }
    }
}