using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Izakayamvc.Controllers
{
    public class ProductsExcelApiController : ApiController
    {
        // GET: api/ProductsExcelApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProductsExcelApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProductsExcelApi
        public string Post(IEnumerable<ProductExcel> products)
        {
            try
            {
                var categoryService = new ProductCategoryService(GetCategoryRepo());
                var productService = new ProductService(GetProductRepo());
                var categorylistInDb = categoryService.Search("");
                var categoryDictionary = categorylistInDb.ToDictionary(x => x.Name.ToLower(), x => x.Id);
                foreach (var product in products)
                {
                    var dto = new ProductDto
                    {
                        Name = product.productName,
                        ProductCategory = new ProductCategoryDto
                        {
                            Name = product.productCategory,
                            Id = categoryDictionary.ContainsKey(product.productCategory.ToLower()) ? categoryDictionary[product.productCategory] : -1
                        },
                        DisplayOrder = product.DisplayOrder,
                        UnitPrice = product.UnitPrice,
                        Present = product.Present
                    };
                    productService.Create(dto);
                }

                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // PUT: api/ProductsExcelApi/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ProductsExcelApi/5
        public void Delete(int id)
        {
        }
        private IProductRepository GetProductRepo()
        {
            return new ProductDapperRepository();
        }
        private IProductCategoryRepository GetCategoryRepo()
        {
            return new ProductCategoryDapperRepository();
        }
    }
    public class ProductExcel
    {
        public int UnitPrice { get; set; }
        public int DisplayOrder { get; set; }
        public string productName { get; set; }
        public string productCategory { get; set; }
        public string Present { get; set; }

    }
}
