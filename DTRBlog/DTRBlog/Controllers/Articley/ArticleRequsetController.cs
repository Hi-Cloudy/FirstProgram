using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTRBlog.Models.Articley.ArticleBLL;
using DTRBlog.Models.Articley.ViewModel;
using DTRBlog.Public;
using Newtonsoft.Json;

namespace DTRBlog.Controllers.Articley
{
    //[AuthorityFilter("~/Home/Login")]
    public class ArticleRequsetController : Controller
    {
        public ArticleRequsetController()
        {
            this.article = new ArticleManage();
        }

        private ArticleManage article = null;
        //返还文章数据
        public JsonResult GetArticles(QueryArticleCondition articleCondition,string BlogID)
        {
            var a = Json(new
            {
                Article = article.GetArticleBlogWorks(articleCondition, BlogID)
            });
            return a;
        }
        //返回草稿  有默认值
        public JsonResult GetArticlesDraft(string BlogID)
        {
            return Json(new
            {
                Article = article.GetArticleBlogWorksDraft(BlogID)
            }, JsonRequestBehavior.AllowGet);
        }
        //返回待审核以及审核未通过的文章  有默认值
        public JsonResult GetWaitArticel(string BlogID)
        {
            return Json(new
            {
                Article = article.GetArticleBlogWorksWait(BlogID)
            }, JsonRequestBehavior.AllowGet);
        }
        //删除文章
        [HttpPost]
        public JsonResult DeleteWorks(int? BlogWorksID)
        {
            bool isOk = article.DeleteArticle(BlogWorksID);
            article.Dispose();
            if (isOk)
                return Json(new { IsOk=true,BlogWorksID = BlogWorksID.Value});
            return Json(new { IsOk = false });

        }
        //改变顶置状态
        [HttpPost]
        public JsonResult ChangTopState(int? BlogWorksID)
        {
            bool isOk = article.TopStateChange(BlogWorksID);
            article.Dispose();
            if (isOk)
                return Json(new { IsOk = true});
            return Json(new { IsOk = false });
        }
        //改变顶置状态
        [HttpPost]
        public JsonResult ChangOpenState(int? BlogWorksID)
        {
            bool isOk = article.OpenStateChange(BlogWorksID);
            article.Dispose();
            if (isOk)
                return Json(new { IsOk = true });
            return Json(new { IsOk = false });
        }
        //发布文章
        [HttpPost]
        public JsonResult PublishArticle(int? BlogWorksID)
        {
            bool isOk = article.PublishArticle(BlogWorksID);
            article.Dispose();
            if (isOk)
                return Json(new { IsOk = true });
            return Json(new { IsOk = false });
        }
        //修改文章信息
        [HttpPost]
        public JsonResult UpdateArticle(UpdateArticleInfo ArticleInfo)
        {
            bool isOk = article.UpdateArticle(ArticleInfo);
            if (isOk)
                return Json(new { IsOk = true, BlogWorksID = ArticleInfo.BlogWorksID });
            return Json(new { IsOk = false, BlogWorksID = ArticleInfo.BlogWorksID });
        }

        protected override void Dispose(bool disposing)
        {
            this.article.Dispose();
            base.Dispose(disposing);
        }
    }
}