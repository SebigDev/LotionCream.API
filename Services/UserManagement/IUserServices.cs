using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.UserManagement;

namespace LotionCream.API.Services.UserManagement
{
    public interface IUserServices
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string emailAddress, string password);
        Task<UserDto> GetUserByID(long ID);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<IEnumerable<UserDto>> GetAllUsersByGender(string gender);
        Task<IEnumerable<UserDto>> GetAllUsersBySkinColor(string color);
        Task<UserDto> GetUserByUserName(string username);
        Task<User> DeleteUser(long ID);
        Task UpdateUser(UserDto user);
    }
}