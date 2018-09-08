using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.UserManagement;
using LotionCream.API.Services.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NaccCoreApp.API.Models.Dtos;

namespace LotionCream.API.Controllers
{
     [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsers()
        {
           try
           {
                var allUsers = await _userServices.GetAllUsers();
                if(allUsers != null){
                    return Ok(allUsers);
                }
                return BadRequest("No Users found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
         [HttpGet("GetAllUserByUsername")]
         [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsersByUsername(string username)
        {
             try
           {
                var allUsers = await _userServices.GetUserByUserName(username);
                if(allUsers != null){
                    return Ok(allUsers);
                }
                return BadRequest($"No User with Username: {username} found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
         [HttpGet("GetAllUsersBySkinColor")]
         [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsersBySkinColor(string color)
        {
              try
           {
                var allUsers = await _userServices.GetAllUsersBySkinColor(color);
                if(allUsers != null){
                    return Ok(allUsers);
                }
                return BadRequest($"No Users of Skin Color: {color} found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
         [HttpGet("GetAllUsersByGender")]
          [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsersByGender(string gender)
        {
         try
           {
                var allUsers = await _userServices.GetAllUsersByGender(gender);
                if(allUsers != null){
                    return Ok(allUsers);
                }
                return BadRequest($"No Users of Gender: {gender} found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
        [HttpGet("GetAllUserByID")]
         [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserByID(long ID)
        {
              try
           {
                var allUsers = await _userServices.GetUserByID(ID);
                if(allUsers != null){
                    return Ok(allUsers);
                }
                return BadRequest($"No User with {ID} found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
       [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
              try
           {
                if(user != null)
                {
                    var IscheckUserID = await _userServices.GetUserByID(user.UserID);
                    if(IscheckUserID != null)
                    {
                         await _userServices.UpdateUser(user);
                        return Ok($"{user.FullName} Updated Successfully");
                    } 
                }
                return BadRequest($"{user.FullName} Not found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(long ID)
        {
              try
           {
               var user = await _userServices.GetUserByID(ID);
                if(user != null)
                {
                    await _userServices.DeleteUser(ID);
                    return Ok($"{user.FullName} Deleted Successfully");
                }
                return BadRequest($"No User with {ID} found");
           }
           catch (Exception ex)
           {
               return BadRequest($"Error!, Your Request cannot be completed, Contact administrator, {ex.Message}");
           }
        }
       [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
      {
          registerDto.Username = registerDto.Username.ToLower();
          try
          {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
 
                if(await _userServices.UserExists(registerDto.Username, registerDto.EmailAddress))
                {
                    return BadRequest("Username or Email Address exists, Choose anothe name");
                }
                var userToCreate = new User
                {
                    Username = registerDto.Username,
                    EmailAddress = registerDto.EmailAddress,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Gender = registerDto.Gender,
                    DateOfBirth = registerDto.DateOfBirth,
                    IsActive = registerDto.IsActive,
                    SkinColor = registerDto.SkinColor
                };
                var createUser = await _userServices.Register(userToCreate, registerDto.Password);
                return StatusCode(201, $" Hello {registerDto.Username}, Your Registration was Successful.");

          }
          catch (Exception)
          {
             return BadRequest("Error!- User cannot be created, Contact Administrator");
          }
      }
    
     [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var logUser = await _userServices.Login(loginDto.Username.ToLower(), loginDto.Password);
            if(logUser == null)
            {
                return Unauthorized();
            }
            //Generating Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Super Secret Key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
               Subject =  new System.Security.Claims.ClaimsIdentity(new Claim []
               {
                   new Claim(ClaimTypes.NameIdentifier, logUser.UserID.ToString()),
                   new Claim(ClaimTypes.Name, logUser.Username)
               }),
               Expires = DateTime.Now.AddDays(2),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok( new {tokenString});
        }
        catch (Exception)
        {
          return StatusCode(400, "Error Processing this request, please contact the Administration");
        }
    }}

}