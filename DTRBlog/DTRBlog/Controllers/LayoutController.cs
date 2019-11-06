using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRBlog.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        public ActionResult Layout(string currentUrl)
        {
            ViewBag.currentUrl = currentUrl;
            return View();
        }
    }
}