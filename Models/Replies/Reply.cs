using System;
using System.Collections.Generic;
using LotionCream.API.ShareType;
using LotionCream.API.Models.Comments;
using LotionCream.API.Models.UserManagement;
namespace LotionCream.API.Models.Replies
{
    public class Reply
    {
        public int ReplyID { get; set; }
        public string ReplyBody { get; set; }
        public long ReplyAuthorID { get; set; }
        public User ReplyAuthor { get; set; }
        public DateTime ReplyDate => DateTime.Now;
        public int ShareTypeID { get; set; }
       // public ICollection<ShareType> ShareType { get; set; }
        public int CommentID { get; set; }
        public virtual Comment Comment { get; set; }
    }
}