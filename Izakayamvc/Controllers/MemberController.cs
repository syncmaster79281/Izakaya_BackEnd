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
    public class MembersController : Controller
    {
        // GET: Members
        [Authorize]
        public ActionResult Index()
        {
            var vms = GetMembers();
            return View(vms);
        }
        private IEnumerable<MemberVm> GetMembers()
        {
            var service = new MemberService(GetRepo());
            var query = service.Search().OrderByDescending(x => x.Id);
            return query.Select(x => ToVm(x));
        }
        private MemberVm ToVm(MemberDto dto)
        {
            return new MemberVm
            {
                Id = dto.Id,
                Name = dto.Name,
                Account = dto.Account,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                Points = dto.Points,
                AuthenticatioCode = dto.AuthenticatioCode,
                Birthday = dto.Birthday.ToString("yyyy/MM/dd"),
            };
        }
        private IMemberRepository GetRepo()
        {
            return new DapperMemberRepository();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberVm vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = ToDto(vm);
                    var service = new MemberService(GetRepo());
                    service.Create(dto);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(vm);
                }
            }
            return View(vm);
        }

        private MemberDto ToDto(MemberVm vm)
        {
            return new MemberDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Account = vm.Account,
                Password = vm.Password,
                Phone = vm.Phone,
                Email = vm.Email,
                Points = vm.Points,
                AuthenticatioCode = vm.AuthenticatioCode,
                Birthday = Convert.ToDateTime(vm.Birthday),
            };
        }
        private MemberDto GetMember(int id)
        {
            var service = new MemberService(GetRepo());
            return service.Search().FirstOrDefault(x => x.Id == id);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var service = new MemberService(GetRepo());
            var dto = service.Get(id);
            var vm = ToVm(dto);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberVm vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = ToDto(vm);
                    var service = new MemberService(GetRepo());
                    service.Edit(dto);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(vm);
                }
            }
            return View(vm);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                var service = new MemberService(GetRepo());
                service.Delete(id);
            }
            catch (Exception ex)
            {
                // 使用 TempData 儲存錯誤訊息，重定向後在顯示給用戶
                TempData["ErrorMessage"] = "刪除操作失敗：" + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}