using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTR.DAL;

namespace DTRBlog.Models
{
    public class HotBlogUserViewModel
    {
        public BlogUser BlogUser { get; set; }
        public int? FollowCount { get; set; }
        public int? SupportCount { get; set; }
    }
}