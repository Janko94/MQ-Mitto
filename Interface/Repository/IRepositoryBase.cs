using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllList();

        Task<T[]> GetAllArray();

        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);

        void CreateRange(IEnumerable<T> entityRange);

        void Delete(T entity);
        void Commit();
    }
}
