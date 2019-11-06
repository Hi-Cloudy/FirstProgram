using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Model
{
    public class FollowListModel
    {
        public int ID { get; set; }
        public string BlogID { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        public bool IsExistFollow { get; set; }
        public bool IsBothFollow { get; set; }
    }
}
