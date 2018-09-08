using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.Replies;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.Replies
{
    public class ReplyServices : IReplyServices
    {
        private readonly LotionCreamDBContext _lotionCreamDBContext;
        public ReplyServices(LotionCreamDBContext lotionCreamDBContext)
        {
            _lotionCreamDBContext = lotionCreamDBContext;
        }
        public async Task<Reply> CreateReply(ReplyDto reply)
        {
           var createReply = new Reply()
           {
               ReplyAuthorID = reply.ReplyAuthorID,
               ReplyBody = reply.ReplyBody,
               ReplyID = reply.ReplyID 
           };
            await _lotionCreamDBContext.AddAsync(createReply);
            await _lotionCreamDBContext.SaveChangesAsync();
            return createReply;
        }

        public async Task<IEnumerable<ReplyDto>> GetAllReplies()
        {
           var allReps = await _lotionCreamDBContext.Replies.ToListAsync();
            var allRepDto = new List<ReplyDto>();
            if(allReps.Count() > 0)
            {
                allRepDto.AddRange(allReps.OrderByDescending(o => o.ReplyDate).Select(r => new ReplyDto()
                {
                    ReplyAuthorID = r.ReplyAuthorID,
                    ReplyBody = r.ReplyBody,
                    ReplyID = r.ReplyID,
                }));
                return allRepDto;
            }
            return null;
        }

        public async Task<IEnumerable<ReplyDto>> GetAllRepliesByCommentID(long ID)
        {
            var allRep = await _lotionCreamDBContext.Replies.Where(a => a.CommentID == ID).ToListAsync();
            var allRepDto = new List<ReplyDto>();
            if(allRep.Count() > 0)
            {
                allRepDto.AddRange(allRep.OrderByDescending(o => o.ReplyDate).Select(r => new ReplyDto()
                {
                    ReplyAuthorID = r.ReplyAuthorID,
                    ReplyBody = r.ReplyBody,
                    ReplyID = r.ReplyID,
                }));
                return allRepDto;
            }
            return null;
        }

        public async Task<ReplyDto> GetRepliesByID(int ID)
        {
           var rep = await _lotionCreamDBContext.Replies.FirstOrDefaultAsync(r =>r.ReplyID == ID);
           if(rep != null)
           {
               var repDto = new ReplyDto()
               {
                   ReplyAuthorID = rep.ReplyAuthorID,
                   ReplyBody = rep.ReplyBody,
                   ReplyID = rep.ReplyID,
               };
               return repDto;
           }
           return null;
        }

        public async Task<bool> UpdateReply(int ID)
        {
           var upRep = await _lotionCreamDBContext.Replies.FindAsync(ID);
           if(upRep != null)
           {
               _lotionCreamDBContext.Entry(upRep).State = EntityState.Modified;
              await _lotionCreamDBContext.SaveChangesAsync();
              return true;
           }
           return false;
        }
        public async Task<bool> DeleteReply(int ID)
        {
            var repDel = await _lotionCreamDBContext.Replies.FindAsync(ID);
            if(repDel != null)
            {
                _lotionCreamDBContext.Remove(repDel);
                await _lotionCreamDBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ReplyServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}