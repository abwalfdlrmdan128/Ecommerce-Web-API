using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models.Product;
using EcommerceAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUniOfwork uniOfwork, IMapper mapper) : base(uniOfwork, mapper){}

        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories=await _uniOfwork.categoryRepository.GetAllAsync();
                if (categories is null)
                {
                    return BadRequest(new ResponseAPI(400));
                } 
                return Ok(categories);
            }
            catch (Exception exption)
            {
                return BadRequest(exption.Message);
            }
        }

        [HttpGet("Get-By-Id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _uniOfwork.categoryRepository.GetByIdAsync(id,x => x.Products);
                if (category is null)
                {
                   return NotFound(new ResponseAPI(404, $"Category with ID={id} not found"));
                }
                var result = _mapper.Map<CategoryDTO>(category);
                return Ok(result);
            }
            catch (Exception exption)
            {
                return BadRequest(exption.Message);
            }
        }

        [HttpPost("Add-Category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);
                await _uniOfwork.categoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200,"Item has been added"));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
         {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);
                await _uniOfwork.categoryRepository.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Item has been Updated"));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
         }

        [HttpDelete("Delete-Category/{id}")]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            try
            {
                await _uniOfwork.categoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item has been Deleted"));
            }
            catch (Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }


    }
}
