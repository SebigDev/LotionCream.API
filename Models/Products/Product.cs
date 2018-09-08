using LotionCream.API.Models.Categories;
using LotionCream.API.Models.UserManagement;

namespace LotionCream.API.Models.Products
{
    public class Product
    {
        public int ProductID{get; set;}
        public string ProductName{get; set;}

        public long CreatorID {get; set;}
        public User CreatedBy {get; set;}
        public int CategoryID {get; set;}
        public Category Category {get; set;}
    }
}