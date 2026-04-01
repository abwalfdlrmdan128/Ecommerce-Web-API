using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models.Product;

namespace EcommerceAPI.Mapping
{
    public class PhotoMapping:Profile
    {
        public PhotoMapping()
        {
          CreateMap<Photo, PhotoDTO>().ReverseMap();
        }
    }
}
