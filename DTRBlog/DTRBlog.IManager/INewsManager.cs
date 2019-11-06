using DTR.DAL;
using DTRBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.IManager
{
   public interface INewsManager
    {
        List<Notice> GetAllNews(int NewsType, string BlogID);
        List<Notice> GetAllNews(int NewsType, string BlogID, int PageIndex, int PageSize, out int count);
        bool CanelReadState(int NewID, int TypeID);
        bool DelNews(int NewID);
        bool  DelAllNews(int NewsType, string BlogID);
        int GetNewsCount(int NewsType, string BlogID);
        int GetNewsCount(string BlogID);
    }
}
