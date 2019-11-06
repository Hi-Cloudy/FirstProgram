using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTR.DAL;
using DTRBlog.IManager;
using DTRBlog.IService;
using DTR.DATAConfig;
using System.IO;
using DTRBlog.Model;
using System.Data.Entity;

namespace DTRBlog.Manager
{
   public class ArticleManager : IArticleManager
    {
        private IArticleService articleService = null;
        private IBlogCommentService blogCommentService = null;
        private INewsService newsService = null;
        private ISupportService supportService = null;
        private ICollectionService collectionService = null;
        public ArticleManager(
            IArticleService articleService, 
            IBlogCommentService blogCommentService, 
            INewsService newsService, 
            ISupportService supportService, 
            ICollectionService collectionService)
        {
            this.articleService = articleService;
            this.blogCommentService = blogCommentService;
            this.newsService = newsService;
            this.supportService = supportService;
            this.collectionService = collectionService;
        }

        public bool UpdateArticleBrowse(int id, int blogId)
        {
            var article = articleService.Find(blogId);
            article.BorwseCount = article.BorwseCount + 1;
           return articleService.Update(article);
        }
        public bool ExistArticle(string authorId, int articleId)
        {
           return articleService.Any(a => a.BlogWorksID == articleId&&a.BlogID==authorId);
        }
        public List<DTR.DAL.BlogWorks> FindAllBlogWorksByUser(string blogUser)
        {
            return  articleService.FindList(0,
              u => u.BlogUser.BlogID == blogUser && u.OpenState == true&&u.StateID==3,
              OrderType.Desc, 
              blog=>blog.PublishDate).ToList();
        }
        public List<SubCommentList>  ShowSubComment(int ParentCommentId)
        {
            var comment = blogCommentService.Find(c => c.ParentCommentID == ParentCommentId).OrderByDescending(o => o.CommentDate).Select(c =>
                  new SubCommentList
                  {
                      Comment = c,
                      CommentByUser = c.BlogID
                  }
            ).ToList();

            return comment;
        }
        public ArticleDetail ShowArticle(string authorId, int articleId, string sessionId)
        {
            IQueryable<DTR.DAL.BlogWorks> temp = null;
            if (authorId == sessionId)
            {
                temp = articleService.Find(a => a.BlogWorksID == articleId && a.BlogID == authorId);
            }
            else {
                temp = articleService.Find(a => a.BlogWorksID == articleId && a.BlogID == authorId
                  && a.OpenState == true && a.StateID == 3);
            }
            int collectionCount = this.CollectionCount(articleId);
            var article = temp.Select(s => new ArticleDetail
                {
                    BlogWorks = s,
                    BlogUser = s.BlogUser,
                    CommentCount = s.BlogComment.Count(),
                    SupportCount=s.Support.Count(),
                    CollectionCount= collectionCount,
                    BlogComment = s.BlogComment.Where(cm=>cm.ParentCommentID==0).OrderByDescending(o=>o.CommentDate).Select(c => 
                        new BlogCommentList
                        {
                            Comment = c,
                            CommentByUser =c.BlogID,
                        }
                    ).ToList()
                }).FirstOrDefault();

            if (article == null)
                return null;

           
            string filePath = Path.Combine(DATAPathConfigs.DTRDataConfigFullPath, article.BlogWorks.WorksPath, DATAPathConfigs.WorksFile);

            string strData = "";
            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    strData = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
            }
            article.BlogWorks.WorksPath = strData;

