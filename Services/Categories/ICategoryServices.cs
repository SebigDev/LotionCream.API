using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotionCream.API.Models.Categories;
using LotionCream.API.Models.Dtos;

namespace LotionCream.API.Services.Categories
{
    public interface ICategoryServices : IDisposable
    {
         Task<IEnumerable<CategoryDto>> GetAllCategories();
         Task<CategoryDto> GetCategoryByID(int ID);
         Task<bool> Update(int ID);
         Task<bool> Delete(int ID);
         Task AddCategory(CategoryDto category);
    }
}