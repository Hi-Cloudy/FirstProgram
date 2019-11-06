using DTRBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models
{
    public class ArticleViewModel
    {
        public BlogUser BlogUser { get; set; }
        public BlogWorks BlogWorksList { get; set; }
    }
}