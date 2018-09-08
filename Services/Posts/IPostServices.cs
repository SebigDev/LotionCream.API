using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.Posts;

namespace LotionCream.API.Services.Posts
{
    public interface IPostServices : IDisposable
    {
        Task<IEnumerable<PostDto>> GetAllPost();
         Task<PostDto> GetPostByPostId(long ID);
         Task<IEnumerable<PostDto>> GetPostByPostDate(DateTime date);
         Task<IEnumerable<PostDto>> GetAllPostsByCategoryID(int ID);
         Task<IEnumerable<PostDto>> GetAllPostsByCategoryName(string name);
         Task<IEnumerable<PostDto>> GetPostByCategoryAndDateCreated(string category, DateTime date);
         Task<bool> UpdatePost(long ID);
         Task<bool> DeletePost(long ID);
         Task<Post> CreatePost(PostDto post);
    }
}