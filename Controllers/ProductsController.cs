using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace LotionCream.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var getAll = await _productServices.GetAllProducts();
                if(getAll != null)
                {
                    return Ok(getAll);
                }
                return NotFound("No Product list found");
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpGet("GetAllProductsByCreatorID")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductsByCreatorID(long ID)
        {
            try
            {
                var getAllByCatId = await _productServices.GetAllProductsByCreatorID(ID);
                if(getAllByCatId != null)
                {
                    return Ok(getAllByCatId);
                }
                return NotFound($"No Product list From {ID} found");   
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpGet("GetProductByID")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByID(int ID)
        {
            try
            {
                var getAllById = await _productServices.GetProductByID(ID);
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

        [HttpGet("GetProductByCategoryID")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByCategoryID(int ID)
        {
            try
            {
                var getAllByCatId = await _productServices.GetAllProductsByCategoryID(ID);
                if(getAllByCatId != null)
                {
                    return Ok(getAllByCatId);
                }
                return NotFound($"No Product with Category ID: {ID} found");  
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _productServices.CreateProduct(product);   
                    return StatusCode(201, $"{product.ProductName} Created Successfully");
                }
                return BadRequest("Cannot create product at this time");
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _productServices.UpdateProduct(model.ProductID);
                    if(update == true)
                    {
                        return Ok("Product Updated Successfully");
                    }
                    return NotFound($"Product with ID: {model.ProductID} not found");
                }
                return BadRequest();
            }
            catch (Exception)
            {
               return BadRequest("Error! Cannot complete request, Contact administrator");
            }
        }

         [HttpDelete("DeleteProduct")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int ID)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _productServices.DeleteProduct(ID);
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