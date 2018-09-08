using System;
using System.Collections.Generic;
using LotionCream.API.ShareType;
using LotionCream.API.Models.Comments;
using LotionCream.API.Models.UserManagement;
namespace LotionCream.API.Models.Dtos
{
    public class PostDto
    {
        public long PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public long PostAuthorID { get; set; }
       // public virtual User PostAuthor { get; set; }
        public string PostCategory { get; set; }
        public byte[] PostPhoto { get; set; }
        public DateTime CreatedOn => DateTime.Now;
        public int CategoryID {get; set;}

       // public virtual ICollection<Comment> PostComments { get; set; }
       // public virtual ICollection<ShareType> ShareType { get; set; }
    }
}