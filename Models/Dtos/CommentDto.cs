using System;
using System.Collections.Generic;
using LotionCream.API.ShareType;
using LotionCream.API.Models.Posts;
using LotionCream.API.Models.UserManagement;
namespace LotionCream.API.Models.Dtos
{
    public class CommentDto
    {
        public int CommentID { get; set; }
        public string CommentBody { get; set; }
        public long CommentAuthorID { get; set; }
        //public virtual User CommentAuthor { get; set; }
        public DateTime CommentDate => DateTime.Now;
        public long PostID {get; set;}
        public byte[] CommentPicture { get; set;}
       // public ICollection<ShareType> ShareType { get; set; }
    }
}