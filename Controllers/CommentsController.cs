using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LotionCream.API.Models.Comments;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Services.Comments;
using Microsoft.AspNetCore.Mvc;

namespace LotionCream.API.Controllers
{
     [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentServices _commentServices;
        public CommentsController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        

        [HttpGet("GetAllComments")]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllComments()
        {
           try
           {
                var allComments = await _commentServices.GetAllComments();
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return NotFound("No Comments Found");
           }
           catch (Exception)
           {
               return BadRequest("Error! Request not completed, Contact administrator");
           }
        }

          [HttpGet("GetAllCommentByPostID")]
           [ProducesResponseType(typeof(IEnumerable<CommentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCommentByPostID(long ID)
        {
           try
           {
                var allComments = await _commentServices.GetAllCommentByPostID(ID);
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return NotFound($"Comments with Post ID: {ID} not found");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }
      [HttpGet("GetAllCommentsByAuthorID")]
       [ProducesResponseType(typeof(IEnumerable<CommentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCommentsByAuthorID(long ID)
        {
             try
           {
                var allComments = await _commentServices.GetAllCommentsByAuthorID(ID);
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return NotFound($"Comments with Author ID: {ID} not found");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }
      [HttpGet("GetCommentByID")]
       [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentByID(long ID)
        {
            try
           {
                var allComments = await _commentServices.GetCommentByID(ID);
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return NotFound($"Comments with Post ID: {ID} not found");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }
      [HttpGet("GetAllCommentsByDateAndPostName")]
       [ProducesResponseType(typeof(IEnumerable<CommentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCommentsByDateAndPostName(DateTime date, string name)
        {
             try
           {
                var allComments = await _commentServices.GetAllCommentsByDateAndPostName(date, name);
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return NotFound($"Comments with Post Title: {name} and Date: {date} not found");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }
       
        [HttpPost("Create")]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto comment)
        {
             try
           {
                var allComments = await _commentServices.CreateComment(comment);
                if(allComments != null)
                {
                    return StatusCode(201, $" {comment.CommentBody} Created Successfully");
                }
                return NotFound($"Comments  cannot be created");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(long ID)
        {
             try
           {
                var allComments = await _commentServices.DeleteComment(ID);
                if(allComments == true)
                {
                    return Ok(allComments);
                }
                return NotFound($"Comments with ID: {ID} not found");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }
         [HttpPut("UpdateComment")]
          [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDto model)
        {
             try
           {
                var allComments = await _commentServices.UpdateComment(model.CommentID);
                if(allComments == true)
                {
                    return Ok(allComments);
                }
                return NotFound($"Comments with ID: {model.CommentID} not found");
           }
           catch (Exception)
           {
             return BadRequest("Error! Request not completed, Contact administrator");
           }
        }
    }
}