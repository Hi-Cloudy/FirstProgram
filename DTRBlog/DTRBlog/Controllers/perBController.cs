using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTR;
using DTR.DATAConfig;
using DTR.DAL;
using System.IO;
using System.Drawing;
using DTRBlog.Public;

namespace DTRBlog.Controllers
{
    public class perBController : Controller
    {
        public DTREF DbContext = new DTREF();
        [AuthorityFilter("~/Home/Index")]
        public ActionResult PB()
        {
            BlogUser blogUser = Session["UserModel"] as BlogUser;
            var re = DbContext.BlogUser.Where(b => b.BlogID == blogUser.BlogID).FirstOrDefault();
            ViewBag.Follow = DbContext.Follow.Count(f => f.BlogID == re.BlogID);
            ViewBag.Fans = DbContext.Follow.Count(f => f.TargetBlogID == re.BlogID);


            return View(re);
        }
        public ActionResult Upload(string img)
        {

            BlogUser loginUser = Session["UserModel"] as BlogUser;

            string userPath = string.Empty;

            if (loginUser != null)
            {
                userPath = loginUser.UserPath; 
            }

            string headerPath = Path.Combine(DATAPathConfigs.DTRDataConfigFullPath, loginUser.UserPath, DATAPathConfigs.BlogUserHeaderImg);

            
            string text = img;

            if (System.IO.File.Exists(headerPath))
            {
                System.IO.File.Delete(headerPath);
                Base64ToImg(text.Split(',')[1]).Save(headerPath);
                return Json(new { state = 0, msg = "修改成功！" }, JsonRequestBehavior.AllowGet);
            }
            else {
                Base64ToImg(text.Split(',')[1]).Save(headerPath); 
                return Json(new { state = 0, msg = "修改成功！" }, JsonRequestBehavior.AllowGet);
            }
        }
    
        private Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }

        public ActionResult GetUserHead(string id)
        {
            BlogUser loginUser = Session["UserModel"] as BlogUser;
            string headerPath = Path.Combine(DATAPathConfigs.DTRDataConfigFullPath, loginUser.UserPath, DATAPathConfigs.BlogUserHeaderImg);

            if (!System.IO.File.Exists(headerPath))
            {
                headerPath = Path.Combine("/", @"DTRDefaultResource\Default\Pictrue\head.jpg");
            }
            string rootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            string url = (headerPath).Replace(@"\", @"/"); ; 

            return File(url, "image/jpeg");

        }

        public ActionResult ChangePwd(string oldePass, string newPass, string newPassAgin)
        {
            if (string.IsNullOrEmpty(oldePass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(newPassAgin))
            {
                return Json(new { state = 1, msg = "旧密码 新密码 和 重复密码必填！" });
            }

            if (newPass != newPassAgin)
            {
                return Json(new { state = 1, msg = "新密码 和 重复密码不一致！" });
            }
            BlogUser sessionUser = Session["UserModel"] as BlogUser;

            if (sessionUser == null)
            {
                return Json(new { state = 1, msg = "登陆状态失效" });
            }

            var loginUser = DbContext.BlogUser.FirstOrDefault(u => u.BlogID == sessionUser.BlogID);

            if (loginUser == null)
            {
                return Json(new { state = 1, msg = "当前用户不存在" });
            }

            if (loginUser.BlogPwd != oldePass)
            {
                return Json(new { state = 1, msg = "旧密码错误" });
            }

            loginUser.BlogPwd = newPass;
            DbContext.SaveChanges();

            return Json(new { state = 0, msg = "修改完成" });
        }
        public ActionResult ChangeInfo(DTR.DAL.BlogUser blogUser)
        {
            try
            {
                BlogUser findBlogUser = DbContext.BlogUser.Where(b => b.BlogID == blogUser.BlogID).FirstOrDefault();

                BlogWorks firstBlogComment = DbContext.BlogWorks.FirstOrDefault();

                findBlogUser.Nikename = blogUser.Nikename;
                findBlogUser.Sex = blogUser.Sex;
                findBlogUser.Area = blogUser.Area;
                findBlogUser.Email = blogUser.Email;
                findBlogUser.Birthday = blogUser.Birthday;
                findBlogUser.Industry = blogUser.Industry;
                findBlogUser.Position = blogUser.Position;
                findBlogUser.BlogDese = blogUser.BlogDese;

                DbContext.SaveChanges();

                return Json(new { state = 0, msg = "修改成功", blogUser = blogUser }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { state = 1, msg = e.Message }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}