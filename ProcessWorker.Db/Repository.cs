using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProcessWorker.Bl.Interfaces;
using ProcessWorker.Entity;

namespace ProcessWorker.Db
{
    public class Repository : IRepository
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _context.Set<T>();
        }

        public ICollection<T> GetPaged<T>(
            PagingFilter paging,
            Expression<Func<T, bool>> filterClause,
            Expression<Func<T, object>> includeClause,
            Expression<Func<T, object>> orderByClause) where T : BaseEntity
        {
            var result = _context.Set<T>().AsQueryable();
            if (filterClause != null)
            {
                result = result.Where(filterClause);
            }

            if (includeClause != null)
            {
                result = result.Include(includeClause);
            }

            result = result.OrderBy(orderByClause ?? (x => x));

            return result.Skip((paging.Page-1) * paging.PageSize).Take(paging.PageSize).ToList();
        }

        public T GetById<T>(int id) where T : BaseEntity
        {
            return _context.Set<T>().Find(id);
        }

        public int Create<T>(T obj) where T : BaseEntity
        {
            _context.Add(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public int Update<T>(T obj) where T : BaseEntity
        {
            _context.Update(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public int Delete<T>(T obj) where T : BaseEntity
        {
            _context.Remove(obj);
            _context.SaveChanges();
            return obj.Id;
        }
    }
}