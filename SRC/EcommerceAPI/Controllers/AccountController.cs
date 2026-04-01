using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models;
using EcommerceAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static StackExchange.Redis.Role;

namespace EcommerceAPI.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUniOfwork uniOfwork, IMapper mapper) : base(uniOfwork, mapper)
        {
        }

        [Authorize]
        [HttpGet("get-address-for-user")]
        public async Task<IActionResult> getAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
                return Unauthorized();

            var address = await _uniOfwork.Autho.getUserAddress(email);

            if (address == null)
                return NotFound(new ResponseAPI(404, "No address found"));

            var result = _mapper.Map<ShipAddressDTO>(address);

            return Ok(result);
        }



        [HttpGet("Logout")]
        public void logout()
        {

            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(-1)
            });
        }


        [Authorize]
        [HttpGet("get-user-name")]
        public IActionResult GetUserName()
        {
            return Ok(new ResponseAPI(200, User.Identity.Name));
        }


        [HttpGet("IsUserAuth")]
        public async Task<IActionResult> IsUserAuth()
        {

            return User.Identity.IsAuthenticated ? Ok() : BadRequest();
        }



        [Authorize]
        [HttpPut("update-address")]
        public async Task<IActionResult> updateAddress(ShipAddressDTO addressDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = _mapper.Map<Address>(addressDTO);
            var result = await _uniOfwork.Autho.UpdateAddress(email, address);
            return result ? Ok() : BadRequest();
        }



        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDTO>> register(RegisterDTO registerDTO)
        {
            string result = await _uniOfwork.Autho.RegisterAsync(registerDTO);
            if (result != "done")
            {
                return BadRequest(new ResponseAPI(400, result));
            }
            return Ok(new ResponseAPI(200, result));
        }



        [HttpPost("Login")]
        public async Task<IActionResult> login(LoginDTO loginDTO)
        {
            string result = await _uniOfwork.Autho.LoginAsync(loginDTO);
            if (result.StartsWith("please"))
            {
                return BadRequest(new ResponseAPI(400, result));
            }

            Response.Cookies.Append("token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1)
            });
            return Ok(new ResponseAPI(200));
        }





        [HttpPost("active-account")]
        public async Task<ActionResult<ActiveAccountDTO>> active(ActiveAccountDTO accountDTO)
        {
            var result = await _uniOfwork.Autho.ActiveAccount(accountDTO);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(200));
        }








        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> forget(string email)
        {
            var result = await _uniOfwork.Autho.SendEmailForForgetPassword(email);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(404));
        }



        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UserDTO dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _uniOfwork.Autho.UpdateProfile(email, dto);
            return result ? Ok() : BadRequest(new ResponseAPI(400));
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> reset(RestPasswordDTO restPasswordDTO)
        {
            var result = await _uniOfwork.Autho.ResetPassword(restPasswordDTO);
            if (result == "done")
            {
                return Ok(new ResponseAPI(200));
            }
            return BadRequest(new ResponseAPI(400));
        }
    }
}

