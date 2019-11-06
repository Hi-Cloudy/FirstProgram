using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Public
{
    public class PublicService
    {
        /// <summary>
        /// 在登录或注册用户时保存的原页面地址
        /// </summary>
        /// <param name="SendUrl">原页面地址</param>
        public void SetBackLink(string ReturnUrl)
        {
            if (ReturnUrl == null) { return; }
            string SendUrl = System.Text.Encoding.Default.GetString(Convert.FromBase64String(ReturnUrl));
            if (SendUrl != null && SendUrl != "" && SendUrl != "\\" && SendUrl != "/"&& SendUrl!= "home/index" && SendUrl != "/home/index")
            {
                HttpContext.Current.Session["GoBack"] = SendUrl;
            }
        }
        /// <summary>
        /// 登录或注册成功后移除原页面地址
        /// </summary>
        public void RemoveBackLink()
        {
            HttpContext.Current.Session.Remove("GoBack");
        }
        /// <summary>
        /// 返回原页面地址
        /// </summary>
        public string GetBackLink
        {
            get { return HttpContext.Current.Session["GoBack"] as string; }
        }
        /// <summary>
        /// 返回是否为已登录用户
        /// </summary>
        public bool IsLogin
        {
            get { return HttpContext.Current.Session["BlogId"] != null; }
        }
    }
}