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
    public class NewsManager : INewsManager
    {
        private INewsService newsService = null;
        private IArticleService articleService = null;
        public NewsManager(INewsService newsService, IArticleService articleService)
        {
            this.newsService = newsService;
            this.articleService = articleService;
        }
        public List<Notice> GetAllNews(int NewsType, string BlogID)
        {
            var msg = newsService.Find(n => n.NewsTypeID == NewsType && n.AccepteBlogID == BlogID).OrderByDescending(o => o.NewsDate).Select(s => new Notice
            {
                NewsID = s.NewsID,
                NewsContent = s.NewsContent,
                ReadState = s.ReadState.Value,
                NewsDate = s.NewsDate,
                URL = s.URL,
                NewsType = s.NewsType,
                Admin = s.Admin,
                FromUser = s.BlogUser,
                ToUser = s.BlogUser1
            }).ToList();

            return msg;
        }
        public bool CanelReadState(int NewID, int TypeID)
        {
            var entity = newsService.Find(NewID);
            entity.ReadState = true;
            return newsService.Update(entity);
        }
        public bool DelNews(int NewID)
        {
            var entity = newsService.Find(NewID);
            return newsService.Delete(entity);
        }
        public bool DelAllNews(int NewsType, string BlogID)
        {
            var news = newsService.Find(n => n.NewsTypeID == NewsType && n.AccepteBlogID == BlogID).ToList();
            foreach (var item in news)
            {
                newsService.Delete(item, false);
            }
            int isSuccess = newsService.Save();

            return isSuccess > 0;
        }
        public List<Notice> GetAllNews(int NewsType, string BlogID, int PageIndex, int PageSize, out int count)
        {
            int totalCount = 0;

            var list = newsService.PageList(PageIndex, PageSize, out totalCount, n => n.NewsTypeID == NewsType && n.AccepteBlogID == BlogID, OrderType.Desc, order => order.NewsDate).Select(s => new Notice
            {
                NewsID = s.NewsID,
                NewsContent = s.NewsContent,
                ReadState = s.ReadState.Value,
                NewsDate = s.NewsDate,
                URL = s.URL,
                NewsType = s.NewsType,
                Admin = s.Admin,
                FromUser = s.BlogUser,
                ToUser = s.BlogUser1
            }).ToList();

            foreach (var item in list)
            {
                if (string.IsNullOrWhiteSpace(item.URL)) continue;
                var article = articleService.Find(int.Parse(item.URL));
                if(article!=null)
                item.ArticleTitle = article.Title;
            }

            count = totalCount;
            return list;
        }
        public int GetNewsCount(int NewsType, string BlogID)
        {
            int count = newsService.Find(n => n.NewsTypeID == NewsType && n.AccepteBlogID == BlogID && n.ReadState == false).Count();
            return count;
        }
        public int GetNewsCount(string BlogID)
        {
            int count = newsService.Find(n => n.AccepteBlogID == BlogID && n.ReadState == false).Count();
            return count;
        }
    }
}
