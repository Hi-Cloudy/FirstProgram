using DTR.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models
{
    public class SearchArticleViewModel
    {
        public BlogWorks Article { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}