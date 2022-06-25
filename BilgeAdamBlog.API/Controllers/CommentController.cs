using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Comment;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Service.Service.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBlog.API.Controllers
{
    [Route("comment")]
    public class CommentController : BaseApiController<CommentController>
    {
        private readonly ICommentService _cs;
        private readonly IMapper _mapper;

        public CommentController(ICommentService cs, IMapper mapper)
        {
            _cs = cs;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentResponse>>> Get()
        {
            UserResponse user = WorkContext.CurrentUser;
            return _mapper.Map<List<CommentResponse>>(await _cs.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponse>> Get(Guid id)
        {
            var comment = _mapper.Map<CommentResponse>(await _cs.GetById(id));
            if (comment == null)
                return NotFound();
            return comment;
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponse>> Post(CommentRequest request)
        {
            Comment entity = _mapper.Map<Comment>(request);
            entity.Id = Guid.NewGuid();
            var insertResult = await _cs.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("Get", new { id = insertResult.Id }, _mapper.Map<CommentResponse>(insertResult));
            else
                return new CommentResponse();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentResponse>> Put(Guid id, CommentRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            try
            {
                Comment entity = await _cs.GetById(id);
                if (entity == null)
                    return NotFound();

                _mapper.Map(request, entity);

                var updated = await _cs.Update(entity);
                if (updated != null)
                    return _mapper.Map<CommentResponse>(updated);

            }
            catch (Exception ex)
            {
                if (!await CommentExist(id))
                    return NotFound();
                else
                    throw ex;
            }
            return NoContent();
        }

        private async Task<bool> CommentExist(Guid id)
        {
            return await _cs.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentResponse>> Delete(Guid id)
        {
            Comment entity = await _cs.GetById(id);
            if (entity == null)
                return NotFound();
            await _cs.Remove(entity);
            return _mapper.Map<CommentResponse>(entity);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<CommentResponse>> Activate(Guid id)
        {
            var result = await _cs.Activate(id);
            return _mapper.Map<CommentResponse>(await _cs.GetById(id));
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<CommentResponse>>> GetActive()
        {
            return _mapper.Map<List<CommentResponse>>(await _cs.GetActive().ToListAsync());
        }
    }
}
