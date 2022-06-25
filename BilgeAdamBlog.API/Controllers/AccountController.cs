using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.Common.Clients.Extensions;
using BilgeAdamBlog.Common.Clients.Models;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.Service.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BilgeAdamBlog.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _us;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AccountController(
            IUserService us,
            IMapper mapper,
            IConfiguration configuration)
        {
            _us = us;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("login")]
        public async Task<WebApiResponse<UserResponse>> Login([FromQuery]LoginRequest request)
        {
            var result = await _us.GetByDefault(x => x.Email == request.Email && x.Password == request.Password);
            if (result != null)
            {
                UserResponse user = _mapper.Map<UserResponse>(result);
                user.AccessToken = SetAccessToken(user);
                return new WebApiResponse<UserResponse>(true, "Success", user);
            }
            return new WebApiResponse<UserResponse>(false, "User Not Found");
        }

        private GetAccessToken SetAccessToken(UserResponse response)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,response.Email),
                new Claim(JwtRegisteredClaimNames.Jti,response.Id.ToString())
            };

            //JWT 
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Tokens:Expires"]));
            var ticks = expires.ToUnixTime();

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return new GetAccessToken
            {
                TokenType = "BilgeAdamAccessToken",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expires = ticks,
                RefreshToken = $"{response.Email}_{response.Id}_{ticks}"
            };
        }
    }
}
