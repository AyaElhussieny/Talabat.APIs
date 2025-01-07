using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repsitory.Data.Cofigruations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(P => P.ProductBrandId)
                   //.OnDelete(DeleteBehavior.SetNull) //if we want to delete the brand and set the brandId to null
                   //if ProductBrandId allow null
                   ;


            builder.HasOne(P => P.ProductType)
                   .WithMany()
                   .HasForeignKey(P => P.ProductTypeId);
                  


            //proroity of the configurations [required]

            builder.Property(P => P.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(P => P.Description)
                   .IsRequired();

            builder.Property(P => P.PictureUrl)
                     .IsRequired();

            builder.Property(P => P.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }
}
