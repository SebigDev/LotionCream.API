using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Dtos;

namespace LotionCream.API.Services.ProductLists
{
    public interface IProductListService : IDisposable
    {
         Task Create(ProductListDto model);

         Task<bool> Update(ProductListDto model);

         Task<bool> Delete(int ID);

         Task<IEnumerable<ProductListDto>> GetAllProductLists();

         Task<IEnumerable<ProductListDto>> GetAllProductListsByProductID(int ID);

         Task<ProductListDto> GetAllProductListsByID(int ID);

    }
}