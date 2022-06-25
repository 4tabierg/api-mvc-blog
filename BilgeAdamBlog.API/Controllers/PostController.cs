using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Post;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Service.Service.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBlog.API.Controllers
{
    [Route("post")]
    public class PostController : BaseApiController<PostController>
    {
        private readonly IPostService _ps;
        private readonly IMapper _mapper;

        public PostController(IPostService ps, IMapper mapper)
        {
            _ps = ps;
            _mapper = mapper;
        }

        [HttpGet,AllowAnonymous]
        public async Task<ActionResult<List<PostResponse>>> Get()
        {
            UserResponse user = WorkContext.CurrentUser;
            return _mapper.Map<List<PostResponse>>(await _ps.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<PostResponse>> Get(Guid id)
        {
            var post = _mapper.Map<PostResponse>(await _ps.GetById(id));
            if (post == null)
                return NotFound();
            return post;
        }

        [HttpPost]
        public async Task<ActionResult<PostResponse>> Post(PostRequest request)
        {
            Post entity = _mapper.Map<Post>(request);
            entity.Id = Guid.NewGuid();
            var insertResult = await _ps.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("Get", new { id = insertResult.Id }, _mapper.Map<PostResponse>(insertResult));
            else
                return new PostResponse();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostResponse>> Put(Guid id, PostRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            try
            {
                Post entity = await _ps.GetById(id);
                if (entity == null)
                    return NotFound();

                _mapper.Map(request, entity);

                var updated = await _ps.Update(entity);
                if (updated != null)
                    return _mapper.Map<PostResponse>(updated);

            }
            catch (Exception ex)
            {
                if (!await PostExist(id))
                    return NotFound();
                else
                    throw ex;
            }
            return NoContent();
        }

        private async Task<bool> PostExist(Guid id)
        {
            return await _ps.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostResponse>> Delete(Guid id)
        {
            Post entity = await _ps.GetById(id);
            if (entity == null)
                return NotFound();
            await _ps.Remove(entity);
            return _mapper.Map<PostResponse>(entity);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<PostResponse>> Activate(Guid id)
        {
            var result = await _ps.Activate(id);
            return _mapper.Map<PostResponse>(await _ps.GetById(id));
        }

        [HttpGet("getactive"), AllowAnonymous]
        public async Task<ActionResult<List<PostResponse>>> GetActive()
        {
            return _mapper.Map<List<PostResponse>>(await _ps.GetActive().ToListAsync());
        }

        [HttpGet("GetByCategoryId/{categoryId}"), AllowAnonymous]
        public async Task<ActionResult<List<PostResponse>>> GetByCategoryId(Guid categoryId)
        {
            return _mapper.Map<List<PostResponse>>(await _ps.GetDefault(x => x.CategoryId == categoryId).ToListAsync());
        }
    }
}
