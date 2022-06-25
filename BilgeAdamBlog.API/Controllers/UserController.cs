using AutoMapper;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Service.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBlog.API.Controllers
{
    [Route("user")]
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _us;
        private readonly IMapper _mapper;

        public UserController(IUserService us, IMapper mapper)
        {
            _us = us;
            _mapper = mapper;
        }

        [HttpGet,AllowAnonymous]
        public async Task<ActionResult<List<UserResponse>>> Get()
        {
            UserResponse user = WorkContext.CurrentUser;
            return _mapper.Map<List<UserResponse>>(await _us.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<UserResponse>> Get(Guid id)
        {
            var user = _mapper.Map<UserResponse>(await _us.GetById(id));
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post(UserRequest request)
        {
            User entity = _mapper.Map<User>(request);
            entity.Id = Guid.NewGuid();
            var insertResult = await _us.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("Get", new { id = insertResult.Id }, _mapper.Map<UserResponse>(insertResult));
            else
                return new UserResponse();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> Put(Guid id, UserRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            try
            {
                User entity = await _us.GetById(id);
                if (entity == null)
                    return NotFound();

                _mapper.Map(request, entity);

                var updated = await _us.Update(entity);
                if (updated != null)
                    return _mapper.Map<UserResponse>(updated);

            }
            catch (Exception ex)
            {
                if (!await UserExist(id))
                    return NotFound();
                else
                    throw ex;
            }
            return NoContent();
        }

        private async Task<bool> UserExist(Guid id)
        {
            return await _us.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponse>> Delete(Guid id)
        {
            User entity = await _us.GetById(id);
            if (entity == null)
                return NotFound();
            await _us.Remove(entity);
            return _mapper.Map<UserResponse>(entity);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<UserResponse>> Activate(Guid id)
        {
            var result = await _us.Activate(id);
            return _mapper.Map<UserResponse>(await _us.GetById(id));
        }

        [HttpGet("getactive"), AllowAnonymous]
        public async Task<ActionResult<List<UserResponse>>> GetActive()
        {
            return _mapper.Map<List<UserResponse>>(await _us.GetActive().ToListAsync());
        }
    }
}
