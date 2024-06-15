using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Permissions;
using Izakayamvc.ViewModels.Vms;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Izakayamvc.MvcApplication;

namespace Izakayamvc.Controllers
{
    public class CartSettingsController : Controller
    {
        [Authorize]
        // GET: CartSettings
        public ActionResult SetSeatTime()
        {
            var service = new CartSettingService(GetCartSettingRepo());

            var branchId = new PermissionsSetting(GetEmployeeRepo(), User.Identity.Name).BranchId;

            var data = service.GetAll().Where(c => c.BranchId == branchId);

            ViewBag.BranchId = branchId;

            IEnumerable<CartSettingVm> vm = AutoMapperHelper.MapperObj.Map<IEnumerable<CartSettingVm>>(data);

            return View(vm);
        }
        //取得座位資訊
        private ICartSettingRepository GetCartSettingRepo()
        {
            return new DapperCartSettingRepository();
        }
        //取得員工資訊
        private IEmployeeRepository GetEmployeeRepo()
        {
            return new DapperEmployeeRepository();
        }
    }
}