using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Izakayamvc.Controllers
{
    public class ProductsController : Controller
    {
        [Authorize]
        // GET: Products
        public ActionResult Index(string name)
        {
            var vms = GetProducts(name);
            return View(vms);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            //todo
            var vm = GetOneProduct(id);
            return View(vm);
        }
        [Authorize]
        public ActionResult Create()
        {
            var categories = GetCategory().ToList();
            categories.Insert(0, new ProductCategoryDto { Id = -1, Name = "請選擇商品分類" });
            //ViewBag.Categories = categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.Categories = new SelectList(categories, "Id", "Name", -1);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                CreateProduct(vm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(vm);
        }
        private void CreateProduct(ProductCreateVm vm)
        {
            var service = new ProductService(GetProductRepo());
            //if (Image != null)
            //{
            //string path = Server.MapPath("/img");
            //string newFileName = new UploadFileHelper().UploadImgFile(Image, path, true);
            //vm.Image = newFileName;
            //}
            //else
            //{
            //    vm.Image = UploadFileHelper.NoImage;
            //}
            var dto = new ProductDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Image = vm.Image,
                DisplayOrder = vm.DisplayOrder,
                IsLaunched = vm.IsLaunched,
                UnitPrice = vm.UnitPrice,
                Present = vm.Present,
                ProductCategory = new ProductCategoryDto
                {
                    Id = vm.CategoryId,
                }
            };
            service.Create(dto);
        }

        private ProductEditVm GetOneProduct(int id)
        {
            var productService = new ProductService(GetProductRepo());
            ProductDto product = productService.Get(id);

            var categories = GetCategory().ToList();
            var category = categories.First(x => x.Id == product.ProductCategory.Id);
            categories.Remove(category);
            categories.Insert(0, category);
            ViewBag.Categories = categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

            return new ProductEditVm
            {
                Id = product.Id,
                CategoryId = product.ProductCategory.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Image = product.Image,
                Present = product.Present,
                DisplayOrder = product.DisplayOrder,
                IsLaunched = product.IsLaunched,
            };
        }
        private IEnumerable<ProductCategoryDto> GetCategory()
        {
            var categoryService = new ProductCategoryService(GetCategoryRepo());
            return categoryService.Search(string.Empty);
        }

        private IEnumerable<ProductVm> GetProducts(string name)
        {
            if (string.IsNullOrEmpty(name)) name = "";
            var service = new ProductService(GetProductRepo());
            IEnumerable<ProductDto> query = service.Search(name);
            return query.Select(x => new ProductVm
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.ProductCategory.Name,
                DisplayOrder = x.DisplayOrder,
                Image = x.Image,
                IsLaunched = x.IsLaunched,
                UnitPrice = x.UnitPrice
            });
        }
        private ProductVm ToVm(ProductDto dto)
        {
            return new ProductVm
            {
                Id = dto.Id,
                Name = dto.Name,
                CategoryName = dto.ProductCategory.Name,
                DisplayOrder = dto.DisplayOrder,
                Image = dto.Image,
                IsLaunched = dto.IsLaunched,
                UnitPrice = dto.UnitPrice
            };
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
}