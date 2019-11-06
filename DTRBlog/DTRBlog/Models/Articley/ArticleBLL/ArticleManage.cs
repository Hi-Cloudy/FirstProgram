using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTRBlog.Models.Articley.ViewModel;
using Newtonsoft.Json;
using DTR.DAL;
using DTR.DATAConfig;

namespace DTRBlog.Models.Articley.ArticleBLL
{
    public class ArticleManage : ArticleBase
    {
        private IEnumerable<ArticleBlogWorks> TransFormArticleBlogWorks(IEnumerable<BlogWorks> blogWorks)
        {
            return blogWorks.Select(w => new ArticleBlogWorks()
            {
                BlogWorksID = w.BlogWorksID,
                Title = w.Title,
                BlogID = w.BlogID,
                StateID = w.StateID,
                BlogTypeID = w.BlogType != null ? w.BlogTypeID : 0,
                SingleBlogTypeID = w.SingleBlogType != null ? w.SingleBlogType.SingleBlogTypeID : 0,
                Introduc = w.Introduc,
                PulishDate = w.PublishDate,
                TopState = w.TopState,
                OpenState = w.OpenState,
                CommentCouont = w.BlogComment.Count(),
                BorwseCount = w.BorwseCount,
                WorksPath = w.WorksPath
            }) ;
        }
       
        private bool AddArticle(string BlogID,string compentPaht)
        {
            BlogWorks blogWorks = new BlogWorks {
                BlogID = BlogID,
                StateID = 1,
                BlogTypeID = null,
                SingleBlogTypeID = null,
                Title = "一个新的博客",
                PublishDate = DateTime.Now,
                TopState = false,
                OpenState = true,
                CharLength = 0,
                SupportCount = 0,
                FollowCount =0,
                CommentCount = 0,
                BorwseCount = 0,
                WorksPath = compentPaht
            };
            this.dbCtrl.BlogWorks.Add(blogWorks);
            return this.dbCtrl.SaveChanges() > 0;
        }

  
        public BlogWorksUI GetBlogWorksUI(int? BlogWorksID)
        {
            if (BlogWorksID == null || BlogWorksID <= 0)
                return null;

            BlogWorks blogWorks = this.dbCtrl.BlogWorks.Find(BlogWorksID);
            if (blogWorks == null)
                return null;
            return new BlogWorksUI {
                BlogTypeID = blogWorks.BlogTypeID,
                SingBlogTypeID = blogWorks.SingleBlogTypeID
            };
        }
     
        public ArticleBlogWorks GetArticle(int? BlogWorksID)
        {
            BlogWorks w = this.dbCtrl.BlogWorks.Find(BlogWorksID);
            if (w == null)
                return null;
            string worksFace = DATAPathConfigs.DTRDataConfigFullPath + w.WorksPath + DATAPathConfigs.WorksFace;
            string httpWorksFace = null;
            if (System.IO.File.Exists(worksFace))
                httpWorksFace = WrapPath.WorksFaceUrl(w.WorksPath);
            else
                httpWorksFace = "";
            return new ArticleBlogWorks()
            {
                Title = w.Title,
                Introduc = w.Introduc,
                WorksPath = httpWorksFace,
            };
        }
   
