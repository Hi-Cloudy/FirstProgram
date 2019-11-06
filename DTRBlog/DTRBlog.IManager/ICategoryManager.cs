using DTRBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.IManager
{
    public interface ICategoryManager
    {
        bool IsExistCategory(string blogID, string cateName);
        bool IsExistCategory(int cateID, string cateName);
        Category AddCategory(string blogID, string CateName);
        bool DelCategory(int categoryId);
        bool ModifyCategory(int categoryId, string cateName);

        string GetCategoryName(int id);
        List<Category> GetCategories(string blogID, int PageIndex, int PageSize, out int count);

        List<BlogWorks> GetArticleInfoByCategory(int cateID, string blogID, int pageIndex, int pageSize, out int count);

        List<BlogWorks> GetArticleByUser(string blogID, int pageIndex, int pageSize, out int count);

        bool AddArticleToCate(string str, int cateId);
        bool ModifyArticleToCate(int articleId);
        bool ModifyManyArticleToCate(string articleId);
    }
}
