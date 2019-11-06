using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTR.DAL;

namespace DTRBlog.Models
{
    public class HomeIndexBlogInfoViewModel
    {
        public BlogType BlogType { get; set; }
        public IEnumerable<BlogWorks> Works { get; set; }
    }
}