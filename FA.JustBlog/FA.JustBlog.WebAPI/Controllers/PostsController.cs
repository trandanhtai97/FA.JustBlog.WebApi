﻿using FA.JustBlog.Models.Common;
using FA.JustBlog.Services;
using FA.JustBlog.WebAPI.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FA.JustBlog.WebAPI.Controllers
{
    public class PostsController : ApiController
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IPostServices _postServices;

        public PostsController(ICategoryServices categoryServices, IPostServices postServices)
        {
            _categoryServices = categoryServices;
            _postServices = postServices;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _postServices.GetAllAsync());
        }

        // GET: api/Categories/5
        [HttpGet]
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var post = await _postServices.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Categories/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(Guid id, CategoryEditViewModel categoryEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryServices.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }

            category.Name = categoryEditViewModel.Name;
            category.UrlSlug = categoryEditViewModel.UrlSlug;
            category.Description = categoryEditViewModel.Description;

            var result = await _categoryServices.UpdateAsync(category);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        

        // DELETE: api/Categories/5
        [HttpDelete]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var post = await _postServices.GetByIdAsync(id);
            if (post == null)
            {
                NotFound();
            }
            var result = await _postServices.DeleteAsync(post);

            if (result)
            {
                return Ok(true);
            }
            return BadRequest();
        }
    }
}