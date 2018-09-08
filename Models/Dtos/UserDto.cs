using System;
using LotionCream.API.Enums;
namespace LotionCream.API.Models.Dtos
{
    public class UserDto
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
       // public string Password { get; set; }
        public string Username { get; set; }
        public GenderEnum Gender { get; set; }
        public SkinColorEnum SkinColor { get; set; }
        public bool IsActive { get; set; }
        public int Age { get; set; }
        public DateTime RegisterDate {get; set;}
        public string FullName { get; set; }
    }
}