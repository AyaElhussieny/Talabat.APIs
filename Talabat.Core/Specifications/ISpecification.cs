using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    // Interface for the specifications  where type is BaseEntity [Database]
    public interface ISpecification <T> where T : BaseEntity 
    {
        //_dbContext.Products.Where(P => P.Id == id)
        //          .Include(P => P.ProductType)
        //          .Include(P => P.ProductBrand)

        //sign for property for where condition [Where(P => P.Id == id)]
        public Expression<Func<T, bool>> Criteria { get; set; }

        //sign for property for Include condition
        // [Include(P => P.ProductType).Include(P => P.ProductBrand)]

        public List<Expression<Func<T, object>>> Includes { get; set; }  
    }
}
