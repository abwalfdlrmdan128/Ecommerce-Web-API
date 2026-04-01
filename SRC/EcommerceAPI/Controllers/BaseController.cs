using AutoMapper;
using Ecommerce.Core.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUniOfwork _uniOfwork;
        protected readonly IMapper _mapper;
        public BaseController(IUniOfwork uniOfwork,IMapper mapper)
        {
            _uniOfwork = uniOfwork;
            _mapper = mapper;
        }
    }
}
