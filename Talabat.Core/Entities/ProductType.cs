﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; } //for one to many relationship (Config fluent API)
    }
}
