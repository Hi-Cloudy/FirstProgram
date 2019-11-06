using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTR;
using DTR.DATAConfig;
using DTR.DAL;

namespace DTRBlog.Controllers
{
    public class PersonBlogController : Controller
    {
        public DTREF DbContext = new DTREF();



        public ActionResult shoucang()
        {
            return View();
        }

        public ActionResult guangzhu()
        {
            return View();
        }
        public ActionResult fensi()
        {
            return View();
        }

        #region 个人信息

        public ActionResult personInfo()
        {
            BlogUser blogUser = Session["UserModel"] as BlogUser;
            var re = DbContext.BlogUser.Where(b => b.BlogID == blogUser.BlogID).FirstOrDefault();

           
            return View(re);
        }

        public ActionResult ChangeInfo(DTR.DAL.BlogUser blogUser)
        {
            var findBlogUser = DbContext.BlogUser.Where(b => b.BlogID == blogUser.BlogID).FirstOrDefault();

            findBlogUser.Nikename = blogUser.Nikename;
            findBlogUser.Sex = blogUser.Sex;
            findBlogUser.Area = blogUser.Area;
            findBlogUser.Birthday = blogUser.Birthday;
            findBlogUser.Industry = blogUser.Industry;
            findBlogUser.Position = blogUser.Position;
            findBlogUser.BlogDese = blogUser.BlogDese;
            DbContext.SaveChanges();

            return RedirectToAction("personInfo");
        }
        #endregion
    }
}