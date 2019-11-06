using DTR.DATAConfig;
using DTRBlog.IManager;
using DTRBlog.Model;
using DTRBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace DTRBlog.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleManager articleManager = null;
        private IBlogUserManager blogUserManager = null;

        public ArticleController(IArticleManager articleManager, IBlogUserManager blogUserManager)
        {
            this.articleManager = articleManager;
            this.blogUserManager = blogUserManager;
        }

        [HttpGet]
        public ActionResult Home(string BlogId, int pageIndex = 1, int pageSize = 7)
        {
            if (!blogUserManager.ExistUser(BlogId))
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<string>("未找到该用户")

                };
            }
            ViewBag.blogId = BlogId;
            int totalCount = 0;
            var data = articleManager.GetAllArticleByUser(pageIndex, pageSize, BlogId, Session["BlogID"] == null ? "" : Session["BlogID"].ToString(), out totalCount);

            
            return View(new PagedList<Model.ArticleList>(data, pageIndex, pageSize, totalCount));
        }

        [HttpGet]
        public ActionResult Details(string BlogId, int? id)
        {
            if (id == null)
                return RedirectToAction("Home", new { BlogId = BlogId });
            if (Session["BlogID"]!=null)
            {
                ViewBag.isSupported = articleManager.IsExistSupport(id.Value, Session["BlogID"].ToString());
                ViewBag.isCollection = articleManager.IsExistCollection(id.Value, Session["BlogID"].ToString());
            }
            //var article = articleManager.GetArticleById( BlogId,id.Value);Session["BlogID"].ToString()
            var article = articleManager.ShowArticle(BlogId, id.Value, Session["BlogID"]==null? "": Session["BlogID"].ToString());
            if (article==null)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<string>("文章不存在或已删除")

                };
            }
            return View(article);
        }
        [ChildActionOnly]
        public ActionResult Author(string BlogId,int type=1)
        {
            if (Session["BlogID"] != null)
            {
                ViewBag.ExistFlolow = blogUserManager.ExistFlolow(Session["BlogID"].ToString(), BlogId);
                ViewBag.IsBothFollor = blogUserManager.IsBothFollow(Session["BlogID"].ToString(), BlogId);
            }
            else
            {
                ViewBag.ExistFlolow = false;
                ViewBag.IsBothFollor = false;
            }
            if (type == 1)
                return PartialView("_Author", this.FindAuthor(BlogId));
            else
                return PartialView("Author", this.FindAuthor(BlogId));
        }

        public ActionResult Get(string blogId,string id, int pageIndex = 1, int pageSize = 10)
        {
            if (blogId == null)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<string>("未找到相关信息")

                };
            }
            ViewBag.blogId = blogId;
            int totalCount = 0;
            List<DTRBlog.Model.FollowListModel> model = null;
            if (id == "Fans")
            {
                Session["active1"] = "article";
                model = blogUserManager.GetFansList(blogId, pageIndex, pageSize, out totalCount);
            }
            else if(id == "Follow") {
                Session["active2"] = "article";
                model = blogUserManager.GetFollowList(blogId, pageIndex, pageSize, out totalCount);
            }
            return View("GetFans", new PagedList<Model.FollowListModel>(model, pageIndex, pageSize, totalCount));
        }


        public ActionResult GetCollection(string blogId, int pageIndex = 1, int pageSize = 10)
        {
            if (blogId == null)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<string>("未找到相关信息")

                };
            }
            ViewBag.blogId = blogId;

            int totalCount = 0;
           
            var model = blogUserManager.GetCollectionList(blogId, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.CollectionListMode>(model, pageIndex, pageSize, totalCount));
        }


        #region Method

        public DTRBlog.Model.BlogUser FindAuthor(string BlogId)
        {
            var author = blogUserManager.FindAuthor(BlogId);
            if (author == null)
                return null;
            return author;
        }
        public ActionResult NikeName(string BlogId)
        {
            return PartialView(this.FindAuthor(BlogId));
        }

        public ActionResult HeadImg(string BlogId)
        {
            return PartialView(this.FindAuthor(BlogId));
        }

        public string GetHeaderImg(string BlogId)
        {
            if (string.IsNullOrWhiteSpace(BlogId))
                return "";
            var img = blogUserManager.GetHeadImg(BlogId);
            string headimg = img.Replace(@"\", @"/");

            return headimg;
        }
        public string GetNickName(string BlogId)
        {
            if (string.IsNullOrWhiteSpace(BlogId))
                return "";
            var author = this.FindAuthor(BlogId);

            return author.Nikename;
        }
        public void Browse(int id,int blogId)
        {
            articleManager.UpdateArticleBrowse(id, blogId);
        }

        public int FanCount(string blogId)
        {
            return blogUserManager.FanCount(blogId);
        }
        public ActionResult AddFollow(string BlogId,string TargetBlogId)
        {

            bool isSuccess = blogUserManager.AddFlolow(BlogId, TargetBlogId);

            bool isBothFans = blogUserManager.IsBothFollow(BlogId, TargetBlogId);
            string text = isBothFans ? "互相关注" : "已关注";

            return isSuccess ? 
                Json(new { code = 1, message = "关注成功!", follow = text }, JsonRequestBehavior.AllowGet):
                Json(new { code = 0, message = "关注失败!" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CancelFollow(string BlogId, string TargetBlogId)
        {
            return blogUserManager.CancelFlolow(BlogId, TargetBlogId) ?
                   Json(new { code = 1, message = "取关成功!" }, JsonRequestBehavior.AllowGet) :
                   Json(new { code = 0, message = "取关失败!" }, JsonRequestBehavior.AllowGet);
        }

        public string JsonStr()
        {
            return "[{}]";
        }
        [ChildActionOnly]
        public ActionResult ReplyCommentList(int ParentCommentID,string authorId)
        {
            ViewBag.blogAuthorId = authorId;
            var model = articleManager.ShowSubComment(ParentCommentID);
            return PartialView(model);
        }
        public ActionResult AddComment(string BlogId,int BlogArticleId,string Content,string AuthorArticleId)
        {
            int commentId = 0;
            return articleManager.AddComment(BlogId, BlogArticleId, Content,out commentId, AuthorArticleId) ?
                Json(new { code = 1, message = "评论成功!", commentId = commentId }, JsonRequestBehavior.AllowGet) :
                Json(new { code = 0, message = "评论失败!" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddGood(int blogArticleId, string fromBlogId, string targetBlogUser)
        {
            var isSuccess = articleManager.GoodAdd(blogArticleId, fromBlogId, targetBlogUser);
            return isSuccess ?
                Json(new { code = 1, message = "点赞成功!" }, JsonRequestBehavior.AllowGet) :
                Json(new { code = 0, message = "点赞失败!" }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CancelGood(int blogArticleId, string fromBlogId)
        {
            var isSuccess = articleManager.BadAdd(blogArticleId, fromBlogId);
            return isSuccess ?
                Json(new { code = 1, message = "取赞成功!" }, JsonRequestBehavior.AllowGet):
                Json(new { code = 0, message = "取赞失败!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCollection(int blogArticleId, string fromBlogId, string targetBlogUser)
        {
            var isSuccess = articleManager.AddCollection(blogArticleId, fromBlogId, targetBlogUser);
            return isSuccess ?
                Json(new { code = 1, message = "收藏成功!" }, JsonRequestBehavior.AllowGet) :
                Json(new { code = 0, message = "收藏失败!" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CancelCollection(int blogArticleId, string fromBlogId)
        {
            var isSuccess = articleManager.CancelCollection(blogArticleId, fromBlogId);
            return isSuccess ?
                Json(new { code = 1, message = "取消收藏成功!" }, JsonRequestBehavior.AllowGet) :
                Json(new { code = 0, message = "取消收藏失败!" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReplyComment(string BlogId, int ArticleId , string ReplyContent, int ReplyId, string AuthorArticleId)
        {
            int commentId = 0;
            var isSuccess = articleManager.ReplyComment(BlogId, ArticleId, ReplyContent, ReplyId, out commentId, AuthorArticleId);
            return isSuccess ?
                Json(new { code = 1, message = "回复成功!",commentId= commentId }, JsonRequestBehavior.AllowGet) :
                Json(new { code = 0, message = "回复失败!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveComment(int commentId)
        {
            var isSuccess = articleManager.RemoveComment(commentId);
            return isSuccess ?
               Json(new { code = 1, message = "删除成功!" }, JsonRequestBehavior.AllowGet) :
               Json(new { code = 0, message = "删除失败!" }, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}