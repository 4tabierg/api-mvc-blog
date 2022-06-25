using AutoMapper;
using BilgeAdamBlog.Common.Clients.Models;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.WebUI.Infrastructor.Extensions;
using BilgeAdamBlog.WebUI.Models.AccountViewModels;

namespace BilgeAdamBlog.WebUI.Infrastructor.Mappers
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<LoginViewModel, UserRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<LoginViewModel, UserResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options =>
                options.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<LoginViewModel, LoginRequest>()
               .ReverseMap()
               .IgnoreAllNonExisting()
               .ForAllMembers(options =>
               options.Condition((src, dest, srcmember) => srcmember != null));
        }
    }
}
