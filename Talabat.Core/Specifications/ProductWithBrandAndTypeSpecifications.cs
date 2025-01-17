using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
       // constructor used in GET ALL Products 
        public ProductWithBrandAndTypeSpecifications():base()
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            
        }

        // constructor used in GET Product By Id 

        public ProductWithBrandAndTypeSpecifications(int id) : base( P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);

        }
    }
}
