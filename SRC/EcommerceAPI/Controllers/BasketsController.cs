using AutoMapper;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models;
using EcommerceAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    public class BasketsController : BaseController
    {
        public BasketsController(IUniOfwork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("Get-basket-Item/{id}")]
        public async Task<IActionResult> GettemInBasket(string id)
        {
            var result = await _uniOfwork.customerBasketRepository.GetBasketAsync(id);
            if (result is null)
            {
                return Ok(new CustomerBasket());
            }
            return Ok(result);
        }

        [HttpPost("Update-Basket")]
        public async Task<IActionResult> AddItemInBasket(CustomerBasket basket)
        {
            var _basket = await _uniOfwork.customerBasketRepository.UpdateBasketAsync(basket);
            return Ok(basket);
        }

        [HttpDelete("Delete-Basket-Item/{id}")]
        public async Task<IActionResult> DeleteItemInBasket(string id)
        {
            var result = await _uniOfwork.customerBasketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ResponseAPI(200, "item deleted!")) :
                BadRequest(new ResponseAPI(400));
        }
    }
}
