using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Intefaces
{
    public interface IAutho
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO login);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<string> ResetPassword(RestPasswordDTO restPassword);
        Task<bool> ActiveAccount(ActiveAccountDTO accountDTO);
        Task<bool> UpdateAddress(string email, Address address);
        Task<Address> getUserAddress(string email);
        Task<bool> UpdateProfile(string email, UserDTO userDTO);
    }
}
