using BilgeAdamBlog.Core.Entity;
using BilgeAdamBlog.Core.Entity.Enums;
using BilgeAdamBlog.Core.Service;
using BilgeAdamBlog.Model.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BilgeAdamBlog.Service.Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly DataContext _context;
        public BaseService(DataContext context)
        {
            _context = context;
        }

        private DbSet<T> _entities;
        public DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
        public IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        public async Task<T> Add(T item)
        {
            try
            {
                if (item == null)
                    return null;

                await Entities.AddAsync(item);

                if (await Save() > 0)
                    return item;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddRange(List<T> items)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    await Entities.AddRangeAsync(items);
                    ts.Complete();
                    return await Save() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Any(Expression<Func<T, bool>> exp) => await Entities.AnyAsync(exp);

        public IQueryable<T> GetActive() => Entities.Where(x => x.Status != Status.Deleted).AsQueryable();

        public async Task<T> GetByDefault(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includeParameters)
        {
            IQueryable<T> queryable = Table;
            foreach (Expression<Func<T, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return await queryable.FirstOrDefaultAsync(exp);
        }

        public async Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includeParameters)
        {
            IQueryable<T> queryable = Table;
            foreach (Expression<Func<T, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return await queryable.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public IQueryable<T> GetDefault(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includeParameters)
        {
            IQueryable<T> queryable = Table;
            foreach (Expression<Func<T, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return queryable.Where(exp).AsQueryable();
        }

        public async Task<bool> Remove(T item)
        {
            try
            {
                item.Status = Status.Deleted;
                if (await Update(item) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                T item = await GetById(id);
                item.Status = Status.Deleted;
                if (await Update(item) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAll(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(exp);
                    int count = 0;
                    foreach (var item in collection)
                    {
                        item.Status = Status.Deleted;
                        if (await Update(item) != null)
                            count++;
                    }
                    if (collection.Count() == count)
                        ts.Complete();
                    else
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T item)
        {
            try
            {
                if (item == null)
                    return null;

                Entities.Update(item);

                if (await Save() > 0)
                    return item;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> Activate(Guid id)
        {
            T activated = await GetById(id);
            activated.Status = Status.Active;
            if (await Update(activated) != null)
                return true;
            else
                return false;
        }
    }
}
