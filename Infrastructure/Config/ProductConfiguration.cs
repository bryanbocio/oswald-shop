using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.id).IsRequired();
            builder.Property(product => product.name).IsRequired()
                   .HasMaxLength(100);
            builder.Property(product => product.description).IsRequired();
            builder.Property(product => product.price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(product =>product.pictureUrl).IsRequired();
            
            builder.HasOne(productBrand => productBrand.productBrand).WithMany()
                    .HasForeignKey(product=>product.productBrandId);

            builder.HasOne(productType => productType.productType).WithMany()
                    .HasForeignKey(product => product.productTypeId);





        }
    }
}
