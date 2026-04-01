using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models.Product;

namespace EcommerceAPI.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product,ProductDTO>().ForMember(x=>x.CategoryName,option=>option.MapFrom(src=>src.category.Name)).ReverseMap();
            CreateMap<AddProductDTO, Product>().ForMember(m=>m.Photos,op=>op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ForMember(m=>m.Photos,op=>op.Ignore()).ReverseMap();
        }
    }
}
