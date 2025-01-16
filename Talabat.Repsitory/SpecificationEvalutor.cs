using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repsitory
{
      public static class SpecificationEvalutor <T> where T : BaseEntity
    {
        // fun build query
        //_dbContext.Products.Where(P => P.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType)


        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> Spec)
        {
            var Query = inputQuery; //_dbContext.Products

            // modify the IQueryable<T> based on the criteria expression

            if (Spec.Criteria != null)// criteria = [P => P.Id == id]
            {
                Query = Query.Where(Spec.Criteria); //_dbContext.Products.Where(P => P.Id == id)
            }
            // Includes all expression [Aggregate Operations]

            //CurrentQuery == _dbContext.Products.Where(P => P.Id == id)
            // includes == [P => P.ProductType, P => P.ProductBrand]
            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}
