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
    public class ActivityController : Controller
    {
        // GET: Activity
        [Authorize]
        public ActionResult Index(string name)
        {
            var vm = GetActivity(name);
            return View(vm);
        }

        private List<ActivityVm> GetActivity(string name)
        {
            var service = new ActivityService(new DapperActivityRepository());
            var ativityEntities = service.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                ativityEntities = ativityEntities.Where(x => x.Name.Contains(name) || x.Type.Contains(name) || x.Description.Contains(name)).ToList();
            }

            List<ActivityVm> activityVms = AutoMapperHelper.MapperObj.Map<List<ActivityVm>>(ativityEntities);
            return activityVms;
        }


        private IProductRepository GetProductRepo()
        {
            return new ProductDapperRepository();
        }

        [Authorize]
        // GET: Activity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityVm vm)
        {
            //欄位驗證
            if (!ModelState.IsValid) return View(vm);

            try
            {
                // 新增紀錄
                CreateActivity(vm);

                //導轉至index

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(vm);
            }
        }

        private void CreateActivity(ActivityVm vm)
        {
            var service = new ActivityService(new DapperActivityRepository());
            ActivityDto dto = AutoMapperHelper.MapperObj.Map<ActivityDto>(vm);
            service.Create(dto);
        }

        [Authorize]
        // GET: Activity/Edit/5
        public ActionResult Edit(int id)
        {
            ActivityVm vm = LoadActivity(id);
            return View(vm);
        }

        // POST: Activity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ActivityVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                UpdateActivity(vm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        private ActivityVm LoadActivity(int id)
        {
            var service = new ActivityService(GetRepo());
            var query = service.GetAll();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id).ToList();
            }
            IEnumerable<ActivityVm> vms = AutoMapperHelper.MapperObj.Map<IEnumerable<ActivityVm>>(query);
            return vms.FirstOrDefault();

        }

        private IActivityRepository GetRepo()
        {
            return new DapperActivityRepository();
        }

        private void UpdateActivity(ActivityVm vm)
        {
            var service = new ActivityService(new DapperActivityRepository());
            ActivityDto dto = AutoMapperHelper.MapperObj.Map<ActivityDto>(vm);
            service.Update(dto);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                DeleteActivity(id);
            }
            catch (Exception ex)
            {
                // 使用 TempData 儲存錯誤訊息，重定向後在顯示給用戶
                TempData["ErrorMessage"] = "刪除操作失敗：" + ex.Message;
            }
            return RedirectToAction("Index");
        }


        private void DeleteActivity(int id)
        {
            var service = new ActivityService(Delete());
            service.Delete(id);
        }
        private IActivityRepository Delete()
        {
            return new DapperActivityRepository();
        }

    }
}
