using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Model
{
    public class BlogUser
    {
        public string BlogID { get; set; }

        public string BlogPwd { get; set; }

        public int WorksCount { get; set; }

        public int? BorwseCount { get; set; }

        public string Nikename { get; set; }

        public bool FrozenState { get; set; }

        public DateTime? StartFrozenDate { get; set; }

        public DateTime? EndFrozenDate { get; set; }

        public string FrozenReason { get; set; }

        public int? UnHonour { get; set; }

        public string UserPath { get; set; }

        public int? SupportCount { get; set; }

       
        public int FollowCount { get; set; }

        /// <summary>
        /// 粉丝数量
        /// </summary>

        public int FansCount { get; set; }

        public int? CollectionCount { get; set; }

        public int? CommentCount { get; set; }

        public bool? Sex { get; set; }

        public DateTime? RegisterTime { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public string Area { get; set; }

        public string Industry { get; set; }

        public string Position { get; set; }

        public string BlogDese { get; set; }
    }
}