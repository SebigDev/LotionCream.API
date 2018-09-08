using System;
using LotionCream.API.Enums;
namespace LotionCream.API.Models.UserManagement
{
    public class User
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Username { get; set; }
        public GenderEnum Gender { get; set; }
        public SkinColorEnum SkinColor { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate{
            get
            {
                return DateTime.Now;
            } 
            set
            {
                value = RegisterDate;
            }
            }
        public int Age
        {
            get
            {
                var age = ((DateTime.Now.Subtract(DateOfBirth).Days / 365));
                return age;
            }
            set
            {
                value = Age;
            }
        }
        public string FullName
        {
            get
            {
                var fullname = FirstName + " " + LastName;
                return fullname;
            }
            set
            {
                value = FullName;
            }
        }
    }
}