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
    public class MealsController : Controller
    {
        [Authorize]
        // GET: Meals
        public ActionResult FoodService()
        {
            var service = new SeatCartService(GetSeatCartRepo());

            //取得 已確認 製作中 待上菜 的狀態編號
            int[] statusId = GetStatusId();

            var branchId = new PermissionsSetting(GetEmployeeRepo(), User.Identity.Name).BranchId;

            //查詢資料
            var data = service.Search(statusId).Where(d => d.BranchId == branchId);

            IEnumerable<SeatCartVm> vm = AutoMapperHelper.MapperObj.Map<IEnumerable<SeatCartVm>>(data);

            return View(vm);
        }

        private IEmployeeRepository GetEmployeeRepo()
        {
            return new DapperEmployeeRepository();
        }

        private int[] GetStatusId()
        {
            var service = new CartStatusService(GetCartStatusRepo());
            var data = service.GetAll();

            return data.Where(c => c.Status.Trim() == "已確認" || c.Status.Trim() == "製作中" || c.Status.Trim() == "待上菜").Select(c => c.Id).ToArray();

        }

        private ISeatCartRepository GetSeatCartRepo()
        {
            return new DapperSeatCartRepository();
        }
        private ICartStatusRepository GetCartStatusRepo()
        {
            return new DapperCartStatusRepository();
        }
    }
}