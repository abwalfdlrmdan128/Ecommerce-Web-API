using Ecommerce.Core.DTOS;
using Ecommerce.Core.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRating rating;

        public RatingsController(IRating rating)
        {
            this.rating = rating;
        }


        [HttpGet("get-rating/{productId}")]
        public async Task<IActionResult> get(int productId)
        {
            var result = await rating.GetAllRatingForProductAsync(productId);
            return Ok(result);
        }



        [HttpPost("add-rating")]
        public async Task<IActionResult> add(RatingDTO ratings)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await rating.AddRatingAsync(ratings, email);
            return result ? Ok() : BadRequest();
        }
    }
}
