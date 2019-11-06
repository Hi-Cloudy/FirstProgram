using DTR.DAL;
using DTRBlog.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DTRBlog.Service
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private DbContext _baseDbContext;
        public BaseService()
        {
            _baseDbContext = ContextFactory.GetContext();
        }
        public int Add(T entity, bool isSave = true)
        {
            _baseDbContext.Set<T>().Add(entity);
            return isSave ? _baseDbContext.SaveChanges() : 0;

        }

        public T AddReturnEntity(T entity, bool isSave = true)
        {
            _baseDbContext.Set<T>().Add(entity);
            if (isSave) _baseDbContext.SaveChanges();
            return entity;

        }
        
        /// <returns></returns>
        public int AddRange(IEnumerable<T> entities, bool isSave = true)
        {
            _baseDbContext.Set<T>().AddRange(entities);
            return isSave ? _baseDbContext.SaveChanges() : 0;
        }

       
        public bool Delete(T entity, bool isSave = true)
        {
            _baseDbContext.Set<T>().Attach(entity);
            _baseDbContext.Entry<T>(entity).State = EntityState.Deleted;
            return isSave ? _baseDbContext.SaveChanges() > 0 : true;
        }


        public int DeleteRange(IEnumerable<T> entities, bool isSave = true)
        {
            _baseDbContext.Set<T>().RemoveRange(entities);
            return isSave ? _baseDbContext.SaveChanges() : 0;
        }

        public bool Update(T entity, bool isSave = true)
        {
            _baseDbContext.Set<T>().Attach(entity);
            _baseDbContext.Entry<T>(entity).State = EntityState.Modified;
            return isSave ? _baseDbContext.SaveChanges() > 0 : true;
        }

        public bool UpdateAll(List<T> models, bool isSave = true)
        {
            foreach (var model in models)
            {
                DbEntityEntry entry = _baseDbContext.Entry(model);
                entry.State = EntityState.Modified;
            }

            return isSave ? _baseDbContext.SaveChanges() > 0 : true;
        }

        public int Save()
        {
            return _baseDbContext.SaveChanges();
        }


        public bool Any(Expression<Func<T, bool>> anyLamdba)
        {
            return _baseDbContext.Set<T>().Any(anyLamdba);
        }


        public int Count(Expression<Func<T, bool>> countLamdba)
        {
            return _baseDbContext.Set<T>().Count(countLamdba);
        }

        public T Find(int ID)
        {
            return _baseDbContext.Set<T>().Find(ID);
        }
        public T Find(object ID)
        {
            return _baseDbContext.Set<T>().Find(ID);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> findLambda)
        {
            return _baseDbContext.Set<T>().Where(findLambda);
        }


        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <param name="number">返回的记录数【0-表示返回所有】</param>
        /// <param name="whereLandba">查询条件</param>
        /// <param name="orderType">排序方式</param>
        /// <param name="orderLandba">排序条件</param>
        /// <returns></returns>
        public IQueryable<T> FindList<TKey>(int number, Expression<Func<T, bool>> whereLandba, OrderType orderType, Expression<Func<T, TKey>> orderLandba)
        {
            IQueryable<T> _tIQueryable = _baseDbContext.Set<T>().Where(whereLandba);
            switch (orderType)
            {
                case OrderType.Asc:
                    _tIQueryable = _tIQueryable.OrderBy(orderLandba);
                    break;
                case OrderType.Desc:
                    _tIQueryable = _tIQueryable.OrderByDescending(orderLandba);
                    break;
            }
            if (number > 0) _tIQueryable = _tIQueryable.Take(number);
            return _tIQueryable;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="whereLandba">查询表达式</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="orderLandba">排序表达式</param>
        /// <returns></returns>
        public IQueryable<T> PageList<TKey>(int pageIndex, int pageSize, out int totalNumber, Expression<Func<T, bool>> whereLandba, OrderType orderType, Expression<Func<T, TKey>> orderLandba)
        {
            IQueryable<T> _tIQueryable = _baseDbContext.Set<T>().Where(whereLandba);
            totalNumber = _tIQueryable.Count();
            switch (orderType)
            {
                case OrderType.Asc:
                    _tIQueryable = _tIQueryable.OrderBy(orderLandba);
                    break;
                case OrderType.Desc:
                    _tIQueryable = _tIQueryable.OrderByDescending(orderLandba);
                    break;
                default: _tIQueryable = _tIQueryable.OrderBy(p => true); break;
            }
            _tIQueryable = _tIQueryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return _tIQueryable;
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void RollBackChanges()
        {
            var items = _baseDbContext.ChangeTracker.Entries().ToList();
            items.ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public void Dispose()
        {
            if(_baseDbContext!=null)
                _baseDbContext.Dispose();
        }

      
    }
}
