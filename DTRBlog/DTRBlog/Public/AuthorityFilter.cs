using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRBlog.Public
{
    public class AuthorityFilterAttribute: AuthorizeAttribute
    {
        /// <summary>
        /// 未登录时返还的地址
        /// </summary>
        private string _LoginPath = "";
        public AuthorityFilterAttribute()
        {
            this._LoginPath = "~/Account/Login";
        }

        public AuthorityFilterAttribute(string loginPath= "~/Account/Login")
        {
            this._LoginPath = loginPath;
        }

        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            else if(filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            var sessionBlog = filterContext.HttpContext.Session["BlogId"];
            var cookieBlog = filterContext.HttpContext.Request.Cookies.Get("BlogId");
           
            if (sessionBlog == null /*|| cookieBlog == null*/)
            {
                string oldUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                string returnUrl = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(oldUrl));
                new PublicService().SetBackLink(returnUrl);
                filterContext.Result = new RedirectResult(this._LoginPath);
            }
        }
    }
}