using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTR.DAL;

namespace DTRBlog.Model
{
    public class ArticleDetail
    {
        /// <summary>
        /// 文章
        /// </summary>
        public DTR.DAL.BlogWorks BlogWorks { get; set; }
        /// <summary>
        /// 文章作者信息
        /// </summary>
        public DTR.DAL.BlogUser BlogUser { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int SupportCount { get; set; }
        /// <summary>
        /// 收藏数
        /// </summary>
        public int CollectionCount { get; set; }

        /// <summary>
        /// 评论集合
        /// </summary>
        public ICollection< BlogCommentList> BlogComment { get; set; }


    }

    public class BlogCommentList
    {   
        /// <summary>
        /// 评论人
        /// </summary>
        public string CommentByUser { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public DTR.DAL.BlogComment Comment { get; set; }
    }

    public class SubCommentList
    {
        public DTR.DAL.BlogComment Comment { get; set; }
        public string CommentByUser { get; set; }
    }

}

