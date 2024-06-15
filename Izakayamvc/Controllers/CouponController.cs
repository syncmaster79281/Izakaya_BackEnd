using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Izakayamvc.MvcApplication;

namespace Izakayamvc.Controllers
{
    public class CouponController : Controller
    {
        // GET: Conpon
        [Authorize]
        public ActionResult Index(string name)
        {
            var vm = GetCoupon(name);
            return View(vm);
        }

        private List<CouponVm> GetCoupon(string name)
        {
            var service = new CouponService(new DapperCouponRepository());
            var couponEntities = service.GetAll();
            if (!string.IsNullOrEmpty(name))
            {
                couponEntities = couponEntities.Where(x => x.Name.Contains(name) || x.Condition.Contains(name) || x.Description.Contains(name)).ToList();
            }
            List<CouponVm> couponVms = AutoMapperHelper.MapperObj.Map<List<CouponVm>>(couponEntities);
            return couponVms;
        }

        [Authorize]
        // GET: Conpon/Create
        public ActionResult Create()
        {
            var service = new ProductService(GetProductRepo());
            var products = service.GetProducts();
            ViewBag.Products = products;
            return View();
        }

        // POST: Conpon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CouponVm vm)
        {
            var service = new ProductService(GetProductRepo());
            var products = service.GetProducts();
            ViewBag.Products = products;
            //欄位驗證
            if (!ModelState.IsValid) return View(vm);

            try
            {
                //新增紀錄
                CreateCoupon(vm);
                //導轉至index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        private void CreateCoupon(CouponVm vm)
        {
            var service = new CouponService(new DapperCouponRepository());
            CouponDto dto = AutoMapperHelper.MapperObj.Map<CouponDto>(vm);
            service.Create(dto);
        }

        private IProductRepository GetProductRepo()
        {
            return new ProductDapperRepository();
        }

        private CouponVm LoadCoupon(int id)
        {
            var service = new CouponService(GetRepo());
            var query = service.Get(id);


            return new CouponVm
            {
                Id = query.Id,
                BranchId = query.BranchId,
                Name = query.Name,
                ProductId = query.ProductId,
                TypeId = query.TypeId,
                Condition = query.Condition,
                DiscountMethod = query.DiscountMethod,
                StartTime = query.StartTime,
                EndTime = query.EndTime,
                IsUsed = query.IsUsed,
                Description = query.Description
            };
        }

        private IConponRepository GetRepo()
        {
            return new DapperCouponRepository();
        }


        [Authorize]
        // GET: Conpon/Edit/5
        public ActionResult Edit(int id)
        {
            var service = new ProductService(GetProductRepo());
            var products = service.GetProducts();
            ViewBag.Products = products;
            CouponVm vm = LoadCoupon(id);
            return View(vm);
        }

        // POST: Conpon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CouponVm vm)
        {
            var service = new ProductService(GetProductRepo());
            var products = service.GetProducts();
            ViewBag.Products = products;

            //欄位驗證
            if (!ModelState.IsValid) return View(vm);
            try
            {
                //新增紀錄
                UpdateCoupon(vm);
                //導轉至index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

        }
        private void UpdateCoupon(CouponVm vm)
        {
            var service = new CouponService(new DapperCouponRepository());
            CouponDto dto = AutoMapperHelper.MapperObj.Map<CouponDto>(vm);
            service.Update(dto);
        }

        [Authorize]
        // GET: Conpon/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                DeleteCoupon(id);
            }
            catch (Exception ex)
            {
                // 使用 TempData 儲存錯誤訊息，重定向後在顯示給用戶
                TempData["ErrorMessage"] = "刪除操作失敗：" + ex.Message;
            }
            return RedirectToAction("Index");
        }

        private void DeleteCoupon(int id)
        {
            var service = new CouponService(Delete());
            service.Delete(id);
        }

        private IConponRepository Delete()
        {
            return new DapperCouponRepository();
        }
    }
}
