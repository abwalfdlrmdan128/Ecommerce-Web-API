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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Id).IsRequired();
            builder.HasData(
                new Category { Id = 1, Name = "Electronics",Description= "Electronics Devices"},
                new Category { Id = 2, Name = "Clothes" , Description = "Clothes" },
                new Category { Id = 3, Name = "Books", Description = "Books" }
                );

        }
    }
}
