using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.Products
{
    public class ProductServices : IProductServices
    {
        private readonly LotionCreamDBContext _lotionCreamDBContext;
        public ProductServices( LotionCreamDBContext lotionCreamDBContext)
        {
            _lotionCreamDBContext = lotionCreamDBContext;
        }
        public async Task CreateProduct(ProductDto product)
        {
            var create = new Product()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CreatorID = product.CreatorID,
                CategoryID = product.CategoryID
            };
            await _lotionCreamDBContext.AddAsync(create);
            await _lotionCreamDBContext.SaveChangesAsync();
        }

        public async Task<ProductDto> GetProductByID(int ID)
        {
            var getProduct = await _lotionCreamDBContext.Products.FindAsync(ID);
        
            if(getProduct != null)
            {
                var productDto = new ProductDto()
                {
                    ProductID = getProduct.ProductID,
                    ProductName = getProduct.ProductName,
                    CreatorID = getProduct.CreatorID,
                    CategoryID = getProduct.CategoryID
                };
                return productDto;
            }
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var getProductByCreator = await _lotionCreamDBContext.Products.ToListAsync();
           var productDto = new List<ProductDto>();
           if(getProductByCreator.Count() > 0)
           {
               productDto.AddRange(getProductByCreator.OrderBy(x =>x.ProductID).Select(p => new ProductDto()
               {
                   ProductID  = p.ProductID,
                   ProductName = p.ProductName,
                   CreatorID = p.CreatorID,
                   CategoryID = p.CategoryID
               }));
               return productDto;
           }
           return null;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsByCreatorID(long ID)
        {
           var getProductByCreator = await _lotionCreamDBContext.Products.Where(g => g.CreatorID == ID).ToListAsync();
           if(getProductByCreator.Count() > 0)
           {
                var productDto = new List<ProductDto>();
               productDto.AddRange(getProductByCreator.OrderBy(x =>x.ProductID).Select(p => new ProductDto()
               {
                   ProductID  = p.ProductID,
                   ProductName = p.ProductName,
                   CreatorID = p.CreatorID,
                   CategoryID = p.CategoryID
               }));
               return productDto;
           }
           return null;
        }
        
        public async Task<IEnumerable<ProductDto>> GetAllProductsByCategoryID(int ID)
        {
            var allProCat = await _lotionCreamDBContext.Products.Where(c =>c.CategoryID==ID).ToListAsync();
            if(allProCat.Count() > 0)
            {
                  var allProCatDto = new List<ProductDto>();
                  allProCatDto.AddRange(allProCat.OrderBy(o => o.ProductID).Select(p => new ProductDto()
                  {
                        ProductID  = p.ProductID,
                        ProductName = p.ProductName,
                        CreatorID = p.CreatorID,
                        CategoryID = p.CategoryID
                  }));
                  return allProCatDto;
            }
            return null;
        }

        public async Task<bool> UpdateProduct(int ID)
        {
            var update = await _lotionCreamDBContext.Products.FindAsync(ID);
            if(update != null)
            {
                var newUpdate = new Product()
                {
                    ProductName = update.ProductName
                };
                 _lotionCreamDBContext.Entry(newUpdate).State = EntityState.Modified;
                 await _lotionCreamDBContext.SaveChangesAsync();
                 return true;
            }
            return false;
        }
         public async Task<bool> DeleteProduct(int ID)
        {
            var delete = await _lotionCreamDBContext.Products.FindAsync(ID);
            if(delete != null)
            {
                _lotionCreamDBContext.Remove(delete);
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
        // ~ProductServices() {
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