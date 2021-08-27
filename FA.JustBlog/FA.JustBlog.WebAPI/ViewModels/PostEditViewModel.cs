using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.WebAPI.ViewModels
{
    public class PostEditViewModel : BasicViewModel
    {
        //    {
        //"Id": "b15ce7cf-8f89-45e7-bf65-e69b71665624",
        //"Title": "Post 06",
        //"ShortDescription": "This is Post 06",
        //"ImageUrl": "blog-6.jpg",
        //"PostContent": "Content post 06",
        //"UrlSlug": "post-06",
        //"Published": true,
        //"CategoryId": "2995ffcd-9be5-4f7e-a449-96d349ea234d",
        //"TagIds": [
        //    "394ed967-c9c1-4438-902e-096a53a1ae98",
        //    "efe2f12b-d42b-4486-83c3-3351f45e7c44"
        //]
        //}
        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [MaxLength(1000, ErrorMessage = "The {0} must less than {1} characters")]
        public string ShortDescription { get; set; }

        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 4)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string PostContent { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string UrlSlug { get; set; }

        public bool Published { get; set; }

        public DateTime PublishedDate { get; set; }

        public int ViewCount { get; set; }

        public int RateCount { get; set; }

        public int TotalRate { get; set; }

        public decimal Rate => RateCount == 0 ? 0 : TotalRate / RateCount;
        public bool IsEdit { get; set; }

        public Guid CategoryId { get; set; }


        public Guid CommentIds { get; set; }


        public List<Guid> TagIds { get; set; }
    }
}