using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Model
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string BlogID { get; set; }
        public string CategoryName { get; set; }
        public int ArticleCount { get; set; }
    }
}
