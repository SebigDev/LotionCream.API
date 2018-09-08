using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace LotionCream.API.Controllers
{
     [Route("api/[controller]")]
    public class PostsController: Controller
    {
        private readonly IPostServices _postServices;
        public PostsController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        [HttpGet("GetAllPosts")]
         [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var allPost = await _postServices.GetAllPost();
                if(allPost != null)
                {
                    return Ok(allPost);
                }
                return NotFound("No Posts returned");
            }
            catch (Exception)
            {
               return BadRequest("Error!  Request failed! contact administrator");
            }
        }

         [HttpGet("GetPostByPostID")]
         [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAPostByPostID(long ID)
        {
            try
            {
                var post = await _postServices.GetPostByPostId(ID);
                if(post != null)
                {
                    return Ok(post);
                }
                return NotFound($"Post with ID: {ID} not found");
            }
            catch (Exception)
            {
             return BadRequest("Error!  Request failed! contact administrator");
            }
        }
        
         [HttpGet("GetPostsByDate")]
          [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostsByDate(DateTime date)
        {
            try
            {
                var postDate = await _postServices.GetPostByPostDate(date);
                if(postDate != null)
                {
                    return Ok(postDate);
                }
                return NotFound($"Post with date: {date} not found");
            }
            catch (Exception)
            {
                return BadRequest("Error!  Request failed! contact administrator");
            }
        }
         [HttpGet("GetAllPostsByCategoryID")]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPostsByCategoryID(int ID)
        {
            try
            {
                var postCat = await _postServices.GetAllPostsByCategoryID(ID);
                if(postCat != null )
                {
                    return Ok(postCat);
                }
                return NotFound($"Post with category Id: {ID} not found");
            }
            catch (Exception)
            {
                
              return BadRequest("Error!  Request failed! contact administrator");
            }
          }
        [HttpGet("GetAllPostsByCategoryName")]
         [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPostsByCategoryName(string category)
        {
            try
            {
                var postCat = await _postServices.GetAllPostsByCategoryName(category);
                if(postCat != null)
                {
                    return Ok(postCat);
                }
                return NotFound($"Post with Catrgory Name: {category} not found");   
            }
            catch (Exception)
            { 
                return BadRequest("Error!  Request failed! contact administrator");
            }
        }

        [HttpGet("GetPostByCategoryAndDateCreated")]
         [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostByCategoryAndDateCreated(string category, DateTime date)
        {
            try
            {
                var postCat = await _postServices.GetPostByCategoryAndDateCreated(category, date);
                if(postCat != null)
                {
                    return Ok(postCat);
                }
                return NotFound($"Post with Catrgory Name: {category} and date: {date} not found");     
            }
            catch (Exception)
            { 
                return BadRequest("Error!  Request failed! contact administrator");
            }
        }

        [HttpPost("PostCreate")]
        public async Task<IActionResult> PostCreate([FromBody] PostDto postDto)
        {
             try
           {
                var create = await _postServices.CreatePost(postDto);
                if(create != null)
                {
                    return StatusCode(201, $" {postDto.PostTitle} Created Successfully");
                }
                return NotFound($"Post  cannot be created");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }


        [HttpPut("Update")]
         [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] PostDto model)
        {
            try
            {
                var postUpdate = await _postServices.UpdatePost(model.PostID);
                if(postUpdate == true)
                {
                    return Ok(postUpdate);
                }
                return NotFound($"Post with ID: {model.PostID} not found");  
            }
            catch (Exception)
            { 
                return BadRequest("Error!  Request failed! contact administrator");
            }
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> Delete(long ID)
        {
            try
            {
                var postUpdate = await _postServices.DeletePost(ID);
                if(postUpdate == true)
                {
                    return Ok(postUpdate);
                }
                return NotFound($"Post with ID: {ID} not found");     
            }
            catch (Exception)
            { 
                return BadRequest("Error!  Request failed! contact administrator");
            }
        }
        
    }
}