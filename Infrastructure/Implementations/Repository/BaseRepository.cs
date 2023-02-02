using Core.Interfaces;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> dbSet;
        protected List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        protected List<string> IncludeStrings { get; } = new List<string>();

        public BaseRepository(DbContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
            dbSet.AsNoTracking();
        }

        public void BeginTransaction()
        {
            context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            context.Database.RollbackTransaction();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public bool IsActiveTransaction()
        {
            return context.Database.CurrentTransaction != null;
        }

        public virtual T GetFirst(Expression<Func<T, bool>> query)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet);
            return currentQuery.FirstOrDefault(query);
        }

        public virtual IEnumerable<T> ListAll()
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet);
            return currentQuery.AsEnumerable();
        }

        public virtual async Task<IReadOnlyList<T>> ListAllAsync()
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet);
            return await currentQuery.ToListAsync();
        }

        public IQueryable<T> ImplementIncludes(IQueryable<T> IncludedQuery)
        {
            IQueryable<T> currentQuery = IncludedQuery;
            foreach (var include in Includes)
                currentQuery = currentQuery.Include(include);
            foreach (var include in IncludeStrings)
                currentQuery = currentQuery.Include(include);
            return currentQuery;
        }

        public virtual IEnumerable<T> GetData(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, int maximumRows = 0, int startRowIndex = 0)
        {

            return Get(query, null, "", orderBy, maximumRows, startRowIndex, "");
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, bool isDesc = false, int maximumRows = 0, int startRowIndex = 0)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet.AsQueryable());
            if (query != null)
            {
                currentQuery = currentQuery.Where(query);
            }
            if (orderBy != null)
                if (isDesc)
                    currentQuery = currentQuery.OrderByDescending(orderBy).AsQueryable();
                else
                    currentQuery = currentQuery.OrderBy(orderBy).AsQueryable();
            if (maximumRows > 0)
                currentQuery = currentQuery.Take(maximumRows).AsQueryable();
            if (startRowIndex > 0)
                currentQuery = currentQuery.Skip(maximumRows).AsQueryable();
            return await currentQuery.ToListAsync();
        }
        public async Task<(IEnumerable<T>, int)> GetAsync(List<Expression> querys, int maximumRows = 0, int startRowIndex = 0)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet.AsQueryable());
            if (querys != null)
            {
                foreach (Expression<Func<T, bool>> query in querys)
                {
                    currentQuery = currentQuery.Where(query);
                }
            }

            int count = await currentQuery.CountAsync();

            if (maximumRows > 0)
                currentQuery = currentQuery.Take(maximumRows).AsQueryable();
            if (startRowIndex > 0)
                currentQuery = currentQuery.Skip((startRowIndex - 1)).AsQueryable();

            return (await currentQuery.ToListAsync(), count);
        }

        public async Task<IEnumerable<T>> GetAsync(List<Expression> querys, Expression<Func<T, object>> orderBy = null, bool isDesc = false, int maximumRows = 0, int startRowIndex = 0)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet.AsQueryable());
            if (querys != null)
            {
                foreach (Expression<Func<T, bool>> query in querys)
                {
                    currentQuery = currentQuery.Where(query);
                }
            }
            if (orderBy != null)
                if (isDesc)
                    currentQuery = currentQuery.OrderByDescending(orderBy).AsQueryable();
                else
                    currentQuery = currentQuery.OrderBy(orderBy).AsQueryable();
            if (maximumRows > 0)
                currentQuery = currentQuery.Take(maximumRows).AsQueryable();
            if (startRowIndex > 0)
                currentQuery = currentQuery.Skip(maximumRows).AsQueryable();
            return await currentQuery.ToListAsync();
        }

        public IQueryable<T> GetQAsync(Expression<Func<T, bool>> query = null, Expression<Func<T, object>> orderBy = null, bool isDesc = false, int maximumRows = 0, int startRowIndex = 0)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet.AsQueryable());
            if (query != null)
            {
                currentQuery = currentQuery.Where(query);
            }
            if (orderBy != null)
                if (isDesc)
                    currentQuery = currentQuery.OrderByDescending(orderBy).AsQueryable();
                else
                    currentQuery = currentQuery.OrderBy(orderBy).AsQueryable();
            if (maximumRows > 0)
                currentQuery = currentQuery.Take(maximumRows).AsQueryable();
            if (startRowIndex > 0)
                currentQuery = currentQuery.Skip(maximumRows).AsQueryable();
            return currentQuery;
        }
        public IQueryable<T> GetQAsync(List<Expression> querys = null, Expression<Func<T, object>> orderBy = null, bool isDesc = false, int maximumRows = 0, int startRowIndex = 0)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet.AsQueryable());
            if (querys != null)
            {
                foreach (Expression<Func<T, bool>> query in querys)
                {
                    currentQuery = currentQuery.Where(query);
                }
            }
            if (orderBy != null)
                if (isDesc)
                    currentQuery = currentQuery.OrderByDescending(orderBy).AsQueryable();
                else
                    currentQuery = currentQuery.OrderBy(orderBy).AsQueryable();
            if (startRowIndex > 0)
                currentQuery = currentQuery.Skip((startRowIndex - 1) * maximumRows).AsQueryable();
            if (maximumRows > 0)
                currentQuery = currentQuery.Take(maximumRows).AsQueryable();
            return currentQuery;
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> query)
        {
            return await dbSet.CountAsync(query);
        }

        public virtual async Task<int> CountAsync(List<Expression> querys)
        {
            IQueryable<T> currentQuery = dbSet.AsQueryable();
            if (querys != null)
            {
                foreach (Expression<Func<T, bool>> query in querys)
                {
                    currentQuery = currentQuery.Where(query);
                }
            }

            return await currentQuery.CountAsync();
        }

        public virtual IBaseRepository<T> AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }
        public virtual IBaseRepository<T> AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
            return this;
        }

        public virtual T Add(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual T AddNoSave(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual List<T> AddRangeNoSave(List<T> entity)
        {
            dbSet.AddRange(entity);
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }

        public virtual IQueryable<T> GetFromJsonQuery(string json)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet);
            var predicate = PredicateBuilder.True<T>();
            predicate = predicate.FromString(json);
            return currentQuery.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<T> GetFromJsonQuery(string json, Expression<Func<T, bool>> query)
        {
            IQueryable<T> currentQuery = ImplementIncludes(dbSet);
            var predicate = PredicateBuilder.True<T>();
            predicate = predicate.FromString(json);
            return currentQuery.Where(predicate).Where(query).AsQueryable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> query, string orderByString, Expression<Func<T, object>> orderBy = null, int maximumRows = 0, int startRowIndex = 0, string Json_filter = "")
        {
            IQueryable<T> queryable = ImplementIncludes(dbSet.AsQueryable());
            if (!string.IsNullOrEmpty(Json_filter))
            {
                queryable = GetFromJsonQuery(Json_filter);
            }
            if (query != null)
            {
                queryable = queryable.Where(query);
            }

            if (orderByString != null && orderByString.Length > 1)
            {
                string[] ordering = orderByString.Split(' ');
                if (ordering.Length > 1)
                {
                    string column = ordering[0];
                    column = GetColumnEquivalent(column);
                    string direccion = ordering[1];
                    if (direccion.ToLower() == "desc")
                    {
                        queryable = queryable.OrderByDescending(column);
                    }
                    else
                    {
                        queryable = queryable.OrderBy(column);
                    }
                }
            }
            else if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }
            queryable = queryable.Skip<T>(startRowIndex);
            if (maximumRows > 0)
            {
                queryable = queryable.Take(maximumRows);
            }

            return queryable.AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> query, Expression<Func<T, object>> groupBy, string orderByString, Expression<Func<T, object>> orderBy = null, int maximumRows = 0, int startRowIndex = 0, string Json_filter = "")
        {
            IQueryable<T> queryable = dbSet;
            if (!string.IsNullOrEmpty(Json_filter) && query != null)
            {
                queryable = GetFromJsonQuery(Json_filter, query);
            }
            else if (!string.IsNullOrEmpty(Json_filter))
            {
                queryable = GetFromJsonQuery(Json_filter);
            }
            else if (query != null)
            {
                queryable = ImplementIncludes(dbSet.Where(query));
            }
            else
            {
                queryable = ImplementIncludes(dbSet);
            }

            if (orderByString != null && orderByString.Length > 1)
            {
                var multipleORden = orderByString.Split(',');
                foreach (var currentOrder in multipleORden)
                {
                    string[] ordering = currentOrder.Trim().Split(' ');
                    if (ordering.Length > 1)
                    {
                        string column = ordering[0].Trim();
                        column = GetColumnEquivalent(column);
                        string direccion = ordering[1].Trim();
                        if (direccion.ToLower() == "desc")
                        {
                            queryable = queryable.OrderByDescending(column);
                        }
                        else
                        {
                            queryable = queryable.OrderBy(column);
                        }
                    }
                }
            }


            if (groupBy != null && query != null)
            {
                queryable = queryable.Where(query).GroupBy(groupBy).Select(m => m.FirstOrDefault());
            }
            if (groupBy != null && orderBy != null)
            {
                queryable = queryable.GroupBy(groupBy).Select(m => m.FirstOrDefault());
            }
            if (groupBy != null && query != null)
            {
                queryable = queryable.Where(query).GroupBy(groupBy).Select(m => m.FirstOrDefault());
            }
            if (orderBy != null && query != null)
            {
                queryable = queryable.Where(query);
            }
            if (orderBy != null && !(orderByString.Length > 1))
            {
                queryable = queryable.OrderBy(orderBy);
            }

            queryable = queryable.Skip<T>(startRowIndex);
            if (maximumRows > 0)
            {
                queryable = queryable.Take(maximumRows);
            }
            return queryable.AsEnumerable();
        }

        public int Count()
        {
            return dbSet.Count();
        }

        private string GetColumnEquivalent(string column)
        {
            string[] multipleColumns = column.Split('.');
            for (int i = 0; i < multipleColumns.Length; i++)
            {
                multipleColumns[i] = char.ToUpper(multipleColumns[i][0]) + multipleColumns[i].Substring(1);
            }
            return string.Join('.', multipleColumns);

        }

        public void UpdateNoSave(T entity)
        {
            dbSet.Update(entity);
        }
        public void UpdateRangeNoSave(List<T> entity)
        {
            dbSet.UpdateRange(entity);
        }

        public void DeleteNoSave(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRangeNoSave(List<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public DbSet<T> DbSet()
        {
            return dbSet;
        }
    }
}