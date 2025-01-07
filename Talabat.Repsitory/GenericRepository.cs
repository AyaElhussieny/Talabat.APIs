using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
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
    }
}
