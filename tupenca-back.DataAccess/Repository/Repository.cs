using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.DataAccess.Repository.IRepository;

namespace tupenca_back.DataAccess.Repository
{
    
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _db = appDbContext;
            this.dbSet = _db.Set<T>();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter) =>
            dbSet.Where(filter)
                 .FirstOrDefault();

        public IEnumerable<T> GetAll() => dbSet.ToList();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            dbSet.Where(expression)
                 .AsNoTracking();

        public void Add(T entity) => dbSet.Add(entity);

        public void Update(T entity) => dbSet.Update(entity);

        public void Remove(T entity) => dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entity) => dbSet.RemoveRange(entity);

    }
}
