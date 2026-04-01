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
    internal class PhotoConfiguratoin:IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> Builder)
        {
            Builder.HasData(
                new Photo { Id = 3, ImageName = "Test", ProductID = 1 },
                new Photo { Id = 4, ImageName = "Test", ProductID = 1 },
                new Photo { Id = 5, ImageName = "Test", ProductID = 1 });
        }
    }
}
