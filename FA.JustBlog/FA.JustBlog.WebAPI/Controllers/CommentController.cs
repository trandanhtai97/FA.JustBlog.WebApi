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
    public class CommentController : ApiController
    {

        private readonly ICommentServices _commentServices;
        private readonly IPostServices _postServices;

        public CommentController(ICommentServices commentServices, IPostServices postServices)
        {
            _commentServices = commentServices;
            _postServices = postServices;
        }

        // GET: api/Tags
        public async Task<IHttpActionResult> GetComment()
        {
            return Ok(await _commentServices.GetAllAsync());
        }

        // GET: api/Tags/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(Guid id)
        {
            var comment = await _commentServices.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Tags/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTags(Guid id, CommentEditViewModel commentEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentServices.GetByIdAsync(id);
            if (comment == null)
            {
                return BadRequest();
            }

            comment.Name = commentEditViewModel.Name;
            comment.CommentHeader = commentEditViewModel.CommentHeader;
            comment.CommentText = commentEditViewModel.CommentText;
            comment.CommentTime = commentEditViewModel.CommentTime;

            var result = await _commentServices.UpdateAsync(comment);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

       

        // DELETE: api/Tags/5
        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> DeleteComment(Guid id)
        {
            var comment = await _commentServices.GetByIdAsync(id);
            if (comment == null)
            {
                NotFound();
            }
            var result = await _commentServices.DeleteAsync(comment);

            if (result)
            {
                return Ok(comment);
            }
            return BadRequest();
        }

    }
}
