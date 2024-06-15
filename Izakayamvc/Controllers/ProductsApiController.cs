using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Izakayamvc.Controllers
{
    public class ProductsApiController : ApiController
    {
        // GET: api/ProductsApi
        //取得所有商品
        public IEnumerable<ProductDto> Get()
        {
            var service = new ProductService(GetRepo());
            var query = service.Search("");
            return query;
        }

        // GET: api/ProductsApi/5
        public string Get(int id)
        {
            return "value";
        }

        //index頁面 修改上下架
        public string Get(int id, bool launch)
        {
            try
            {
                var service = new ProductService(GetRepo());
                var productInDb = service.Get(id);
                if (productInDb == null) throw new ArgumentNullException($"此Id: {id} 收不到商品");
                if (launch)
                {
                    productInDb.IsLaunched = true;
                }
                else
                {
                    productInDb.IsLaunched = false;
                }
                service.Update(productInDb);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        // POST: api/ProductsApi
        public IEnumerable<ProductPagination> Post(SearchDataDto dto)
        {
            try
            {
                var service = new ProductService(GetRepo());
                int counts = service.GetCount(dto);
                int totalPages = counts % dto.PageSize == 0
                    ? counts / dto.PageSize
                    : (int)(double)counts / dto.PageSize + 1;
                var products = service.Search(dto);
                return products.Select(x => new ProductPagination
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.ProductCategory.Name,
                    DisplayOrder = x.DisplayOrder,
                    Image = x.Image,
                    ImageUrl = x.ImageUrl,
                    IsLaunched = x.IsLaunched,
                    UnitPrice = x.UnitPrice,
                    TotalPage = totalPages
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        // PUT: api/ProductsApi
        //Edit頁面使用 編輯商品
        public string Put(ProductEditVm vm)
        {

            try
            {
                var service = new ProductService(GetRepo());
                var dto = new ProductDto
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    UnitPrice = vm.UnitPrice,
                    DisplayOrder = vm.DisplayOrder,
                    IsLaunched = vm.IsLaunched,
                    Present = vm.Present,
                    ProductCategory = new ProductCategoryDto
                    {
                        Id = vm.CategoryId
                    }
                };
                service.Update(dto);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // DELETE: api/ProductsApi/5
        //Edit頁面使用 
        public string Delete(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException($"Id: {id}不可小於1");
                var service = new ProductService(GetRepo());
                var productInDb = service.Get(id);
                if (productInDb == null) throw new ArgumentNullException("查無該產品,無法刪除");
                service.Delete(id);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private IProductRepository GetRepo()
        {
            return new ProductDapperRepository();
        }
    }

}
