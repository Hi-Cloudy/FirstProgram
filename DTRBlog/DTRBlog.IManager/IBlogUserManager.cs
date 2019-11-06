using DTR.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.IManager
{
   public interface IBlogUserManager
    {

        string GetHeadImg(string blogID);
        bool ExistUser(string id);
        int FanCount(string BlogId);
        DTRBlog.Model.BlogUser FindAuthor(string BlogId);
        bool ExistFlolow(string BlogId, string TargetBlogId);
        bool IsBothFollow(string BlogId1, string BlogId2);
        bool AddFlolow(string BlogId, string TargetBlogId);
        bool CancelFlolow(string BlogId, string TargetBlogId);
        int GetCommentCountByUser(string blogID);
        int GetCollectionCountByUser(string blogID);
        List<DTRBlog.Model.FollowListModel> GetFollowList(string blogId, int pageIndex, int pageSize, out int count);
        List<DTRBlog.Model.FollowListModel> GetFansList(string blogId, int pageIndex, int pageSize, out int count);
        List<DTRBlog.Model.CollectionListMode> GetCollectionList(string blogId, int pageIndex, int pageSize, out int count);
        bool RemoveCollection(int collectionId);
    }
}
