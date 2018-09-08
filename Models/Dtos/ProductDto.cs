namespace LotionCream.API.Models.Dtos
{
    public class ProductDto
    {
        public int ProductID{get; set;}
        public string ProductName{get; set;}

        public long CreatorID {get; set;}
        //public User CreatedBy {get; set;}
        public int CategoryID {get; set;}
       // public Category Category {get; set;}
    }
}