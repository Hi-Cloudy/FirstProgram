using DTR.DAL;
using DTRBlog.IManager;
using DTRBlog.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace DTRBlog.Controllers
{
    [AuthorityFilter("~/Home/Login")]
    public class MsgController : Controller
    {
        private INewsManager newsManager = null;
        public MsgController(INewsManager newsManager)
        {
           this.newsManager = newsManager;
        }
       
        /// <summary>
        /// 评论
        /// </summary>
        /// <returns></returns>
        public ActionResult Comment(int pageIndex = 1, int pageSize = 7)//1
        {
            int totalCount = 0;

            ViewBag.NewType =1 ;

            var msg = newsManager.GetAllNews(1, Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.Notice>(msg, pageIndex, pageSize, totalCount));

        }
        /// <summary>
        /// 关注
        /// </summary>
        /// <returns></returns>
        public ActionResult Attention(int pageIndex = 1, int pageSize = 7)//2
        {
            int totalCount = 0;

            ViewBag.NewType = 2;

            var msg = newsManager.GetAllNews(2, Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.Notice>(msg, pageIndex, pageSize, totalCount));
        }
        /// <summary>
        /// 点赞
        /// </summary>
        /// <returns></returns>
        public ActionResult Like(int pageIndex = 1, int pageSize = 10)//3
        {
            int totalCount = 0;

            ViewBag.NewType = 3;
            var msg = newsManager.GetAllNews(3, Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.Notice>(msg, pageIndex, pageSize, totalCount));

        }
        /// <summary>
        /// 收藏
        /// </summary>
        /// <returns></returns>
        public ActionResult Collect(int pageIndex = 1, int pageSize = 10)//4
        {
            int totalCount = 0;

            ViewBag.NewType = 4;
            var msg = newsManager.GetAllNews(4, Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.Notice>(msg, pageIndex, pageSize, totalCount));

        }
        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult Replay(int pageIndex = 1, int pageSize = 7)//5
        {
            int totalCount = 0;

            ViewBag.NewType = 5;

            var msg = newsManager.GetAllNews(5, Session["BlogID"].ToString(), pageIndex, pageSize, out totalCount);

            return View(new PagedList<Model.Notice>(msg, pageIndex, pageSize, totalCount));

        }
  
        public ActionResult NewsCount(int NewsType)
        {
          int count=  newsManager.GetNewsCount(NewsType, Session["BlogID"].ToString());

            return PartialView(count);
        }
        public int GetAllNewsCount()
        {
            int count = newsManager.GetNewsCount(Session["BlogID"].ToString());
            return count;
        }
        public int FindCount(int TypeID)
        {
            int count = newsManager.GetNewsCount(TypeID, Session["BlogID"].ToString());

            return count;
        }
        public ActionResult SideMenu()
        {
            return PartialView();
        }
        public ActionResult CancelReadState(int Id,int TypeID)
        {
          bool isSuccess=  newsManager.CanelReadState(Id,TypeID);
          return isSuccess?
                Json(new { code = 1, message = "成功!",typeId=TypeID }, JsonRequestBehavior.AllowGet) :
                Json(new { code = 0, message = "失败!" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DelNews(int Id,int TypeID)
        {
            bool isSuccess = newsManager.DelNews(Id);
            return isSuccess ?
                  Json(new { code = 1, message = "成功!",typeId = TypeID }, JsonRequestBehavior.AllowGet) :
                  Json(new { code = 0, message = "失败!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelAll(int TypeID) {
            bool isSuccess = newsManager.DelAllNews(TypeID, Session["BlogID"].ToString());
            return isSuccess ?
                  Json(new { code = 1, message = "成功!", typeId = TypeID }, JsonRequestBehavior.AllowGet) :
                  Json(new { code = 0, message = "失败!" }, JsonRequestBehavior.AllowGet);
        }
    }
}