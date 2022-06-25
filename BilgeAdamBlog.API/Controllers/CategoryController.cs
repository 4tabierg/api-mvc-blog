using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Category;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Service.Service.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBlog.API.Controllers
{
    [Route("category")]
    public class CategoryController : BaseApiController<CategoryController>
    {
        private readonly ICategoryService _cs;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService cs, IMapper mapper)
        {
            _cs = cs;
            _mapper = mapper;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<List<CategoryResponse>>> Get()
        {
            UserResponse user = WorkContext.CurrentUser;
            return _mapper.Map<List<CategoryResponse>>(await _cs.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<CategoryResponse>> Get(Guid id)
        {
            var category = _mapper.Map<CategoryResponse>(await _cs.GetById(id));
            if (category == null)
                return NotFound();
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Post(CategoryRequest request)
        {
            Category entity = _mapper.Map<Category>(request);
            entity.Id = Guid.NewGuid();
            var insertResult = await _cs.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("Get", new { id = insertResult.Id }, _mapper.Map<CategoryResponse>(insertResult));
            else
                return new CategoryResponse();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponse>> Put(Guid id, CategoryRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            try
            {
                Category entity = await _cs.GetById(id);
                if (entity == null)
                    return NotFound();

                _mapper.Map(request, entity);

                var updated = await _cs.Update(entity);
                if (updated != null)
                    return _mapper.Map<CategoryResponse>(updated);

            }
            catch (Exception ex)
            {
                if (!await CategoryExist(id))
                    return NotFound();
                else
                    throw ex;
            }
            return NoContent();
        }

        private async Task<bool> CategoryExist(Guid id)
        {
            return await _cs.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryResponse>> Delete(Guid id)
        {
            Category entity = await _cs.GetById(id);
            if (entity == null)
                return NotFound();
            await _cs.Remove(entity);
            return _mapper.Map<CategoryResponse>(entity);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<CategoryResponse>> Activate(Guid id)
        {
            var result = await _cs.Activate(id);
            return _mapper.Map<CategoryResponse>(await _cs.GetById(id));
        }

        [HttpGet("getactive"),AllowAnonymous]
        public async Task<ActionResult<List<CategoryResponse>>> GetActive()
        {
            return _mapper.Map<List<CategoryResponse>>(await _cs.GetActive().ToListAsync());
        }

    }
}
