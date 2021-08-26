using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FA.JustBlog.WebAPI.ViewModels
{
    public class CommentViewModel : BasicViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string CommentHeader { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }
    }
    public class CommentEditViewModel : BasicViewModel
    {
        [Required]
        [MaxLength(255, ErrorMessage = "The {0} is at least {1} characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The {0} is at least {1} characters")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The {0} is at least {1} characters")]
        public string CommentHeader { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The {0} is at least {1} characters")]
        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }

        public Guid PostId { get; set; }


    }
}