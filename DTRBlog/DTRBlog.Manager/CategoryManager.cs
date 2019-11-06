using DTRBlog.IManager;
using DTRBlog.IService;
using DTRBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Manager
{
   public class CategoryManager : ICategoryManager
    {
        private ICategoryService categoryService = null;
        private IArticleService articleService = null;
        public CategoryManager(ICategoryService categoryService, IArticleService articleService)
        {
            this.categoryService = categoryService;
            this.articleService = articleService;
        }

        public Category AddCategory(string blogID, string CateName)
        {
          var entity=  categoryService.AddReturnEntity(new DTR.DAL.SingleBlogType()
            {
              BlogID = blogID,
              SingleBlogTypeName = CateName
            });

            var category = new Category()
            {
                CategoryID = entity.SingleBlogTypeID,
                CategoryName = entity.SingleBlogTypeName,
            };
            return category;
        }

        public List<Category> GetCategories(string blogID, int pageIndex, int pageSize, out int count)
        {
            int totalCount = 0;
            var cateList = categoryService.PageList(pageIndex,pageSize,out totalCount, cate => cate.BlogID == blogID, OrderType.Asc, order=>order.SingleBlogTypeID).Select(s=> new Category()
            {
              CategoryID=s.SingleBlogTypeID,
              CategoryName = s.SingleBlogTypeName,
              ArticleCount=s.BlogWorks.Count()
            }).ToList();
            count = totalCount;
            return cateList;
        }

        public bool DelCategory(int categoryId)
        {
            var cate = categoryService.Find(categoryId);
            return  categoryService.Delete(cate);
        }

        public bool IsExistCategory(string blogID, string cateName)
        {
            return categoryService.Any(cate => cate.BlogID == blogID && cate.SingleBlogTypeName == cateName);
        }
        public bool ModifyCategory(int categoryId,string cateName)
        {
            var cate = categoryService.Find(categoryId);
            cate.SingleBlogTypeName = cateName;
            return categoryService.Update(cate);
        }

        public bool IsExistCategory(int cateID, string cateName)
        {
            return categoryService.Any(cate => cate.SingleBlogTypeID == cateID && cate.SingleBlogTypeName == cateName);
        }

        public List<BlogWorks> GetArticleInfoByCategory(int cateID,string blogID,int pageIndex, int pageSize, out int count)
        {
            int totalCount = 0;
            var list=  articleService.PageList(pageIndex, pageSize, out totalCount, where => where.BlogID == blogID && where.SingleBlogTypeID == cateID, OrderType.Desc, order => order.PublishDate).Select(s => new BlogWorks()
              {
                  BlogWorksID = s.BlogWorksID,
                  BlogID = s.BlogID,
                  SingleBlogTypeName = s.SingleBlogType.SingleBlogTypeName,
                  Title = s.Title,
                  Introduc = s.Introduc,
                  PublishDate = s.PublishDate
              }).ToList();
            count = totalCount;
            return list;
        }

        public string GetCategoryName(int id)
        {
            if (!categoryService.Any(c => c.SingleBlogTypeID == id))
                return string.Empty;
            return categoryService.Find(id).SingleBlogTypeName;
        }
        public List<BlogWorks> GetArticleByUser(string blogID, int pageIndex, int pageSize, out int count)
        {
            int totalCount = 0;
            var list = articleService.PageList(pageIndex, pageSize, out totalCount, where => where.BlogID == blogID , OrderType.Desc, order => order.PublishDate).Select(s => new BlogWorks()
            {
                BlogWorksID = s.BlogWorksID,
                BlogID = s.BlogID,
                SingleBlogTypeName = s.SingleBlogType.SingleBlogTypeName,
                Title = s.Title,
                Introduc = s.Introduc,
                PublishDate = s.PublishDate
            }).ToList();
            count = totalCount;
            return list;
        }

        public bool AddArticleToCate(string str,int cateId)
        {
            string strTemp = str.Substring(0, str.LastIndexOf(","));
            string[] strArr = strTemp.Split(',');

            List<DTR.DAL.BlogWorks> list = new List<DTR.DAL.BlogWorks>();
            foreach (var item in strArr)
            {
                int articleId = int.Parse(item);
                var article= articleService.Find(articleId);
                article.SingleBlogTypeID = cateId;
                list.Add(article);
            }
           return articleService.UpdateAll(list);
        }

        public bool ModifyArticleToCate(int articleId)
        {
            if (!articleService.Any(a => a.BlogWorksID == articleId))
                return false;
            var article = articleService.Find(articleId);
            article.SingleBlogTypeID = null;
          return  articleService.Update(article);
        }

        public bool ModifyManyArticleToCate(string articleId)
        {
            string strTemp = articleId.Substring(0, articleId.LastIndexOf(","));
            string[] articleIdArr = strTemp.Split(',');
            List<DTR.DAL.BlogWorks> list = new List<DTR.DAL.BlogWorks>();

           
            foreach (var item in articleIdArr)
            {
                int id = int.Parse(item);
                if (!articleService.Any(a => a.BlogWorksID == id))
                    continue;
                var article = articleService.Find(id);
                article.SingleBlogTypeID = null;
                list.Add(article);
            }
            return articleService.UpdateAll(list);
        }
    }
}
