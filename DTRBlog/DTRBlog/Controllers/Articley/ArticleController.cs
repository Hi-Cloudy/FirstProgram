using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTRBlog.Models.Articley.ArticleBLL;
using DTRBlog.Models.Articley.ViewModel;
using DTRBlog.Public;

namespace DTRBlog.Controllers.Articley
{
    [AuthorityFilter("~/Home/Login")]
    public class ArticleyController : Controller
    {
        public ArticleyController()
        {
            this.article = new ArticleManage();
        }

        private ArticleManage article = null;

        // GET: Article
        public ActionResult ArticleManage(string BlogID)
        {
            BlogID = CheckLog(BlogID);
            this.ViewData["BlogID"] = BlogID;
            ViewData["ArticleManage"] = "CurentPage";
            ViewData["ArticleDraft"] = "";
            ViewData["ArticleWaitTest"] = "";
            SingleBlogTypeCollection singBlogType = new SingleBlogTypeCollection(article.GetBlogType(),article.GetSingType(BlogID));
            return View(singBlogType);
        }

        public ActionResult ArticleDraft(string BlogID)
        {
            BlogID = CheckLog(BlogID);
            this.ViewData["BlogID"] = BlogID;
            ViewData["ArticleManage"] = "";
            ViewData["ArticleDraft"] = "CurentPage";
            ViewData["ArticleWaitTest"] = "";
            return View();
        }

        public ActionResult ArticleWaitTest(string BlogID)
        {
            BlogID = CheckLog(BlogID);
            this.ViewData["BlogID"] = BlogID;
            ViewData["ArticleManage"] = "";
            ViewData["ArticleDraft"] = "";
            ViewData["ArticleWaitTest"] = "CurentPage";
            return View();
        }

        public ActionResult ArticleWrite(string BlogID, int? BlogWorksID = null)
        {
            BlogID = CheckLog(BlogID);
            ArticleWriterInfo writerInfo = article.GetArticleWriterInfo(BlogID,BlogWorksID);

            if (writerInfo.IsFrozen)
                return RedirectToAction("ArticleManage");

            if (writerInfo == null)
                return Content("你的账号存在异常");

            return View(writerInfo);
        }

        public string CheckLog(string BlogID)
        {
            BlogID = BlogID ?? Request.Cookies["BlogID"].Value;
            if (string.IsNullOrEmpty(BlogID))
                Response.Redirect("/Home/Login");
            return BlogID;
        }

        protected override void Dispose(bool disposing)
        {
            this.article.Dispose();
            base.Dispose(disposing);
        }
    }
}