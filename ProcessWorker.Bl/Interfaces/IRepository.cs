using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ProcessWorker.Entity;

namespace ProcessWorker.Bl.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> GetAll<T>() where T: BaseEntity;

        ICollection<T> GetPaged<T>(PagingFilter paging,
            Expression<Func<T, bool>> filterClause,
            Expression<Func<T, object>> includeClause,
            Expression<Func<T, object>> orderByClause) where T : BaseEntity;

        T GetById<T>(int id) where T: BaseEntity;

        int Create<T>(T obj) where T: BaseEntity;

        int Update<T>(T obj) where T: BaseEntity;

        int Delete<T>(T obj) where T: BaseEntity;
    }
}