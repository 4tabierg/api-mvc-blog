using AutoMapper;
using BilgeAdamBlog.API.Infrastructor.Extensions;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.Model.Entities;

namespace BilgeAdamBlog.API.Infrastructor.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + src.LastName));
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<User, UserResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmember) => srcmember != null));

        }
    }
}