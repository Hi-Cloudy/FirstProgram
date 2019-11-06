using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRBlog.Models.Articley.ViewModel
{
    /// <summary>
    /// 表示需要保存的文章信息
    /// </summary>
    public class SaveArticleInfo
    {
        public int? BlogWorksID { get; set; }
        public string Title { get; set; }
        public int? BlogTypeID { get; set; }
        public int? SingBlogTypeID { get; set; }
        public string Introduc { get; set; }
        public int? CharLeng { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}