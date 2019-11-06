using DTRBlog.IManager;
using DTRBlog.Model;
using DTRBlog.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace DTRBlog.Controllers
{
    [AuthorityFilter("~/Home/Index")]
    public class UserController : Controller
    {
        private readonly ICategoryManager categoryManager = null;
        private readonly IBlogUserManager blogUserManager = null;
        public UserController(ICategoryManager categoryManager, IBlogUserManager blogUserManager)
        {
            this.categoryManager = categoryManager;
            this.blogUserManager = blogUserManager;
        }

        #region ActionName
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(int pageIndex = 1, int pageSize = 9)
        {
            int totalCount = 0;
            var cate = categoryManager.GetCategories(Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);
            return View(new PagedList<Model.Category>(cate, pageIndex, pageSize, totalCount));
        }

        public ActionResult ArticleCategory_List(int id, int pageIndex = 1, int pageSize = 9)
        {
            if (categoryManager.GetCategoryName(id) == string.Empty)
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<string>("未找到该分类信息")
                };
            ViewBag.categoryName = categoryManager.GetCategoryName(id);
            ViewBag.categoryId = id;
            int totalCount = 0;
            var cate = categoryManager.GetArticleInfoByCategory(id, Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.BlogWorks>(cate, pageIndex, pageSize, totalCount));
        }

        public ActionResult Follow(string blogId, int pageIndex = 1, int pageSize = 9)
        {
            int totalCount = 0;
            var id = blogId == null ? Session["BlogID"].ToString() : blogId;

            var model = blogUserManager.GetFollowList(id, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.FollowListModel>(model, pageIndex, pageSize, totalCount));
        }
        public ActionResult Fans(string blogId, int pageIndex = 1, int pageSize = 9)
        {
            int totalCount = 0;
            var id = blogId == null ? Session["BlogID"].ToString() : blogId;

            var model = blogUserManager.GetFansList(id, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.FollowListModel>(model, pageIndex, pageSize, totalCount));
        }
        public ActionResult Collection(string blogId, int pageIndex = 1, int pageSize = 9)
        {
            int totalCount = 0;
            var id = blogId == null ? Session["BlogID"].ToString() : blogId;

            var model = blogUserManager.GetCollectionList(id, pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.CollectionListMode>(model, pageIndex, pageSize, totalCount));

        }
        #endregion


        #region Menthod-API
        public ActionResult GetHeadImg()
        {
            var author = blogUserManager.FindAuthor(Session["BlogID"].ToString());
            if (author == null)
                throw new Exception("");
            string query = "?query=" + new Random().Next(1, 10000000).ToString();

            return Content($"<img style='width: 4.99rem; height: 4.99rem; border - radius: 50 % ' src='{author.UserPath}{query}'/>");
        }
        public ActionResult AddCategory(string categoryName)
        {
            var isExist = categoryManager.IsExistCategory(Session["BlogID"].ToString(), categoryName);
            Category cate = null;
            if (!isExist)
            {
                cate = categoryManager.AddCategory(Session["BlogID"].ToString(), categoryName);
                return cate != null ? Json(new { code = 1, message = "添加成功", cate = cate }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "失败" }, JsonRequestBehavior.AllowGet); ;
            }
            return Json(new { code = 0, message = "分类名已存在" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCategory(int categoryID, string categoryName)
        {
            var isExist = categoryManager.IsExistCategory(Session["BlogID"].ToString(), categoryName);
            if (isExist) return Json(new { code = 0, message = "分类名已存在" }, JsonRequestBehavior.AllowGet);
            var cate = categoryManager.ModifyCategory(categoryID, categoryName);
            return cate ? Json(new { code = 1, message = "修改成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "失败" }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DelCategory(int categoryID)
        {
            var cate = categoryManager.DelCategory(categoryID);
            return cate ? Json(new { code = 1, message = "删除成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "失败" }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddCategoryArticle(int cateId, int pageIndex = 1, int pageSize = 9)
        {
            int totalCount = 0;

            var model = categoryManager.GetArticleByUser(Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);
            ViewBag.categoryId = cateId;
            return View(new PagedList<Model.BlogWorks>(model, pageIndex, pageSize, totalCount));
        }
        public ActionResult AddArticleToCate(string strBulder, int cateId)
        {
            var result = categoryManager.AddArticleToCate(strBulder, cateId);
            return result ? Json(new { code = 1, message = "成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "失败" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateArticleToCate(int articleId)
        {
            var result = categoryManager.ModifyArticleToCate(articleId);
            return result ? Json(new { code = 1, message = "移除成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "移除失败" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MultUpdateCateOfArticle(string articleId)
        {
            var result = categoryManager.ModifyManyArticleToCate(articleId);
            return result ? Json(new { code = 1, message = "移除成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "移除失败" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddFollow(string targetBlogId)
        {
            var result = blogUserManager.AddFlolow(Session["BlogID"].ToString(), targetBlogId);

            bool isBothFans = blogUserManager.IsBothFollow(Session["BlogID"].ToString(), targetBlogId);
            string text = isBothFans ? "相互关注" : "已关注";
            return result ? Json(new { code = 1, message = "关注成功", follow = text }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "关注失败" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelFollow(string targetBlogId)
        {
            var result = blogUserManager.CancelFlolow(Session["BlogID"].ToString(), targetBlogId);
            return result ? Json(new { code = 1, message = "取关成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "取关失败" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveCollection(int collectionId)
        {
            var result = blogUserManager.RemoveCollection(collectionId);
            return result ? Json(new { code = 1, message = "成功" }, JsonRequestBehavior.AllowGet) : Json(new { code = 0, message = "失败" }, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}