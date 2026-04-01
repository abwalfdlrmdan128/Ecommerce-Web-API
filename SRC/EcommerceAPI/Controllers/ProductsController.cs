using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models.Product;
using Ecommerce.Core.Sharing;
using EcommerceAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EcommerceAPI.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUniOfwork uniOfwork, IMapper mapper) : base(uniOfwork, mapper)
        { }

        [HttpGet("Get-All-Product")]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductParameters productParameters)
        {
            try
            {
                var products = await _uniOfwork.productRepository.GetAllAsync(productParameters);
                var TotalCount = await _uniOfwork.productRepository.CountAsync();
                return Ok(new Paginatoin<ProductDTO>(productParameters.PageNumber,productParameters.PageSize,TotalCount,products));
            }
            catch (Exception exceptoin)
            {
                return BadRequest(exceptoin.Message);
            }
        }

        [HttpGet("Get-By-Id/{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            try
            {

                var product = await _uniOfwork.productRepository.GetByIdAsync(id, x => x.category, x => x.Photos);
                var result = _mapper.Map<List<ProductDTO>>(product);
                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
            try
            {
                await _uniOfwork.productRepository.AddAsync(addProductDTO);
                return Ok(new ResponseAPI(200, "Item Has been Added Successfully"));
            }
            catch (Exception exceptoin)
            {
                return BadRequest(new ResponseAPI(400, exceptoin.Message));
            }
        }


        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            try
            {
                await _uniOfwork.productRepository.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200,"Product has been updated successfully !"));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            try
            {
                var product = await _uniOfwork.productRepository.GetByIdAsync(id, x => x.Photos, x => x.category);
                await _uniOfwork.productRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product has been Deleted"));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
