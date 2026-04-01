using Ecommerce.Core.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product{
                    Id=1,
                    Name="Cammera",
                    Description="this is an elctronic Device",
                    CategoryID=1,
                    NewPrice=200
                },
                new Product
                {
                    Id = 2,
                    Name = "labtop",
                    Description = "this is an elctronic Device",
                    CategoryID = 1,
                    NewPrice = 2000
                }
            );
        }
    }
}
