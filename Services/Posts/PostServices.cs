using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.Posts
{
    public class PostServices : IPostServices
    {
        private readonly LotionCreamDBContext _lotioncreamDBcontext;
     
     public PostServices(LotionCreamDBContext lotioncreamDBcontext)
     {
         _lotioncreamDBcontext = lotioncreamDBcontext;
     }
    
        
        public async Task<Post> CreatePost(PostDto post)
        {
           var postDto = new Post()
           {
               PostID = post.PostID,
               PostTitle = post.PostTitle,
               PostBody = post.PostBody,
               PostAuthorID = post.PostAuthorID,
               CategoryID = post.CategoryID,
               PostPhoto = post.PostPhoto
           };
           await _lotioncreamDBcontext.AddAsync(postDto);
           await _lotioncreamDBcontext.SaveChangesAsync();
           return postDto;
        }

        public async Task<IEnumerable<PostDto>> GetAllPost()
        {
            var allPosts = await _lotioncreamDBcontext.Posts.ToListAsync();
            var postDto = new List<PostDto>();
            if(allPosts.Count() > 0)
            {
                postDto.AddRange(allPosts.OrderBy(x =>x.CreatedOn).Select(post => new PostDto(){
                    PostAuthorID = post.PostAuthorID,
                    PostBody = post.PostBody,
                    PostID = post.PostID,
                    PostCategory = post.PostCategory.CategoryName,
                    PostTitle = post.PostTitle,
                    PostPhoto = post.PostPhoto,
                }));
                return postDto;
            }
            return null;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsByCategoryID(int ID)
        {
            var allPosts = await _lotioncreamDBcontext.Posts.Where(x=>x.PostCategory.CategoryID == ID).ToListAsync();
            var postDto = new List<PostDto>();
            if(allPosts.Count() > 0)
            {
                postDto.AddRange(allPosts.OrderBy(x =>x.CreatedOn).Select(post => new PostDto(){
                    PostAuthorID = post.PostAuthorID,
                    PostBody = post.PostBody,
                    PostID = post.PostID,
                    PostCategory = post.PostCategory.CategoryName,
                    PostTitle = post.PostTitle,
                    PostPhoto = post.PostPhoto,
                }));
                return postDto;
            }
            return null;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsByCategoryName(string name)
        {
           var allPosts = await _lotioncreamDBcontext.Posts.Where(x=>x.PostCategory.CategoryName == name).ToListAsync();
            var postDto = new List<PostDto>();
            if(allPosts.Count() > 0)
            {
                postDto.AddRange(allPosts.OrderBy(x =>x.CreatedOn).Select(post => new PostDto(){
                    PostAuthorID = post.PostAuthorID,
                    PostBody = post.PostBody,
                    PostID = post.PostID,
                    PostCategory = post.PostCategory.CategoryName,
                    PostTitle = post.PostTitle,
                    PostPhoto = post.PostPhoto,
                }));
                return postDto;
            }
            return null;
        }

        public async Task<IEnumerable<PostDto>> GetPostByPostDate(DateTime date)
        {
            var allPosts = await _lotioncreamDBcontext.Posts.Where(x=>x.CreatedOn == date).ToListAsync();
            var postDto = new List<PostDto>();
            if(allPosts.Count() > 0)
            {
                postDto.AddRange(allPosts.OrderBy(x =>x.CreatedOn).Select(post => new PostDto(){
                    PostAuthorID = post.PostAuthorID,
                    PostBody = post.PostBody,
                    PostID = post.PostID,
                    PostCategory = post.PostCategory.CategoryName,
                    PostTitle = post.PostTitle,
                    PostPhoto = post.PostPhoto,
                }));
                return postDto;
            }
            return null;
        }
         public async Task<IEnumerable<PostDto>> GetPostByCategoryAndDateCreated(string category, DateTime date)
        {
            var allPosts = await _lotioncreamDBcontext.Posts
                .Where(x=>x.CreatedOn == date && x.PostCategory.CategoryName == category ).ToListAsync();
            var postDto = new List<PostDto>();
            if(allPosts.Count() > 0)
            {
                postDto.AddRange(allPosts.OrderBy(x =>x.CreatedOn).Select(post => new PostDto(){
                    PostAuthorID = post.PostAuthorID,
                    PostBody = post.PostBody,
                    PostID = post.PostID,
                    PostCategory = post.PostCategory.CategoryName,
                    PostTitle = post.PostTitle,
                    PostPhoto = post.PostPhoto,
                }));
                return postDto;
            }
            return null;
        }

        public async Task<PostDto> GetPostByPostId(long ID)
        {
           var post = await _lotioncreamDBcontext.Posts.FirstOrDefaultAsync(p=>p.PostID == ID);
           if(post != null)
           {
              var postDto = new PostDto()
              {
                PostAuthorID = post.PostAuthorID,
                PostBody = post.PostBody,
                PostID = post.PostID,
                PostCategory = post.PostCategory.CategoryName,
                PostTitle = post.PostTitle,
                PostPhoto = post.PostPhoto,
              };
              return postDto;
           }
           return null;

        }

        public async Task<bool> UpdatePost(long ID)
        {
           var postToUpdate = await _lotioncreamDBcontext.Posts.FindAsync(ID);
           if(postToUpdate != null)
           {
               _lotioncreamDBcontext.Entry(postToUpdate).State = EntityState.Modified;
               await _lotioncreamDBcontext.SaveChangesAsync();
               return true;
           }
           return false;
        }
        public async Task<bool> DeletePost(long ID)
        {
            var postToDelete = await _lotioncreamDBcontext.Posts.FindAsync(ID);
            if(postToDelete != null)
            {
                _lotioncreamDBcontext.Remove(postToDelete);
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
        // ~PostServices() {
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