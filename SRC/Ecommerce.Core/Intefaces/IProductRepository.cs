using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models.Product;
using Ecommerce.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Intefaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        // for Futuer Functoin
        Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParameters productParameters);
        Task<bool>AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);
        Task DeleteAsync(Product product);
    }
}
