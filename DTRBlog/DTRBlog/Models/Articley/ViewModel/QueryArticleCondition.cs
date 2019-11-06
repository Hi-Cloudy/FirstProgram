using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models.Articley.ViewModel
{
    public class QueryArticleCondition
    {
        public DateTime? Time { get; set; }
        public int? BlogTypeID { get; set; }
        public int? SingBlogTypeID { get; set; }
        public string Title { get; set; }
        public bool? isNoPublic { get; set; }
    }
}