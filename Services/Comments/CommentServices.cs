using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Comments;
using LotionCream.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.Comments
{
    public class CommentServices : ICommentServices
    {
        private readonly LotionCreamDBContext _lotioncreamDBcontext;
        public CommentServices(LotionCreamDBContext lotioncreamDBcontext)
        {
            _lotioncreamDBcontext = lotioncreamDBcontext;
        }
        public async Task<Comment> CreateComment(CommentDto comment)
        {
            var newCommentDto = new Comment()
            {
               CommentAuthorID = comment.CommentAuthorID,
               CommentBody = comment.CommentBody,
               CommentID = comment.CommentID,
               PostID = comment.PostID
            };
            await _lotioncreamDBcontext.AddAsync(newCommentDto);
            await _lotioncreamDBcontext.SaveChangesAsync();
            return newCommentDto;
            
        }

        public async Task<CommentDto> GetCommentByID(long ID)
        {
            var comment = await _lotioncreamDBcontext.Comments.FirstOrDefaultAsync(c => c.CommentID == ID);
            if(comment != null)
            {
              var commentDto = new CommentDto()
              {
                  CommentAuthorID = comment.CommentAuthor.UserID,
                  CommentBody = comment.CommentBody,
                  CommentID = comment.CommentID,
                  CommentPicture = comment.CommentPicture,
                  PostID = comment.PostID
              };
              return commentDto;
            }
            return null;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentByPostID(long ID)
        {
           var allComment = await _lotioncreamDBcontext.Comments.Where(a =>a.PostID == ID).ToListAsync();
           var allCommentDto = new List<CommentDto>();
           if(allComment.Count() > 0)
           {
               allCommentDto.AddRange(allComment.OrderBy(c =>c.CommentDate).Select(n => new CommentDto()
               {
                    CommentAuthorID = n.CommentAuthor.UserID,
                    CommentBody = n.CommentBody,
                    CommentID = n.CommentID,
                    CommentPicture = n.CommentPicture,
                   PostID = n.CommentPost.PostID
               }));
               return allCommentDto;
           }
           return null;
        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
          var allComment = await _lotioncreamDBcontext.Comments.ToListAsync();
           var allCommentDto = new List<CommentDto>();
           if(allComment.Count() > 0)
           {
               allCommentDto.AddRange(allComment.OrderBy(c =>c.CommentDate).Select(n => new CommentDto()
               {
                    CommentAuthorID = n.CommentAuthor.UserID,
                    CommentBody = n.CommentBody,
                    CommentID = n.CommentID,
                    CommentPicture = n.CommentPicture
               }));
               return allCommentDto;
           }
           return null;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsByAuthorID(long ID)
        {
           var allComment = await _lotioncreamDBcontext.Comments.Where(a =>a.CommentAuthorID == ID).ToListAsync();
           var allCommentDto = new List<CommentDto>();
           if(allComment.Count() > 0)
           {
               allCommentDto.AddRange(allComment.OrderBy(c =>c.CommentDate).Select(n => new CommentDto()
               {
                    CommentAuthorID = n.CommentAuthor.UserID,
                    CommentBody = n.CommentBody,
                    CommentID = n.CommentID,
                    CommentPicture = n.CommentPicture
               }));
               return allCommentDto;
           }
           return null;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsByDateAndPostName(DateTime date, string post)
        {
           var allComment = await _lotioncreamDBcontext.Comments
                        .Where(a =>a.CommentDate == date && a.CommentPost.PostTitle == post).ToListAsync();
           var allCommentDto = new List<CommentDto>();
           if(allComment.Count() > 0)
           {
               allCommentDto.AddRange(allComment.OrderBy(c =>c.CommentDate).Select(n => new CommentDto()
               {
                    CommentAuthorID = n.CommentAuthor.UserID,
                    CommentBody = n.CommentBody,
                    CommentID = n.CommentID,
                    CommentPicture = n.CommentPicture
               }));
               return allCommentDto;
           }
           return null;
        }

        public async Task<bool> UpdateComment(long ID)
        {
            var update = await _lotioncreamDBcontext.Comments.FindAsync(ID);
            if(update != null)
            {
                _lotioncreamDBcontext.Entry(update).State = EntityState.Modified;
                await _lotioncreamDBcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteComment(long ID)
        {
             var update = await _lotioncreamDBcontext.Comments.FindAsync(ID);
            if(update != null)
            {
                _lotioncreamDBcontext.Remove(update);
                await _lotioncreamDBcontext.SaveChangesAsync();
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
        // ~CommentServices() {
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