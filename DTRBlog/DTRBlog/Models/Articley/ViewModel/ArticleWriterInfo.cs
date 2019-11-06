using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTRBlog.Models.Articley.ArticleBLL;

namespace DTRBlog.Models.Articley.ViewModel
{
    public class ArticleWriterInfo
    {
        public ArticleWriterInfo() { }

        /// <summary>
        /// 表示必要的文章信息数据模型
        /// </summary>
        /// <param name="BlogID">用户账号</param>
        /// <param name="BlogWorks">文章ID</param>
        /// <param name="compentPath">文章文件夹对应的相对路径</param>
        public ArticleWriterInfo(string BlogID,int? BlogWorks,string compentPath)
        {
            this.BlogID = BlogID;
            this.BlogWorks = BlogWorks;
            this.WorksContent = new ArticleFile(compentPath);
        }

        public string BlogID { get; set; }//表示用户账户
        public int? BlogWorks { get; set; }//表示文章的标识
        public ArticleFile WorksContent { get; set; }//表示文章文件
        public bool IsFrozen { get; set; } = false; //是否冻结了
    }
}