using System.ComponentModel.DataAnnotations;
using LotionCream.API.Models.Products;

namespace LotionCream.API.Models.ProductListing
{
    public class ProductList
    {
        [Key]
        public int ListID {get; set;}

        public string ListName {get; set;}

        public int ProductID {get; set;}

        public virtual Product Product {get; set;}

        public string Image {get; set;}
    }
}