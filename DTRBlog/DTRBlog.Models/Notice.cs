using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTR.DAL;
namespace DTRBlog.Model
{
   public class Notice
    {
        public int  NewsID { get; set; }
        public string NewsContent { get; set; }
        public bool ReadState { get; set; }
        public DateTime? NewsDate { get; set; }
        public string URL { get; set; }
        public NewsType NewsType { get; set; }
        public Admin  Admin { get; set; }
        public DTR.DAL.BlogUser FromUser { get; set; }
        public DTR.DAL.BlogUser ToUser { get; set; }
        public string ArticleTitle { get; set; }


    }
}
