using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTRBlog.Models.Articley.ArticleBLL;
using DTRBlog.Models.Articley.ViewModel;

namespace DTRBlog.Controllers.Article
{
    public class ArticleWriteController : Controller
    {
        public ArticleWriteController()
        {
            this.article = new ArticleManage();
        }

        private ArticleManage article = null;


        public JsonResult GetBlogType(int? BlogWorksID)
        {
            return Json(new {
                BlogType  = article.GetBlogType(),
                BlogTypeID = article.GetBlogWorksUI(BlogWorksID)?.BlogTypeID
            },JsonRequestBehavior.AllowGet);
        }
  
        public JsonResult GetSingleBlogType(string BlogID, int? BlogWorksID)
        {
            return Json(new
            {
                SingleType = article.GetSingType(BlogID),
                SingleTypeID = article.GetBlogWorksUI(BlogWorksID)?.SingBlogTypeID
            }, JsonRequestBehavior.AllowGet);
        }
     
        public JsonResult GetArticleInfo(int? BlogWorksID)
        {
            return Json(new {
                Article = article.GetArticle(BlogWorksID)
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpLoadTitleImg(HttpPostedFileBase titleImg,string WorksPath)
        {
            ArticleUpLoadResource articleUpLoadResource = new ArticleUpLoadResource(WorksPath);
            return Json(new {
                IsOk = articleUpLoadResource.UpLoadWorksFace(titleImg)
            }) ;
        }
        [HttpPost]
        public JsonResult SaveArticle(SaveArticleInfo saveArticle)
        {
            bool isOk = article.SaveArticle(saveArticle);
            article.Dispose();
            if (isOk)
                return Json(new { IsOk = true });
            return Json(new { IsOk = false });
        }
        public JsonResult GetAllResource(string WorksPath)
        {
            ArticleUpLoadResource articleUp = new ArticleUpLoadResource(WorksPath);
            return Json(new {
                Files = articleUp.GetAllResource(),
                FileUrl = WrapPath.WorksResourceUrl(WorksPath),
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpLoadFileList(string WorksPath, IEnumerable<HttpPostedFileBase> Files)
        {
            ArticleUpLoadResource articleUpLoad = new ArticleUpLoadResource(WorksPath);
            if (articleUpLoad.SaveFiles(Files))
                return Json(new { IsOk = true });
            return Json(new { IsOk = false });
        }
        [HttpPost]
        public JsonResult RemoveResource(string fileName, string WorksPath)
        {
            ArticleUpLoadResource articleUpLoad = new ArticleUpLoadResource(WorksPath);
            if (articleUpLoad.RemoveFiles(fileName))
                return Json(new { IsOk = true , fileName = fileName});
            return Json(new { IsOk = false });

        }

        protected override void Dispose(bool disposing)
        {
            this.article.Dispose();
            base.Dispose(disposing);
        }
    }
}