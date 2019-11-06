using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Model
{
    public class BlogWorks
    {
        public int BlogWorksID { get; set; }

        public string BlogID { get; set; }

        public string  NikeName { get; set; }
        public int? StateID { get; set; }

        public string BlogTypeName { get; set; }

        public int? SingleBlogTypeID { get; set; }
        public string SingleBlogTypeName { get; set; }
        public string Title { get; set; }
        public string TitleImg { get; set; }
        public string Introduc { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool TopState { get; set; }

        public bool? OpenState { get; set; }

        public int? CharLength { get; set; }

        public int? SupportCount { get; set; }

        public int? FollowCount { get; set; }

        public int? CommentCount { get; set; }

        public int? BorwseCount { get; set; }

        public string WorksPath { get; set; }
    }
}