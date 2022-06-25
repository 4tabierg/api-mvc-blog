using AutoMapper;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.UserViewModels;
using BilgeAdamBlog.WebUI.Infrastructor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.Infrastructor.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserViewModel, UserRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UserViewModel, UserResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CreateUserViewModel, UserRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<CreateUserViewModel, UserResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UpdateUserViewModel, UserRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<UpdateUserViewModel, UserResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));
        }
    }
}
