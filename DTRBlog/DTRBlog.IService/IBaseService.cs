using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DTRBlog.IService
{
    public interface IBaseService<T> : IDisposable where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        int Add(T entity, bool isSave = true);
        /// <summary>  
        ///新增实体，返回对应的实体对象  
        /// </summary>  
        /// <param name="model">数据实体</param>  
        /// <returns>新增的实体对象</returns>  
        T AddReturnEntity(T entity, bool isSave = true);
        /// <summary>
        /// 批量添加【立即执行】
        /// </summary>
        /// <param name="entities">数据列表</param>
        /// <returns>添加的记录数</returns>
        int AddRange(IEnumerable<T> entities, bool isSave = true);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        bool Update(T entity, bool isSave = true);
        /// <summary>
        /// 批量新修改
        /// </summary>
        /// <param name="models"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        bool UpdateAll(List<T> models, bool isSave = true);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        bool Delete(T entity, bool isSave = true);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities">数据列表</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>删除的记录数</returns>
        int DeleteRange(IEnumerable<T> entities, bool isSave = true);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns>受影响的记录数</returns>
        int Save();

        /// <summary>
        /// 是否有满足条件的记录
        /// </summary>
        /// <param name="anyLamdba">条件表达式</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> anyLamdba);

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="countLamdba">查询表达式</param>
        /// <returns>记录数</returns>
        int Count(Expression<Func<T, bool>> countLamdba);


        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">实体ID</param>
        /// <returns></returns>
        T Find(int ID);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T Find(object ID);
        /// <summary>
        /// 查找实体 
        /// </summary>
        /// <param name="findLambda">Lambda表达式</param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> findLambda);


      

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <param name="number">返回的记录数【0-返回所有】</param>
        /// <param name="whereLandba">查询条件</param>
        /// <param name="orderType">排序方式</param>
        /// <param name="orderLandba">排序条件</param>
        /// <returns></returns>
        IQueryable<T> FindList<TKey>(int number, Expression<Func<T, bool>> whereLandba, OrderType orderType, Expression<Func<T, TKey>> orderLandba);


    

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
         IQueryable<T> PageList<TKey>(int pageIndex, int pageSize, out int totalNumber, Expression<Func<T, bool>> whereLandba, OrderType orderType, Expression<Func<T, TKey>> orderLandba);


        /// <summary>
        /// 回滚
        /// </summary>
        void RollBackChanges();
    }
    public enum OrderType
    {
        /// <summary>
        /// 不排序
        /// </summary>
        No,
        /// <summary>
        /// 升序
        /// </summary>
        Asc,
        /// <summary>
        /// 降序
        /// </summary>
        Desc
    }
}
