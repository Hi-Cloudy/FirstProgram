using DTR.DAL;
using DTRBlog.IManager;
using DTRBlog.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTR.DATAConfig;
using System.IO;
using DTRBlog.Models;
using Webdiyer.WebControls.Mvc;

namespace DTRBlog.Controllers
{
    public class HomeController : Controller
    {
        private IArticleManager articleManager = null;
        private IBlogUserManager blogUserManager = null;
        public HomeController(IArticleManager articleManager, IBlogUserManager blogUserManager)
        {
            this.articleManager = articleManager;
            this.blogUserManager = blogUserManager;
            this.DbContext = new DTREF();
        }
        public DTREF DbContext;

        #region 首页

        public ActionResult Index()
        {
            var toutiao1 = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.BorwseCount).Take(7).ToList();
            ViewData["key"] = toutiao1;
           
            var type = DbContext.BlogType;
            ViewData["key2"] = type.ToList();
        
            var data = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.PublishDate).Take(18).ToList();
            ViewData["key3"] = data;
            IEnumerable<HotBlogUserViewModel> bolgUser = (from user in DbContext.BlogUser.Where(u => !u.FrozenState)
                                                          where user.FrozenState == false
                                                          select new
                                                          {
                                                              BlogUser = user,
                                                              FollowCount = user.Follow1.Count,
                                                              SupportCount = user.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).Sum(w => w.Support.Count())
                                                          }
                                                          into orderUser orderby orderUser.FollowCount descending, orderUser.SupportCount descending
                                                          select new HotBlogUserViewModel
                                                          {
                                                              BlogUser = orderUser.BlogUser,
                                                              FollowCount = orderUser.FollowCount,
                                                              SupportCount = orderUser.SupportCount
                                                          }).Take(9).ToList();
            ViewData["key4"] = bolgUser;

            ViewBag.IsLogin = Session["UserModel"] != null;
            ViewData["BlogTypeInfo"] = GetBlogTypeInfo() ?? Enumerable.Empty<HomeIndexBlogInfoViewModel>();

   
            var hotblog= (DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.BlogComment.Count)).Take(8).ToList();
            ViewData["hotblog"] = hotblog ;
            return View();
        }

        private IEnumerable<HomeIndexBlogInfoViewModel> GetBlogTypeInfo()
        {
            IEnumerable<HomeIndexBlogInfoViewModel> blogTypeInfo = (from type in this.DbContext.BlogType
                                                                    join blog in this.DbContext.BlogWorks.Where(b=> b.StateID == 3 && b.OpenState == true)
                                                                    on type.BlogTypeID equals blog.BlogTypeID into blogDetails
                                                                    select new HomeIndexBlogInfoViewModel
                                                                    {
                                                                        BlogType = type,
                                                                        Works = blogDetails.OrderByDescending(w => w.BorwseCount).Take(7)
                                                                    }).ToList();
            return blogTypeInfo;
        }


        #region 登录、注册

        
        public ActionResult Login(string currentUrl)
        {
            PublicService service = new PublicService();
            service.SetBackLink(currentUrl);
            return View();
        }

        
        public ActionResult Register(string currentUrl)
        {
            PublicService service = new PublicService();
            service.SetBackLink(currentUrl);
            return View();
        }

        
        [HttpPost]
        public ActionResult LoginResult(string BlogID, string userPwd, string code)
        {
            if (string.IsNullOrEmpty(BlogID) || string.IsNullOrEmpty(userPwd) || string.IsNullOrEmpty(code))
            {
                return Json(new { state = 1, msg = "用户名、密码和验证码不能为空！" }, JsonRequestBehavior.AllowGet);
            }

            //判断验证码正确否？
            string realCode = (Session["SecurityCode"] ?? "").ToString();
            Session["SecurityCode"] = null;

            if (string.IsNullOrEmpty(realCode) || realCode.ToUpper() != code.ToUpper())
            {
                return Json(new { state = 1, msg = "验证码错误！" }, JsonRequestBehavior.AllowGet);
            }


            var re = DbContext.BlogUser.Where(b => b.BlogID == BlogID && b.BlogPwd == userPwd);

            if (!re.Any())
            {
                return Json(new { state = 1, msg = "账号 或 密码 错误！" }, JsonRequestBehavior.AllowGet);
            }

            Session["UserModel"] = re.FirstOrDefault();
            Session["BlogID"] = BlogID;
            Session["NickName"] = re.FirstOrDefault().Nikename;
            Response.Cookies["BlogID"].Value = re.FirstOrDefault().BlogID;
            //Response.Cookies["BlogID"].Expires = DateTime.Now.AddDays(1);


            PublicService service = new PublicService();
            string retureUrl = "";

            if (service.GetBackLink != null)
                retureUrl = service.GetBackLink;
            else
                retureUrl = "/home/index";

            service.RemoveBackLink();

            return Json(new { state = 0, msg = "登陆成功，准备跳转...", url = $"{retureUrl}" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void SmSRegisterResult(string Phone)
        {
            return;//取消验证码
            if (string.IsNullOrEmpty(Phone)) return;

            SmS s = new SmS();

            Session["SecurityCode"] = s.Send(Phone);


        }
        
        public ActionResult RegisterResult(string BlogID, string userName, string userPwd, string Email, string code)
        {

            //关注    Follow ==> BlogID
            //粉丝    Follow1 ==> TargetBlogID
            if (!(DbContext.BlogUser.Where(u => u.BlogID == BlogID).Count() == 0))
            {
                return Json(new
                {
                    IsOk = false,
                    ErrorMsg = "此账号已存在，请更换一个账号号码！"
                });
            }
            else
            {
                //判断验证码正确否？
                //string realCode = (Session["SecurityCode"] ?? "").ToString();
                //Session["SecurityCode"] = null;
                //if (realCode != code || realCode == null)
                //{
                //    return Json(new
                //    {
                //        yanzm = false,
                //        yanzmMsg = "验证码错误！"
                //    });
                //}
                BlogUser user = new BlogUser();
                string msg = null;
                bool b = false;
                var result = Create();
                if (userPwd != null && result.Item1 )
                {
                    user.BlogID = BlogID;
                    user.Nikename = userName;
                    user.BlogPwd = userPwd;
                    user.RegisterTime = DateTime.Now;
                    user.FrozenState = false;
                    user.UserPath = result.Item2;

                    DbContext.BlogUser.Add(user);
                    bool isOk = DbContext.SaveChanges() > 0;
                    if (isOk)
                    {
                        msg = "注册成功！";
                        b = true;
                    }
                    else
                        msg = "注册失败！";
                    b = false;
                }
                else
                {
                    msg = "注册失败！";
                    b = false;
                }
                JsonResult ajaxres = new JsonResult();
                ajaxres.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                ajaxres.Data = new
                {
                    msg = msg,
                    b = b
                };
                return ajaxres;
            }
        }

        #endregion

        #region 退出
        public ActionResult SingOut()
        {
            Session.Clear();
            HttpCookie cookie = Request.Cookies["BlogID"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.AppendCookie(cookie);
            return RedirectToAction("Index");
        }
        #endregion

        #region 验证码

        
        public ActionResult SecurityCode()
        {
            //生成随机字符串
            string realCode = DTRBlog.Public.SecurityCode.CreateRandomCode(5);

            //储存正确验证码
            Session["SecurityCode"] = realCode;

            //创建打乱图片
            return File(DTRBlog.Public.SecurityCode.CreateValidateGraphic(realCode), "image/Jpeg");
        }

        #endregion

        #region 下拉
        public ActionResult showType()
        {
            var type = DbContext.BlogType.ToList();

            return PartialView(type);
        }

        #endregion

        #region 分类
        public ActionResult BlogInfo(int? id, int pageIndex = 1, int pageSize = 10)
        {
           
            var toutiao1 = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.Support.Count).Take(8).ToList();
            ViewData["key"] = toutiao1;

           
            var data = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderBy(u => u.PublishDate).Take(7).ToList();
            ViewData["key3"] = data;
            
            ViewData["key4"] = data;
            var ulike = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.PublishDate).Take(7).ToList();
            
            ViewBag.IsLogin = true;
            IQueryable<BlogWorks> temp = null;
            if (!(id == null))
            {
                temp = DbContext.BlogWorks.Include("BlogComment").Include("BlogType").Where(u => u.BlogTypeID == id && u.StateID == 3 && u.OpenState == true);
            }
            else
            {
                temp = DbContext.BlogWorks.Include("BlogComment").Include("BlogType").Where(u => u.StateID == 3 && u.OpenState == true);
            }
            var list = temp.OrderByDescending(u => u.BorwseCount).
                ThenByDescending(u => u.Support.Count()).ThenByDescending(u => u.PublishDate).
                Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(s => new SearchArticleViewModel
                {
                    Article = s,
                    Title = s.Title,
                    SubTitle = s.Introduc
                }).ToList();
           return View( new PagedList<SearchArticleViewModel>(list, pageIndex, pageSize, temp.Count()));
        }
        #endregion

        public ActionResult Seach(string keyword="", int pageIndex = 1, int pageSize = 10)
        {
            var toutiao1 = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.Support.Count).Take(8).ToList();
            ViewData["key"] = toutiao1;

           
            var data = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderBy(u => u.PublishDate).Take(7).ToList();
            ViewData["key3"] = data;
            
            
            ViewData["key4"] = data;
            var ulike = DbContext.BlogWorks.Where(b => b.StateID == 3 && b.OpenState == true).OrderByDescending(u => u.PublishDate).Take(7).ToList();

            IQueryable<BlogWorks> temp = null;
            if (keyword.Length==0 && string.IsNullOrWhiteSpace(keyword))
            {
                temp = DbContext.BlogWorks
                .Where(t => t.StateID == 3 && t.OpenState == true);
            }
            else {
                temp = DbContext.BlogWorks
                .Where(t => t.StateID == 3 && t.OpenState == true && t.Title.ToLower().Contains(keyword.ToLower()) || t.Introduc.ToLower().Contains(keyword.ToLower()));
            }

            var list = temp.OrderByDescending(u => u.BorwseCount).
              ThenByDescending(u => u.Support.Count()).ThenByDescending(u => u.PublishDate).
              Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(s=> new SearchArticleViewModel {
                  Article=s,
                  Title= keyword.Length==0 ? s.Title : s.Title?.Replace(keyword, $" <span style='color:red;display:inline;'>{keyword}</span>"),
                  SubTitle= keyword.Length == 0 ? s.Introduc :  s.Introduc?.Replace(keyword, $" <span style='color:red;display:inline;'>{keyword}</span>")
              });
            return View("BlogInfo", new PagedList<SearchArticleViewModel>(list, pageIndex, pageSize, temp.Count()));
        }

        public ActionResult stype(int? id)
        {
            IQueryable<BlogWorks> temp = null;
            if (!(id == null))
            {
                temp = DbContext.BlogWorks.Include("BlogComment").Include("BlogType").Where(u => u.BlogTypeID == id && u.OpenState == true);
            }

            var list = temp.OrderByDescending(u => u.BorwseCount).
                ThenByDescending(u => u.Support.Count()).ThenByDescending(u => u.PublishDate).ToList();
            ViewData["key3"] = list.ToList();
            return RedirectToAction("index", "home");
        }

        protected override void Dispose(bool disposing)
        {
            this.DbContext.Dispose();
            base.Dispose(disposing);
        }
        
        public string Json()
        {
            return "[{}]";
        }
        private Tuple<bool, string> Create()
        {
            bool isOk = false;
            string UserPath = DATAPathConfigs.BlogUserComponentPath + Guid.NewGuid().ToString() + "\\";
            string UserFullPath = DATAPathConfigs.DTRDataConfigFullPath + UserPath;

            while (Directory.Exists(UserFullPath))//是否已经存在此路径
            {
                UserPath = DATAPathConfigs.BlogUserComponentPath + Guid.NewGuid().ToString() + "\\";
                UserFullPath = DATAPathConfigs.DTRDataConfigFullPath + UserPath;
            }

            try
            {
                DirectoryInfo paren = Directory.CreateDirectory(UserFullPath);//当前用户的资源文件夹
                paren.CreateSubdirectory(DATAPathConfigs.BlogUser);//当前用户的文章集合
                paren.CreateSubdirectory(DATAPathConfigs.PublicResource);//当前用户的公共资源

                //将默认的用户头像copy至此用户文件夹下
                string headerDefault = DATAPathConfigs.DTRDataConfigFullPath + DATAPathConfigs.DTRDataDefaultResourceComponentPath + DATAPathConfigs.BlogUserHeaderDefault;//默认的用户头像
                if (!System.IO.File.Exists(headerDefault))
                    throw new Exception("抱歉，没有配置好默认的用户头像。");
                System.IO.File.Copy(headerDefault, UserFullPath + "\\" + DATAPathConfigs.BlogUserHeaderImg);

                isOk = true;
            }
            catch (Exception)
            {
                isOk = false;
            }
            return new Tuple<bool, string>(isOk, UserPath);
        }

    }
}
#endregion