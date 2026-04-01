using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models.Product;

namespace EcommerceAPI.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
           CreateMap<CategoryDTO,Category>().ReverseMap();
           CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
