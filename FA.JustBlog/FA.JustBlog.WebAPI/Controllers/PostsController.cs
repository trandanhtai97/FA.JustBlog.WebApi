using FA.JustBlog.Models.Common;
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

        // GET: api/Post
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _postServices.GetAllAsync());
        }

        // GET: api/POst/5
        [HttpGet]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var post = await _postServices.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CreateUpdate(PostEditViewModel postEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (postEditViewModel.IsEdit)
            {
                var postEdit = await Update(postEditViewModel);
                if (postEdit == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(postEdit);
            }
            var postCreate = await Create(postEditViewModel);
            if (postCreate == null)
            {
                return BadRequest();
            }
            return Ok(postCreate);
        }

        private async Task<Post> Create(PostEditViewModel postEditViewModel)
        {
            var post = new Post()
            {
                Id = postEditViewModel.Id,
                Title = postEditViewModel.Title,
                UrlSlug = postEditViewModel.UrlSlug,
                ShortDescription = postEditViewModel.ShortDescription,
                PublishedDate = postEditViewModel.PublishedDate,
                CategoryId = postEditViewModel.CategoryId,
                
            };

            var result = await _postServices.AddAsync(post);
            if (result > 0)
            {
                return post;
            }
            else
            {
                return null;
            }
        }

        private async Task<Post> Update(PostEditViewModel postEditViewModel)
        {
            var post = await _postServices.GetByIdAsync(postEditViewModel.Id);
            if (post == null)
            {
                return null;
            }

            post.Title = postEditViewModel.Title;
            post.UrlSlug = postEditViewModel.UrlSlug;
            post.ShortDescription = postEditViewModel.ShortDescription;
            post.PublishedDate = postEditViewModel.PublishedDate;
            post.CategoryId = postEditViewModel.CategoryId;

            var result = await _postServices.UpdateAsync(post);
            if (result)
            {
                return post;
            }
            else
            {
                return null;
            }
        }

        // DELETE: api/post/5
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