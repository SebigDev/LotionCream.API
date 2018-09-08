using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.ProductListing;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.ProductLists
{
     public class ProductListService : IProductListService
     {
          private readonly LotionCreamDBContext _lotionCreamDbContext;

          public ProductListService(LotionCreamDBContext lotionCreamDbContext )
          {
              _lotionCreamDbContext = lotionCreamDbContext;
          }


        public async Task<IEnumerable<ProductListDto>> GetAllProductLists()
        {
            var allProductList = await _lotionCreamDbContext.ProductLists.ToListAsync();
            if(allProductList.Count() > 0)
            {
                var allProductDto = new List<ProductListDto>();

                allProductDto.AddRange(allProductList.OrderBy(a => a.ListID).Select(l => new ProductListDto()
                {
                    ListID = l.ListID,
                    ListName = l.ListName,
                    ProductID = l.ProductID,
                    Image = l.Image
                }));
                return allProductDto;
            }
            return null;
        }

        public async Task<ProductListDto> GetAllProductListsByID(int ID)
        {
            var productListById = await _lotionCreamDbContext.ProductLists
                                    .Where(x => x.ListID == ID).FirstOrDefaultAsync();
            if(productListById != null)
            {
                var productListByIdDto = new ProductListDto()
                {
                    ListID = productListById.ListID,
                    ListName = productListById.ListName,
                    ProductID = productListById.Product.ProductID,
                    Image = productListById.Image
                };
                return productListByIdDto;
            }
            return null;
        }

        public async Task<IEnumerable<ProductListDto>> GetAllProductListsByProductID(int ID)
        {
            var allProductListByOroductID = await _lotionCreamDbContext.ProductLists
                                        .Where(a => a.ProductID == ID).ToListAsync();
            if(allProductListByOroductID.Count() > 0)
            {
                var allProductDto = new List<ProductListDto>();

                allProductDto.AddRange(allProductListByOroductID.OrderBy(a => a.ListID)
                .Select(l => new ProductListDto()
                {
                    ListID = l.ListID,
                    ListName = l.ListName,
                    ProductID = l.ProductID,
                    Image = l.Image
                }));
                return allProductDto;
            }
            return null;
        }

        public async Task Create(ProductListDto model)
        {
            if(model != null)
            {
               var newProductList = new ProductList()
                {
                    ListName = model.ListName,
                    ProductID = model.ProductID,
                    Image = model.Image,
                };
                await _lotionCreamDbContext.AddAsync(newProductList);
                await _lotionCreamDbContext.SaveChangesAsync();
            }
           
        }


        public async Task<bool> Delete(int ID)
        {
            var delete = await _lotionCreamDbContext.ProductLists.FindAsync(ID);
            if(delete != null)
            {
                _lotionCreamDbContext.Remove(delete);
                await _lotionCreamDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Update(ProductListDto model)
        {
           var update = await _lotionCreamDbContext.ProductLists.FindAsync(model.ListID);
           if(update != null)
           {
               update.ListName = model.ListName;
               update.Image = model.Image;

               _lotionCreamDbContext.Entry(update).State = EntityState.Modified;
               await _lotionCreamDbContext.SaveChangesAsync();
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
        // ~ProductListService() {
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