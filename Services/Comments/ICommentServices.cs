using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Comments;
using LotionCream.API.Models.Dtos;

namespace LotionCream.API.Services.Comments
{
    public interface ICommentServices : IDisposable
    {
         Task<IEnumerable<CommentDto>> GetAllComments();
         Task<IEnumerable<CommentDto>> GetAllCommentByPostID(long ID);
         Task<IEnumerable<CommentDto>> GetAllCommentsByAuthorID(long ID);
         Task<CommentDto> GetCommentByID(long ID);  
         Task<IEnumerable<CommentDto>> GetAllCommentsByDateAndPostName(DateTime date, string post);
         Task<bool> DeleteComment(long ID);
         Task<bool> UpdateComment(long ID);
         Task<Comment> CreateComment(CommentDto comment);
    }
}