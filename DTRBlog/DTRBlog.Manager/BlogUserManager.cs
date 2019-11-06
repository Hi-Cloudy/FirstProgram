using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTR.DAL;
using DTR.DATAConfig;
using DTRBlog.IManager;
using DTRBlog.IService;
using DTRBlog.Model;

namespace DTRBlog.Manager
{
    public class BlogUserManager : IBlogUserManager
    {
        private IBlogUserService blogUserService = null;
        private IArticleManager articleManager = null;
        private IFollowService followService = null;
        private INewsService newsService = null;
        private IBlogCommentService blogCommentService = null;
        private ICollectionService collectionService = null;

        public BlogUserManager(IBlogUserService blogUserService, 
            IArticleManager articleManager, 
            IFollowService followService, 
            INewsService newsService,
            IBlogCommentService blogCommentService,
            ICollectionService collectionService)
        {
            this.blogUserService = blogUserService;
            this.articleManager = articleManager;
            this.followService = followService;
            this.newsService = newsService;
            this.blogCommentService = blogCommentService;
            this.collectionService = collectionService;
        }

        public bool ExistUser(string id)
        {
            return blogUserService.Any(u => u.BlogID == id);
        }

        public int FanCount(string BlogId)
        {
            return followService.Count(f => f.TargetBlogID == BlogId);
        }

        public Model.BlogUser FindAuthor(string BlogId)
        {
            if (blogUserService.Find(u => u.BlogID == BlogId).Any())
            {
                var author = blogUserService.Find(u => u.BlogID == BlogId).First();
                string img=  this.GetHeadImg(BlogId);

                int fanCount = followService.Count(f => f.TargetBlogID == BlogId);
                int followCount = followService.Count(f => f.BlogID == BlogId);
                int commentCount = this.GetCommentCountByUser(BlogId);
                int collectionCount = this.GetCollectionCountByUser(BlogId);
                var article = articleManager.FindAllBlogWorksByUser(BlogId);

                int count = 0;

                foreach (var item in article)
                {
                    count += item.BorwseCount.Value;
                }
                Model.BlogUser user = new Model.BlogUser
                {
                    BlogID = author.BlogID,
                    WorksCount = article.Count(),
                    BorwseCount = count,
                    Nikename = author.Nikename,
                    FrozenState = author.FrozenState,
                    UserPath = img,
                    SupportCount = author.SupportCount,
                    FansCount = fanCount,
                    FollowCount= followCount,
                    CollectionCount = collectionCount,
                    CommentCount = commentCount,
                };

                return user;
            }
            return null;
        }


        public bool ExistFlolow(string BlogId, string TargetBlogId)
        {
            return followService.Find(u => u.BlogID == BlogId && u.TargetBlogID == TargetBlogId).Any();
        }
        public bool IsBothFollow(string BlogId1, string BlogId2)
        {
            bool fl1 = this.ExistFlolow(BlogId1, BlogId2);
            bool fl2 = this.ExistFlolow(BlogId2, BlogId1);
            return (fl1 && fl2);
        }
        public bool AddFlolow(string BlogId, string TargetBlogId)
        {

            if (!followService.Find(u => u.BlogID == BlogId && u.TargetBlogID == TargetBlogId).Any())
            {
                var t = new DTR.DAL.Follow()
                {
                    BlogID = BlogId,
                    TargetBlogID = TargetBlogId,
                    FollowDate = DateTime.Now
                };

                 newsService.Add(new DTR.DAL.News()
                    {
                        AccepteBlogID = TargetBlogId,
                        NewsTypeID = 2,
                        SourceBlogID = BlogId,
                        NewsContent = "关注了你",
                        NewsDate = DateTime.Now,
                        ReadState = false
                    });
            

                bool result = followService.Add(t) > 0;

                return result;
            }
            return false;

        }
        public bool CancelFlolow(string BlogId, string TargetBlogId)
        {
            var del = followService.Find(u => u.BlogID == BlogId && u.TargetBlogID == TargetBlogId).First();

            if (del != null)
            {
                return followService.Delete(del);
            }

            return false;

        }

        public int GetCommentCountByUser(string blogID)
        {
            return blogCommentService.Count(b => b.BlogID == blogID);
        }

        public int GetCollectionCountByUser(string blogID)
        {
            return collectionService.Count(b => b.BlogID == blogID);
        }

        public List<FollowListModel> GetFollowList(string blogId, int pageIndex, int pageSize, out int count)
        {
            int totalCount = 0;
            var list = followService.PageList(pageIndex, pageSize, out totalCount, n => n.BlogID == blogId, OrderType.Desc, order => order.FollowDate).Select(s => new FollowListModel
            {
              ID=s.FollowID,
              BlogID=s.BlogUser1.BlogID,
              NickName=s.BlogUser1.Nikename
            }).ToList();
            count = totalCount;
           
            foreach (var item in list)
            {
                 item.HeadImg =  this.GetHeadImg(item.BlogID); 
                item.IsBothFollow = this.IsBothFollow(blogId, item.BlogID);
            }

            return list;
        }
        

        public List<FollowListModel> GetFansList(string blogId, int pageIndex, int pageSize, out int count)
        {
            int totalCount = 0;
            var list = followService.PageList(pageIndex, pageSize, out totalCount, n => n.TargetBlogID == blogId, OrderType.Desc, order => order.FollowDate).Select(s => new FollowListModel
            {
                ID = s.FollowID,
                BlogID = s.BlogUser.BlogID,
                NickName = s.BlogUser.Nikename
            }).ToList();
            count = totalCount;

            foreach (var item in list)
            {
                bool isFollow = this.ExistFlolow(blogId, item.BlogID);
                item.HeadImg = this.GetHeadImg(item.BlogID);
                item.IsExistFollow = isFollow;
                item.IsBothFollow = this.IsBothFollow(blogId, item.BlogID);
            }
            return list;
        }

        public List<CollectionListMode> GetCollectionList(string blogId, int pageIndex, int pageSize, out int count)
        {
            int totalCount = 0;
            var list = collectionService.PageList(pageIndex, pageSize, out totalCount, n => n.BlogID == blogId, OrderType.Desc, order => order.CollectionDate).Select(s => new CollectionListMode
            {
                Id = s.CollectionID,
                BlogID = s.BlogWorks.BlogUser.BlogID,
                articleId = s.BlogWorksID,
                Title = s.BlogWorks.Title,
                Intr = s.BlogWorks.Introduc,
                CollectionTime = s.CollectionDate.Value
            }).ToList();
            count = totalCount;
            return list;
        }

        public bool RemoveCollection(int collectionId)
        {
            if (collectionService.Any(c => c.CollectionID == collectionId))
            {
              return  collectionService.Delete(collectionService.Find(collectionId));
            }
            return false;
        }

        public string GetHeadImg(string blogID)
        {
            var author = blogUserService.Find(u => u.BlogID == blogID).First();
            string headerPath = Path.Combine(DATAPathConfigs.DTRDataConfigFullPath, author.UserPath, DATAPathConfigs.BlogUserHeaderImg);
            string query = "?query=" + new Random().Next(1, 10000000).ToString();

            string img = "";
            if (!File.Exists(headerPath))
            {
                img = Path.Combine("/", @"DTRDefaultResource\Default\Pictrue\head.jpg"+ query);
            }
            else
            {
                img = DATAPathConfigs.DTRDataConfigNetWorkelPath + author.UserPath + DATAPathConfigs.BlogUserHeaderImg + query;
            }
            return img;
        }
    }
}
