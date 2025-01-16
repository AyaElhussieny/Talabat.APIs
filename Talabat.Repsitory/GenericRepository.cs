using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repsitory.Data;

namespace Talabat.Repsitory
{
     public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        //dependany injection
        private readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Without Spacification

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _dbContext.Products
                    .Include(p => p.ProductBrand)
                    .Include(p => p.ProductType)
                    .ToListAsync();
            }
            else {
                return await _dbContext.Set<T>().ToListAsync();
            }
            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // search DB for the id
            //return await _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync(); 
            return await _dbContext.Set<T>().FindAsync(id);
        }

        #endregion


        #region With Spacification

        public async Task<IEnumerable<T>> GetAllWithSpacAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();

        }

        public async Task<T> GetByIdWithSpacAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();

        }

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        #endregion
    }
}
