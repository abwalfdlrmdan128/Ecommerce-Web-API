using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models.Product;
using EcommerceInfraStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries
{
    public class PhotoRepository : GenericRepositories<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
