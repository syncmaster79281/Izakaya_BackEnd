using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Izakayamvc.Controllers
{
    public class ProductCategoriesController : Controller
    {
        [Authorize]
        // GET: ProductCategory
        public ActionResult Index(string name)
        {
            var vms = GetList(name);
            return View(vms);
        }

        private IEnumerable<ProductInSameCategoryVm> GetProducts(int id)
        {
            var service = new ProductService(GetProductRepo());
            var datas = service.GetProductsInSameCategory(id);
            return datas.Select(x => new ProductInSameCategoryVm { Id = x.Id, Name = x.Name, UnitPrice = x.UnitPrice }); ;
        }

        private void DeleteCategory(int id)
        {
            var service = new ProductCategoryService(GetCategoryRepo());
            service.Delete(id);
        }

        private void EditCategory(ProductCategoryVm vm)
        {
            var dto = ToDto(vm);
            var service = new ProductCategoryService(GetCategoryRepo());
            service.Update(dto);
        }

        private ProductCategoryVm GetOne(int id)
        {
            var service = new ProductCategoryService(GetCategoryRepo());
            var dto = service.Get(id);
            return ToVm(dto);
        }
        private void CreateProduct(ProductCategoryVm vm)
        {
            var service = new ProductCategoryService(GetCategoryRepo());
            service.Create(ToDto(vm));
        }
        private IEnumerable<ProductCategoryVm> GetList(string name)
        {
            if (string.IsNullOrEmpty(name)) name = "";
            var service = new ProductCategoryService(GetCategoryRepo());
            var query = service.Search(name);
            return query.Select(x => ToVm(x));
        }
        private ProductCategoryVm ToVm(ProductCategoryDto dto)
        {
            return new ProductCategoryVm { Id = dto.Id, Name = dto.Name };
        }
        private ProductCategoryDto ToDto(ProductCategoryVm vm)
        {
            return new ProductCategoryDto { Id = vm.Id, Name = vm.Name };
        }
        private IProductCategoryRepository GetCategoryRepo()
        {
            return new ProductCategoryDapperRepository();
        }
        private IProductRepository GetProductRepo()
        {
            return new ProductDapperRepository();
        }
    }
}