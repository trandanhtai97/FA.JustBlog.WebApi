using FA.JustBlog.Models.Common;
using FA.JustBlog.Services;
using FA.JustBlog.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FA.JustBlog.WebAPI.Controllers
{
    public class TagController : ApiController
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ITagServices _tagServices;
        private readonly IPostServices _postServices;

        public TagController(ITagServices tagServices , IPostServices postServices)
        {
            _tagServices = tagServices;
            _postServices = postServices;
        }

        // GET: api/Tags
        public async Task<IHttpActionResult> GetTag()
        {
            return Ok(await _tagServices.GetAllAsync());
        }

        // GET: api/Tags/5
        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> GetTags(Guid id)
        {
            var tag = await _tagServices.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        // PUT: api/Tags/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTags(Guid id, TagsEditViewModel tagsEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tags = await _tagServices.GetByIdAsync(id);
            if (tags == null)
            {
                return BadRequest();
            }

            tags.Name = tagsEditViewModel.Name;
            tags.UrlSlug = tagsEditViewModel.UrlSlug;
            tags.Description = tagsEditViewModel.Description;
            tags.Count = tagsEditViewModel.Count;

            var result = await _tagServices.UpdateAsync(tags);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tags
        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> PostTag(TagsEditViewModel tagEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = new Tag()
            {
                Id = tagEditViewModel.Id,
                Name = tagEditViewModel.Name,
                UrlSlug = tagEditViewModel.UrlSlug,
                Description = tagEditViewModel.Description,
                Count = tagEditViewModel.Count
            };

            var result = await _tagServices.AddAsync(tag);
            if (result <= 0)
            {
                return BadRequest(ModelState);
            }

            var tagViewNodel = new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                UrlSlug = tag.UrlSlug,
                Description = tag.Description,
                Count = tag.Count,
                IsDeleted = tag.IsDeleted,
                InsertedAt = tag.InsertedAt,
                UpdatedAt = tag.UpdatedAt,
            };

            return CreatedAtRoute("DefaultApi", new { id = tag.Id }, tagViewNodel);
        }

        // DELETE: api/Tags/5
        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> DeleteTag(Guid id)
        {
            var tag = await _tagServices.GetByIdAsync(id);
            if (tag == null)
            {
                NotFound();
            }
            var result = await _tagServices.DeleteAsync(tag);

            if (result)
            {
                return Ok(tag);
            }
            return BadRequest();
        }
    }
}