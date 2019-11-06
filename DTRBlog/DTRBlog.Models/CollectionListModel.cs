using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Model
{
   public class CollectionListMode
    {
        public int Id { get; set; }
        public string  BlogID { get; set; }
        public int articleId { get; set; }
        public string Title { get; set; }
        public string Intr { get; set; }
        public DateTime CollectionTime { get; set; }


    }

}
