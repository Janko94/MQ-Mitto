using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class  
    {
        private MittoContext _context;
        public RepositoryBase(MittoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllList()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T[]> GetAllArray()
        {
            return await _context.Set<T>().ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async void Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async void CreateRange(IEnumerable<T> entityRange)
        {
            await _context.Set<T>().AddRangeAsync(entityRange);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async void Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
