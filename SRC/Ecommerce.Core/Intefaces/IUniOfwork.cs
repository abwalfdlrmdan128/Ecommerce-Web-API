using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Intefaces
{
    public interface IUniOfwork
    {
        public IPhotoRepository photoRepository { get; }
        public ICategoryRepository categoryRepository { get; }
        public IProductRepository productRepository { get; }
        public ICustomerBasketRepository customerBasketRepository { get; }
        public IAutho Autho { get; }
    }
}
