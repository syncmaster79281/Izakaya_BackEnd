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
    public class ProductCategoryApiController : ApiController
    {
        // GET: api/ProductCategoryApi
        //使用中
        public IEnumerable<ProductCategoryDto> Get()
        {
            var service = new ProductCategoryService(GetRepo());
            var categiries = service.Search("").ToList();
            return categiries;
        }

        // GET: api/ProductCategoryApi/5
        //使用中
        public string Get(int id)
        {
            try
            {
                var service = new ProductCategoryService(GetRepo());
                var category = service.Get(id);
                return category.Name;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        // POST: api/ProductCategoryApi
        //使用中
        public string Post([FromBody] ProductCategoryVm vm)
        {
            try
            {
                var service = new ProductCategoryService(GetRepo());
                var dto = new ProductCategoryDto
                {
                    Id = vm.Id,
                    Name = vm.Name
                };
                service.Update(dto);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        // PUT: api/ProductCategoryApi/5
        //使用中
        public string Put(string name)
        {
            try
            {
                var service = new ProductCategoryService(GetRepo());
                service.Create(new ProductCategoryDto { Name = name });
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // DELETE: api/ProductCategoryApi/5
        //使用中
        public string Delete(int id)
        {
            try
            {
                var service = new ProductCategoryService(GetRepo());
                service.Delete(id);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private IProductCategoryRepository GetRepo()
        {
            return new ProductCategoryDapperRepository();
        }
    }
}
