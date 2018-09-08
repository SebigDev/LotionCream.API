using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.Products;

namespace LotionCream.API.Services.Products
{
    public interface IProductServices : IDisposable
    {
         Task<IEnumerable<ProductDto>> GetAllProducts();
         Task<IEnumerable<ProductDto>> GetAllProductsByCreatorID(long ID);
         Task<IEnumerable<ProductDto>> GetAllProductsByCategoryID(int ID);
         Task<ProductDto> GetProductByID(int ID);
         Task CreateProduct(ProductDto product);
         Task<bool> UpdateProduct(int ID);
         Task<bool> DeleteProduct(int ID);

    }
}