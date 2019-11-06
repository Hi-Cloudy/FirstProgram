using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTR.DAL;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace DTRBlog.Models.Articley.ArticleBLL
{
    public class ArticleBase : IDisposable
    {
        public ArticleBase()
        {
            dbCtrl = new DTREF();
        }

        protected DTREF dbCtrl;

        protected DbSet<Entity> GetEntity<Entity>() where Entity : class
        {
            return dbCtrl.Set<Entity>();
        }
        private bool disposedValue = false; 

        public int SaveChange()
        {
            return this.dbCtrl.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {}
                this.dbCtrl.Dispose();
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ArticleBase()
        {
            this.Dispose(false);
        }


    }
}