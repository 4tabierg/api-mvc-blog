using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Post;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels;
using BilgeAdamBlog.WebUI.Infrastructor.Extensions;

namespace BilgeAdamBlog.WebUI.Infrastructor.Mappers
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            CreateMap<PostViewModel, PostRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(optitions =>
                optitions.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<PostViewModel, PostResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(optitions =>
                optitions.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CreatePostViewModel, PostRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(optitions =>
                optitions.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CreatePostViewModel, PostResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(optitions =>
                optitions.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UpdatePostViewModel, PostRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(optitions =>
                optitions.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UpdatePostViewModel, PostResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(optitions =>
                optitions.Condition((src, dest, srcmember) => srcmember != null));
        }
    }
}
