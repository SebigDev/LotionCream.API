using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.Replies;

namespace LotionCream.API.Services.Replies
{
    public interface IReplyServices : IDisposable
    {
         Task<IEnumerable<ReplyDto>> GetAllReplies();
         Task<ReplyDto> GetRepliesByID(int ID);
         Task<IEnumerable<ReplyDto>> GetAllRepliesByCommentID(long ID);
         Task<Reply> CreateReply(ReplyDto reply);
         Task<bool> UpdateReply(int ID);
        Task<bool> DeleteReply(int ID);
    }
}