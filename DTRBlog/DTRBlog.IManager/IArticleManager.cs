using DTR.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.IManager
{
   public interface IArticleManager 
    {

        string GetArticleCover(string path);
        List<BlogWorks> FindAllBlogWorksByUser(string blogUser);
        bool ExistArticle(string author,int articleId);
        DTRBlog.Model.ArticleDetail ShowArticle(string authorId, int articleId, string sessionId);
        List<DTRBlog.Model.SubCommentList> ShowSubComment(int ParentCommentId);
        bool UpdateArticleBrowse(int id,int blogId);
        bool GoodAdd(int articleId, string fromBlogUser, string targetBlogUser);
        bool BadAdd(int articleId, string fromBlogId);
        bool IsExistSupport(int articleId, string fromBlogId);
        bool AddComment(string BlogId, int BlogArticleId, string Content, out int CommentId, string AuthorArticleId);
        bool ReplyComment(string BlogId, int ArticleId , string Content, int ReplyId, out int CommentId, string AuthorArticleId);
        bool RemoveComment(int commentId);
        IEnumerable<Model.ArticleList> GetAllArticleByUser(int pageIndex, int pageSize, string blogUser, string currentUser, out int count);
        bool AddCollection(int articleId, string fromBlogUser, string targetBlogUser);
        bool CancelCollection(int articleId, string fromBlogId);
        bool IsExistCollection(int articleId, string fromBlogId);
       
    }
}
