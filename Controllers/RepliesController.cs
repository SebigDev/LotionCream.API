using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Services.Replies;
using Microsoft.AspNetCore.Mvc;

namespace LotionCream.API.Controllers
{
     [Route("api/[controller]")]
    public class RepliesController : Controller
    {
        private readonly IReplyServices _replyServices;
        public RepliesController(IReplyServices replyServices)
        {
            _replyServices = replyServices;
        }

        [HttpGet("GetAllReplies")]
        [ProducesResponseType(typeof(IEnumerable<ReplyDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllReplies()
        {
            try
            {
                var allReps = await _replyServices.GetAllReplies();
                if(allReps != null)
                {
                    return Ok(allReps);
                }
                return NotFound("No Replies Found");
            }
            catch (Exception)
            {
                return BadRequest("Error! Could not Complete your request, contact administrator");
            }
        }

        [HttpGet("GetRepliesByID")]
        [ProducesResponseType(typeof(ReplyDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRepliesByID(int ID)
        {
            try
            {
                var allRepId = await _replyServices.GetRepliesByID(ID);
                if(allRepId != null)
                {
                    return Ok(allRepId);
                }
                return NotFound($"No Reply with ID: {ID} found");
            }
            catch (Exception)
            {
                return BadRequest("Error! Could not Complete your request, contact administrator");
            }
        }
        [HttpGet("GetAllRepliesByCommentID")]
        [ProducesResponseType(typeof(IEnumerable<ReplyDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRepliesByCommentID(int ID)
        {
            try
            {
                var allRepComment = await _replyServices.GetAllRepliesByCommentID(ID);
                if(allRepComment != null)
                {
                    return Ok(allRepComment);
                }
                return NotFound($"No Reply on comment with ID: {ID} found");
            }
            catch (Exception)
            {
                return BadRequest("Error! Could not Complete your request, contact administrator");
            }
        }
        [HttpPost("CreateReply")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateReply([FromBody] ReplyDto reply)
        {
            try
            {
                 if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                var createReply = await _replyServices.CreateReply(reply);
                return StatusCode(201, $"Reply with ID: {reply.ReplyID} created successfully");
            }
            catch (Exception)
            {
                return BadRequest("Error! Could not Complete your request, contact administrator");
            }
        }

        [HttpPut("UpdateReply")]
        [ProducesResponseType(typeof(ReplyDto),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateReply([FromQuery] ReplyDto model)
        {
            try
            {
                 if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }

                var createReply = await _replyServices.UpdateReply(model.ReplyID);
                if(createReply == true)
                {
                    return Ok(createReply);
                }
                return NotFound($"No Reply with ID: {model.ReplyID} to Update");
                
            }
            catch (Exception)
            {
                return BadRequest("Error! Could not Complete your request, contact administrator");
            }
        }

          [HttpDelete("DeleteReply")]
        [ProducesResponseType(typeof(ReplyDto),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteReply([FromQuery]int ID)
        {
            try
            {
                 if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                var deleteReply = await _replyServices.DeleteReply(ID);
                if(deleteReply == true)
                {
                    return Ok($"Reply with ID: {ID} Delete Successfully");
                }
                return NotFound($"No Reply with ID: {ID} to Delete");
                
            }
            catch (Exception)
            {
                return BadRequest("Error! Could not Complete your request, contact administrator");
            }
        }
    }
}