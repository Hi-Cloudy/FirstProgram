using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTR.DAL;
namespace DTRBlog.Models
{
    public class LanMu
    {
        public Board Board { get; set; }
        public IEnumerable<BoardDetails> LanMulist { get; set; }
        
        public IEnumerable<BlogWorks> Works { get; set; }

    }
}