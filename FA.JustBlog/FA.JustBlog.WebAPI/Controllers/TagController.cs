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
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CreateUpdate(TagsEditViewModel tagEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tagEditViewModel.IsEdit)
            {
                var tagEdit = await Update(tagEditViewModel);
                if (tagEdit == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(tagEdit);
            }
            var tagCreated = await Create(tagEditViewModel);
            if (tagCreated == null)
            {
                return BadRequest();
            }
            return Ok(tagCreated);
        }

        private async Task<Tag> Create(TagsEditViewModel tagEditViewModel)
        {
            var tag = new Tag()
            {
                Id = tagEditViewModel.Id,
                Name = tagEditViewModel.Name,
                UrlSlug = tagEditViewModel.UrlSlug,
                Description = tagEditViewModel.Description,
            };

            var result = await _tagServices.AddAsync(tag);
            if (result > 0)
            {
                return tag;
            }
            else
            {
                return null;
            }
        }

        private async Task<Tag> Update(TagsEditViewModel tagEditViewModel)
        {
            var tags = await _tagServices.GetByIdAsync(tagEditViewModel.Id);
            if (tags == null)
            {
                return null;
            }

            tags.Name = tagEditViewModel.Name;
            tags.UrlSlug = tagEditViewModel.UrlSlug;
            tags.Description = tagEditViewModel.Description;
            var result = await _tagServices.UpdateAsync(tags);
            if (result)
            {
                return tags;
            }
            else
            {
                return null;
            }
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