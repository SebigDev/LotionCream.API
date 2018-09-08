using System;
using System.ComponentModel.DataAnnotations;
using LotionCream.API.Enums;

namespace NaccCoreApp.API.Models.Dtos
{
    public class RegisterDto
    {     
        [Required] 
        public string EmailAddress{get; set;} 
        [Required]
        public string Username{get; set;}
        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage ="Password length MUST be greater than 4")]
        [DataType(DataType.Password)]
        public string Password{get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth {get; set;}
        public GenderEnum Gender { get; set; }
        public SkinColorEnum SkinColor { get; set; }
        public bool IsActive { get; set; } 
         public DateTime RegisterDate{get; set;}
    }
}