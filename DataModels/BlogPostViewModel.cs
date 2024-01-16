using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Web.DataModels
{
    public class BlogPostViewModel
    {
       public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public List<BlogPostImageViewModel> Images { get; set; } = new();
    }
}