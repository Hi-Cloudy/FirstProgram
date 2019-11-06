using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRBlog.Models.Articley.ViewModel
{
    public class SingleBlogTypeCollection
    {
        public SingleBlogTypeCollection(IEnumerable<BlogTypeUI> blogType, IEnumerable<SingType> singTypes)
        {
            this.blogTypes = blogType;
            this.singTypes = singTypes;
        }
        public IEnumerable<BlogTypeUI> blogTypes { get; private set; }
        public IEnumerable<SingType> singTypes { get; private set; }
    }
}