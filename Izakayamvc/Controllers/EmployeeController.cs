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
    public class EmployeeController : Controller
    {
        [Authorize]
        // GET:Employee
        public ActionResult Index()
        {
            var vms = GetEmployee();
            return View(vms);
        }
        private IEnumerable<EmployeeVm> GetEmployee()
        {
            var service = new EmployeeService(GetRepo());
            var query = service.Search();
            return query.Select(x => ToVm(x));
        }
        private EmployeeVm ToVm(EmployeeDto dto)
        {
            return new EmployeeVm
            {
                Id = dto.Id,
                BranchId = dto.BranchId,
                Name = dto.Name,
                Department = dto.Department,
                Salary = dto.Salary,
                EmployeePassword = dto.EmployeePassword,
                HireDate = dto.HireDate
            };
        }
        private IEmployeeRepository GetRepo()
        {
            return new DapperEmployeeRepository();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var dto = ToDto(vm);
                var service = new EmployeeService(GetRepo());
                service.Create(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        private EmployeeDto ToDto(EmployeeVm vm)
        {
            return new EmployeeDto
            {
                Id = vm.Id,
                BranchId = vm.BranchId,
                Name = vm.Name,
                Department = vm.Department,
                Salary = vm.Salary,
                EmployeePassword = vm.EmployeePassword,
                HireDate = vm.HireDate
            };
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var service = new EmployeeService(GetRepo());
            var dto = service.Get(id);
            var vm = ToVm(dto);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeVm vm)
        {

            if (!ModelState.IsValid) return View(vm);
            try
            {
                var dto = ToDto(vm);
                var service = new EmployeeService(GetRepo());
                service.Edit(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                var service = new EmployeeService(GetRepo());
                service.Delete(id);
            }
            catch (Exception ex)
            {
                // 使用 TempData 儲存錯誤訊息，重定向後在顯示給用戶
                TempData["ErrorMessage"] = "刪除操作失敗：" + ex.Message;
            }
            return RedirectToAction("Index");
        }

        private EmployeeDto GetEmployee(int id)
        {
            var service = new EmployeeService(GetRepo());
            return service.Search().FirstOrDefault(x => x.Id == id);
        }
    }
}