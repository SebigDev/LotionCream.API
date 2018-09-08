using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Categories;
using LotionCream.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.Categories
{
    public class CategoryServices : ICategoryServices
    {
        private readonly LotionCreamDBContext _lotionCreamDBContext;
        public CategoryServices(LotionCreamDBContext lotionCreamDBContext)
        {
            _lotionCreamDBContext = lotionCreamDBContext;
        }
      

        public async Task AddCategory(CategoryDto category)
        {
           var create = new Category()
           {
               CategoryID = category.CategoryID,
               CategoryName = category.CategoryName,
           };
             await _lotionCreamDBContext.AddAsync(create);
            await _lotionCreamDBContext.SaveChangesAsync(); 
        }
        public async Task<bool> Delete(int ID)
        {
            var getCat = await _lotionCreamDBContext.Categories.FindAsync(ID);
           _lotionCreamDBContext.Remove(getCat);
           await _lotionCreamDBContext.SaveChangesAsync();
           return true ;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
           var allCategories = await _lotionCreamDBContext.Categories.ToListAsync();
           var  categoryToReturn = new List<CategoryDto>();
           if(allCategories.Count > 0){
                categoryToReturn.AddRange(allCategories.OrderBy(c => c.CategoryID).Select(x => new CategoryDto(){
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName
                }));
                return categoryToReturn;
           }
           return null;
        }

        public async Task<CategoryDto> GetCategoryByID(int ID)
        {
           var catByID = await _lotionCreamDBContext.Categories.FirstOrDefaultAsync(x=>x.CategoryID == ID);
          
           if(catByID != null)
           {
                var categoryDto = new CategoryDto()
                {
                    CategoryID = catByID.CategoryID,
                    CategoryName = catByID.CategoryName
                };
                return categoryDto; 
           }
           return null;
        }

        public async Task<bool> Update(int ID)
        {
      
            var catToUpdate = await _lotionCreamDBContext.Categories.FindAsync(ID);
            if(catToUpdate != null)
            {
                 _lotionCreamDBContext.Entry(catToUpdate).State = EntityState.Modified;
                 await _lotionCreamDBContext.SaveChangesAsync();
                 return true;
            }
            return false;

            
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CategoryServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
}