        public IEnumerable<ArticleBlogWorks> GetArticleBlogWorks(QueryArticleCondition articleCondition, string BlogID)
        {
            if (string.IsNullOrEmpty(BlogID))
                return Enumerable.Empty<ArticleBlogWorks>();

            IQueryable<BlogWorks> blogWorks = this.GetEntity<BlogWorks>().Where(w => w.BlogID == BlogID).Where(w => w.StateID == 3).OrderByDescending(w=>w.TopState).ThenByDescending(w=>w.PublishDate);
            if (articleCondition != null)
            {
                if (articleCondition.Time != null)
                {
                    DateTime dateTime = articleCondition.Time.Value.AddDays(1);
                    blogWorks = blogWorks.Where(w => w.PublishDate >= articleCondition.Time && w.PublishDate < dateTime);
                }
                if (articleCondition.isNoPublic != null)
                    blogWorks = blogWorks.Where(w=>w.OpenState == !articleCondition.isNoPublic);
                if (articleCondition.BlogTypeID > 0)
                    blogWorks = blogWorks.Where(w => w.BlogTypeID == articleCondition.BlogTypeID);
                if (articleCondition.SingBlogTypeID > 0)
                    blogWorks = blogWorks.Where(w => w.SingleBlogTypeID == articleCondition.SingBlogTypeID);
                if (!string.IsNullOrEmpty(articleCondition.Title))
                    blogWorks = blogWorks.Where(w => w.Title.Contains(articleCondition.Title));
            }

            return this.TransFormArticleBlogWorks(blogWorks);
        }
    
        public IEnumerable<ArticleBlogWorks> GetArticleBlogWorksDraft(string BlogID)
        {
            if (string.IsNullOrEmpty(BlogID))
                return Enumerable.Empty<ArticleBlogWorks>();

            IQueryable<BlogWorks> blogWorks = this.GetEntity<BlogWorks>().Where(w=> w.BlogID == BlogID && w.StateID == 1).OrderByDescending(w=>w.PublishDate);
            return Enumerable.Select(blogWorks, w => new ArticleBlogWorks()
            {
                BlogWorksID = w.BlogWorksID,
                Title = w.Title,
                BlogID = w.BlogID,
                Introduc = w.Introduc,
                PulishDate = w.PublishDate,
                WorksPath = w.WorksPath
            });
        }
     
        public IEnumerable<ArticleBlogWorks> GetArticleBlogWorksWait(string BlogID)
        {
            if (string.IsNullOrEmpty(BlogID))
                return Enumerable.Empty<ArticleBlogWorks>();

            IQueryable<BlogWorks> blogWorks = this.GetEntity<BlogWorks>().Where(w => w.BlogID == BlogID).Where(w=>w.StateID==2 || w.StateID == 4).OrderBy(w=>w.StateID).ThenByDescending(w=>w.PublishDate);
            return Enumerable.Select(blogWorks,w=>new ArticleBlogWorks {
                BlogWorksID = w.BlogWorksID,
                Title = w.Title,
                Introduc = w.Introduc,
                PulishDate = w.PublishDate,
                StateID = w.StateID,
                WorksPath = w.WorksPath
            });
        }
        
        public IEnumerable<BlogTypeUI> GetBlogType()
        {
            return Enumerable.Select(this.dbCtrl.BlogType, t=>new BlogTypeUI {
                BlogTypeID = t.BlogTypeID,
                BlogTypeName = t.BlogTypeName,
            }).ToList();
        }
      
        public IEnumerable<SingType> GetSingType(string BlogID)
        {
            return Enumerable.Select(this.dbCtrl.SingleBlogType.Where(u=>u.BlogID == BlogID), t=>new SingType {
                SingleTypeID = t.SingleBlogTypeID,
                SingTypeName = t.SingleBlogTypeName
            }).ToList();
        }
        
        public bool DeleteArticle(int? BlogWorksID)
        {
            if (BlogWorksID == null)
                return false;
            try
            {
                var article = this.dbCtrl.BlogWorks.Find(BlogWorksID);
                if (article == null)
                    return false;

                ArticleFile articleFile = new ArticleFile(article.WorksPath);
                articleFile.Delete();
                this.dbCtrl.BlogWorks.Remove(article);
                return this.SaveChange() > 0;
            }
            catch 
            {}
            return false;
        }
 
        public bool TopStateChange(int? BlogWorksID)
        {
            if (BlogWorksID == null)
                return false;
            var article = this.dbCtrl.BlogWorks.Find(BlogWorksID);
            if (article == null)
                return false;

            article.TopState = !article.TopState;
            return this.SaveChange() > 0;
        }
        
