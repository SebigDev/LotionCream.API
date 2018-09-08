using LotionCream.API.Models.Products;

namespace LotionCream.API.Models.Dtos
{
    public class ProductListDto
    {
        public int ListID {get; set;}

        public string ListName {get; set;}

        public int ProductID {get; set;}

        public string Image {get; set;}
    }
}