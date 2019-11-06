using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models.Articley.ViewModel
{
    public class ArticleBlogWorks
    {
        public int? BlogWorksID { get; set; }
        public string Title { get; set; }
        public string BlogID { get; set; }
        public int? StateID { get; set; }
        public string BlogTypeName { get; set; }
        public int? BlogTypeID { get; set; }
        public string SingleBlogTypeName { get; set; }
        public int? SingleBlogTypeID { get; set; }
        public string Introduc { get; set; }
        public DateTime? PulishDate { get; set; }
        public bool? TopState { get; set; }
        public bool? OpenState { get; set; }
        public int? CommentCouont { get; set; }
        public int? BorwseCount { get; set; }
        public string WorksPath { get; set; }
    }
}