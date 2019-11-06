using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Model
{
   public class ArticleList
    {
        public DTR.DAL.BlogUser BlogUser { get; set; }
        public DTR.DAL.BlogWorks Article { get; set; }
        public string TitleImg { get; set; }

    }

}
