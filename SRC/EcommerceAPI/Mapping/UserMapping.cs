using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models;

namespace EcommerceAPI.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<UserDTO, AppUser>();
        }
    }
}