            return article;
        }
        public  bool GoodAdd(int articleId ,string fromBlogUser,string targetBlogUser)
        {
            DateTime dateTime = DateTime.Now;
            int  result;
            if (!supportService.Any(s => s.BlogID == fromBlogUser && s.BlogWorksID == articleId))
            {
                result = supportService.Add(new Support()
                {
                    BlogID = fromBlogUser,
                    BlogWorksID = articleId,
                    SupportDate = DateTime.Now

                });

                var article = articleService.Find(articleId);
                article.SupportCount += 1;
                articleService.Update(article);

                var t = new DTR.DAL.News() {
                    AccepteBlogID = targetBlogUser,
                    NewsTypeID = 3,
                    SourceBlogID = fromBlogUser,
                    NewsContent = "点赞了你的作品",
                    NewsDate = dateTime,
                    ReadState = false,
                    URL = articleId.ToString()
                };
                newsService.Add(t);
                return result>0;
            }
            return false;
          
        }
        public bool BadAdd(int articleId, string fromBlogId)
        {
            var article = articleService.Find(articleId);
            article.SupportCount =- 1;
            var result = articleService.Update(article);
            if (this.IsExistSupport(articleId, fromBlogId))
              return  supportService.Delete( supportService.Find(s => s.BlogID == fromBlogId && s.BlogWorksID == articleId).First());
            return false;
        }
        public bool IsExistSupport(int articleId, string fromBlogId)
        {
            return supportService.Any(s => s.BlogID == fromBlogId && s.BlogWorksID == articleId);
        }
        public bool AddComment(string BlogId, int BlogArticleId, string Content, out int CommentId, string AuthorArticleId)
        {
            DateTime dateTime = DateTime.Now;

            DTR.DAL.BlogComment blogComment = new DTR.DAL.BlogComment()
            {
                BlogID = BlogId,
                BlogWorksID = BlogArticleId,
                CommentContent = Content,
                CommentDate = dateTime,
                ReadState = false,
                ParentCommentID = 0
            };
          
            int isSuccess = blogCommentService.Add(blogComment);

            newsService.Add( new DTR.DAL.News(){
                    AccepteBlogID = AuthorArticleId,
                    NewsTypeID = 1,
                    SourceBlogID = BlogId,
                    NewsContent = Content,
                    NewsDate = dateTime,
                    ReadState = false,
                    URL = BlogArticleId.ToString()

            });

            CommentId = blogComment.CommentID;

          return isSuccess > 0;
        }
        public bool ReplyComment(string BlogId,int ArticleId , string Content,int ReplyId,out int CommentId, string AuthorArticleId)
        {
            DTR.DAL.BlogComment reply = new DTR.DAL.BlogComment
            {
                BlogID = BlogId,
                BlogWorksID=ArticleId,
                CommentContent = Content,
                CommentDate = DateTime.Now,
                ReadState = false,
                ParentCommentID = ReplyId
            };
           int isSuccess =blogCommentService.Add(reply);

            newsService.Add(new DTR.DAL.News()
            {
                AccepteBlogID = AuthorArticleId,
                NewsTypeID = 5,
                SourceBlogID = BlogId,
                NewsContent = Content,
                NewsDate = DateTime.Now,
                ReadState = false,
                URL = ArticleId.ToString()

            });

            CommentId = reply.CommentID;

            return isSuccess > 0;
        }
        public IEnumerable<ArticleList> GetAllArticleByUser(int pageIndex, int pageSize, string blogUser, string currentUser, out int count)
        {
            List<ArticleList> list = new List<ArticleList>();
            IQueryable<DTR.DAL.BlogWorks> topList = null;
            IQueryable<DTR.DAL.BlogWorks> noTopList = null;
            if (blogUser == currentUser)
            {
                topList = articleService.FindList(0, u => u.BlogUser.BlogID == blogUser && u.TopState == true, OrderType.Desc, order => order.PublishDate);
                noTopList = articleService.FindList(0, u => u.BlogUser.BlogID == blogUser && u.TopState == false, OrderType.Desc, order => order.PublishDate);
            }
            else
            {
                topList = articleService.FindList(0, u => u.BlogUser.BlogID == blogUser && u.OpenState == true && u.StateID == 3 && u.TopState == true, OrderType.Desc, order => order.PublishDate);
                noTopList = articleService.FindList(0, u => u.BlogUser.BlogID == blogUser && u.OpenState == true && u.StateID == 3 && u.TopState == false, OrderType.Desc, order => order.PublishDate);
            }
            foreach (var item in topList)
            {
                ArticleList t = new ArticleList()
                {
                    BlogUser = item.BlogUser,
                    Article = item
                };

                list.Add(t);
            }
            foreach (var item in noTopList)
            {
                ArticleList t = new ArticleList()
                {
                    BlogUser = item.BlogUser,
                    Article = item
                };
                list.Add(t);
            }
            count = list.Count;

            foreach (var item in list)
            {
                var commentCount = blogCommentService.Count(comment => comment.BlogWorksID == item.Article.BlogWorksID);
                item.TitleImg = GetArticleCover(item.Article.WorksPath);
                item.Article.CommentCount = commentCount;
            }
           var aList= list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return aList;
         
        }
        public bool RemoveComment(int commentId)
        {
            var t = blogCommentService.Find(commentId);
            var isSuccess= blogCommentService.Delete(t);
            return isSuccess;
        }
        public bool AddCollection(int articleId, string fromBlogUser, string targetBlogUser)
        {
            DateTime dateTime = DateTime.Now;
            int result;
            if (!collectionService.Any(s => s.BlogID == fromBlogUser && s.BlogWorksID == articleId))
            {
                result = collectionService.Add(new Collection()
                {
                    BlogID = fromBlogUser,
                    BlogWorksID = articleId,
                    CollectionDate = DateTime.Now

                });
                var t = new DTR.DAL.News()
                {
                    AccepteBlogID = targetBlogUser,
                    NewsTypeID = 4,
                    SourceBlogID = fromBlogUser,
                    NewsContent = "收藏了你的作品",
                    NewsDate = dateTime,
                    ReadState = false,
                    URL = articleId.ToString()
                };
                newsService.Add(t);
                return result > 0;
            }
            return false;

        }
        public bool CancelCollection(int articleId, string fromBlogId)
        {
            if (this.IsExistCollection(articleId, fromBlogId))
                return collectionService.Delete(collectionService.Find(s => s.BlogID == fromBlogId && s.BlogWorksID == articleId).First());
            return false;
        }
        public bool IsExistCollection(int articleId, string fromBlogId)
        {
            return collectionService.Any(s => s.BlogID == fromBlogId && s.BlogWorksID == articleId);
        }
        public int CollectionCount(int articleID)
        {
            return collectionService.Count(c => c.BlogWorksID == articleID);
        }

        public string GetArticleCover(string path)
        {
            string titlePathTmp = Path.Combine(DATAPathConfigs.DTRDataConfigFullPath, path, DATAPathConfigs.WorksFace);

            string query = "?query=" + new Random().Next(1, 10000000).ToString();


            string coverImg= Path.Combine(DATAPathConfigs.DTRDataConfigNetWorkelPath, path, DATAPathConfigs.WorksFace+ query);

            string titleImg = File.Exists(titlePathTmp) ? coverImg : null;
            return titleImg;
        }
    }
}
