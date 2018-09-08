using System.Collections.Generic;
using LotionCream.API.Models.Posts;

namespace LotionCream.API.Models.Categories
{
    public class Category
    {
        public int CategoryID {get; set;}
        public string CategoryName {get; set;}

        public virtual ICollection<Post> CategoryPosts {get; set;}
    }
}