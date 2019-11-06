using DTR.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Service
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// 获取请求数据库上下文
        /// </summary>
        /// <returns></returns>
        public static DTREF GetContext()
        {
            DTREF _DbContext = CallContext.GetData("Context") as DTREF;
            if (_DbContext == null)
            {
                _DbContext = new DTREF();
                CallContext.SetData("Context", _DbContext);
            }
            return _DbContext;
        }
    }
}
