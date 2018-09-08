using LotionCream.API.Data;
using LotionCream.API.Models.Categories;
using LotionCream.API.Models.Comments;
using LotionCream.API.Models.Posts;
using LotionCream.API.Models.ProductListing;
using LotionCream.API.Models.Products;
using LotionCream.API.Models.Replies;
using LotionCream.API.Models.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Data
{
    public class LotionCreamDBContext : DbContext
    {
        public LotionCreamDBContext(DbContextOptions<LotionCreamDBContext> options)
         : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Product> Products {get; set;}

        public DbSet<ProductList> ProductLists {get; set;}
    }
}