        public bool OpenStateChange(int? BlogWorksID)
        {
            if (BlogWorksID == null)
                return false;
            var article = this.dbCtrl.BlogWorks.Find(BlogWorksID);
            if (article == null)
                return false;

            article.OpenState = !article.OpenState;
            return this.SaveChange() > 0;
        }
  
        public bool PublishArticle(int? BlogWorksID)
        {
            if (BlogWorksID == null)
                return false;
            var article = this.dbCtrl.BlogWorks.Find(BlogWorksID);
            if (article == null)
                return false;

            article.StateID = 3;
            article.PublishDate = DateTime.Now;
            return this.SaveChange() > 0;
        }
        public bool SaveArticle(SaveArticleInfo saveArticle)
        {
            if (saveArticle == null)
                return false;

            BlogWorks blogWorks = this.dbCtrl.BlogWorks.Find(saveArticle.BlogWorksID);
            if (blogWorks == null)
                return false;

            string worksPath = blogWorks.WorksPath;
            blogWorks.Title = saveArticle.Title;
            blogWorks.BlogTypeID = saveArticle.BlogTypeID;
            blogWorks.CharLength = saveArticle.CharLeng;
            blogWorks.SingleBlogTypeID = saveArticle.SingBlogTypeID;
            blogWorks.Introduc = saveArticle.Introduc;
            blogWorks.PublishDate = DateTime.Now;
            
            try
            {
                this.dbCtrl.SaveChanges();
            }
            catch
            {
                return false;
            }

            new ArticleFile(worksPath).UpdateContent(saveArticle.Content);
            return true;
        }
        public bool UpdateArticle(UpdateArticleInfo ArticleInfo)
        {
           BlogWorks works =  this.dbCtrl.BlogWorks.Find(ArticleInfo.BlogWorksID);
            if (works == null)
                return false;
            if (works.BlogID != ArticleInfo.BlogID)
                return false;
            if (ArticleInfo.BlogTypeID > 0)
                works.BlogTypeID = ArticleInfo.BlogTypeID;
            else
                works.BlogTypeID = null;
            if (ArticleInfo.SingleBlogTypeID > 0)
                works.SingleBlogTypeID = ArticleInfo.SingleBlogTypeID;
            else
                works.SingleBlogTypeID = null;
            works.Title = ArticleInfo.Title;
            works.Introduc = ArticleInfo.Introduc;
            try
            {
                return this.dbCtrl.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ArticleWriterInfo GetArticleWriterInfo(string BlogID,int? BlogWorksID)
        {
            if (string.IsNullOrEmpty(BlogID))
            {
                this.Dispose();
                return null;
            }

            ArticleWriterInfo writerInfo = null;


            BlogUser blogUser = this.dbCtrl.BlogUser.Find(BlogID);
            if (blogUser == null)
            {
                this.Dispose();
                return null;
            }

            BlogWorks works = this.dbCtrl.BlogWorks.Find(BlogWorksID);
            if (blogUser != null && blogUser.FrozenState)
            {
                writerInfo = new ArticleWriterInfo();
                writerInfo.IsFrozen = true;
                return writerInfo;
            }

            if (works != null && works.BlogID == BlogID)
            {
                writerInfo = new ArticleWriterInfo(BlogID, works.BlogWorksID, works.WorksPath);
                this.Dispose();
                return writerInfo;
            }

            
                
            ArticleCreate articleCreate = new ArticleCreate(blogUser.UserPath);
            string CompentPath = articleCreate.ArticlePath;
            bool isAdd = this.AddArticle(BlogID, CompentPath);
            if (!isAdd) {
                this.Dispose();
                return null;
            }
                
            try
            {
                BlogWorks NewWorks = this.dbCtrl.BlogWorks.Where(w => w.BlogID == BlogID).Where(w => w.WorksPath == CompentPath).Single();
                writerInfo = new ArticleWriterInfo(BlogID,NewWorks.BlogWorksID, NewWorks.WorksPath);
                this.Dispose();
                return writerInfo;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }
    }
}