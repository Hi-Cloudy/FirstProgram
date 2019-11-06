using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models.Articley.ViewModel
{
    //用户修改的文章信息
    public class UpdateArticleInfo
    {
        public string BlogID { get; set; }
        public int? BlogTypeID { get; set; } = 0;
        public int? BlogWorksID { get; set; } = 0;
        public string Title { get; set; }
        public string Introduc { get; set; }
        public int? SingleBlogTypeID { get; set; } = 0;
        public string WorksPath { get; set; }
    }
}