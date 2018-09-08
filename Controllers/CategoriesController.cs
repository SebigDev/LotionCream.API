using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LotionCream.API.Models.Categories;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace LotionCream.API.Controllers
{
     [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var getAll = await _categoryServices.GetAllCategories();
                if(getAll != null){
                    return Ok(getAll);
                }
                return NotFound("No Categories Found");
            }
            catch (Exception ex)
            {
               return BadRequest($"Error! Cannot complete the request at this moment, Contact administrator, {ex.Message}");
            }
        }

         [HttpGet("GetCategoryByID")]
         [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryByID(int ID)
        {
            try
            {
                var getAll = await _categoryServices.GetCategoryByID(ID);
                if(getAll != null){
                    return Ok(getAll);
                }
                return BadRequest($"No Category with ID: {ID} Found");
            }
            catch (Exception ex)
            {
               return BadRequest($"Error! Cannot complete the request at this moment, Contact administrator, {ex.Message}");
            }
        }

        [HttpPut("UpdateCategory")]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
          public async Task<IActionResult> UpdateCategory ([FromBody] CategoryDto model)
        {
            try
            {
                var getAll = await _categoryServices.Update(model.CategoryID);
                if(getAll == true){
                    return Ok(getAll);
                }
                return BadRequest($"No Category with ID: {model.CategoryID} Found");
            }
            catch (Exception ex)
            {
               return BadRequest($"Error! Cannot complete the request at this moment, Contact administrator, {ex.Message}");
            }
        }
         [HttpPost("CreateCategory")]
         [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            try
            {
            if(ModelState.IsValid)
            {
                 await _categoryServices.AddCategory(category);
              
                return StatusCode(201, $"{category.CategoryName} Created Successfully");
            }
            return BadRequest("Category Cannot be created");
            }
            catch (Exception ex)
            {
               return BadRequest($"Error! Cannot complete the request at this moment, Contact administrator, {ex.Message}");
            }
        }
        
        [HttpDelete("DeleteCategory")]
          public async Task<IActionResult> DeleteCategory(int ID)
        {
            try
            {
              var getCatToDelete = await _categoryServices.GetCategoryByID(ID);
              if(getCatToDelete != null){
                    await _categoryServices.Delete(ID);
                    return Ok("Category Deleted Successfully");
              }
              return NotFound($"No Category with the ID: {ID} found to delete");
            }
            catch (Exception ex)
            {
               return BadRequest($"Error! Cannot complete the request at this moment, Contact administrator, {ex.Message}");
            }
        }

    }
}