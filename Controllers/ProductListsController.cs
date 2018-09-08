using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Services.ProductLists;
using Microsoft.AspNetCore.Mvc;

namespace LotionCream.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductListsController : Controller
    {
        private readonly IProductListService _productListService;

        public ProductListsController(IProductListService productListService)
        {
            _productListService = productListService;
        }

        [HttpGet("GetAllProductLists")]
        [ProducesResponseType(typeof(IEnumerable<ProductListDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductLists()
        {
            try
            {
                var getAll = await _productListService.GetAllProductLists();
                if(getAll != null)
                {
                    return Ok(getAll);
                }
                return NotFound("No Product list found");
            }
            catch (Exception ex)
            {
               return BadRequest($"Error! Cannot complete request, Contact administrator, {ex.Message}");
            }
        }

        [HttpGet("GetAllProductListsByProductID")]
        [ProducesResponseType(typeof(IEnumerable<ProductListDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductListsByProductID(int ID)
        {
            try
            {
                var getAllByCatId = await _productListService.GetAllProductListsByProductID(ID);
                if(getAllByCatId != null)
                {
                    return Ok(getAllByCatId);
                }
                return NotFound($"No Product list with {ID} found");   
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpGet("GetAllProductListsByID")]
        [ProducesResponseType(typeof(ProductListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductListsByID(int ID)
        {
            try
            {
                var getAllById = await _productListService.GetAllProductListsByID(ID);
                if(getAllById != null)
                {
                    return Ok(getAllById);
                }
                return NotFound($"No Product with ID: {ID} found");  
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductListDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _productListService.Create(model);   
                    return StatusCode(201, $"{model.ListName} Created Successfully");
                }
                return BadRequest("Cannot create product at this time");
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(typeof(ProductListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductListDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _productListService.Update(model);
                    if(update == true)
                    {
                        return Ok("Product Updated Successfully");
                    }
                    return NotFound($"Product with ID: {model.ListID} not found");
                }
                return BadRequest();
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

         [HttpDelete("DeleteProduct")]
        [ProducesResponseType(typeof(ProductListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int ID)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _productListService.Delete(ID);
                    if(update == true)
                    {
                        return Ok("Product deleted Successfully");
                    }
                    return NotFound($"Product with ID: {ID} not found");
                }
                return BadRequest();
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }
    }
}