using DTRBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models
{
    public class BlogUserViewModel
    {
        public BlogUser BlogUser { get; set; }
        public List<BlogWorks> ArticleTopList { get; set; }
        public List<BlogWorks> ArticleNoTopList { get; set; }
    }
}