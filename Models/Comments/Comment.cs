using System;
using System.Collections.Generic;
using LotionCream.API.Models.Posts;
using LotionCream.API.ShareType;
using LotionCream.API.Models.UserManagement;
namespace LotionCream.API.Models.Comments
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string CommentBody { get; set; }
        public long CommentAuthorID { get; set; }
        public virtual User CommentAuthor { get; set; }
        public DateTime CommentDate => DateTime.Now;
        public byte[] CommentPicture { get; set;}

        public long PostID {get; set;}
        public virtual Post CommentPost{get; set;}
        //public ICollection<ShareType> ShareType { get; set; }
    }
}