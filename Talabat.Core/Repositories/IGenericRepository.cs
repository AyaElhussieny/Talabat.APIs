using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    // BaseEntity or inhertance BaseEntity is a class that has an Id property [Representation Id]
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Without Spacification
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        #endregion

        #region With Spacification
        Task<IEnumerable<T>> GetAllWithSpacAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpacAsync(ISpecification<T> spec);
        #endregion


        //Task<T> AddAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);

    }
}
