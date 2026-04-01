using AutoMapper;
using Ecommerce.Core.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUniOfwork uniOfwork, IMapper mapper) : base(uniOfwork, mapper) { }

        [HttpGet("Not-Found")]
        public async Task<IActionResult> GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("Server-Error")]
        public async Task<IActionResult> GeServerError()
        {
            return NotFound();
        }
        [HttpGet("Bad-Request/{id}")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
      
    }
}
