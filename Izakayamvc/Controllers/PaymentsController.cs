using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Permissions;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Izakayamvc.MvcApplication;

namespace Izakayamvc.Controllers
{
    public class PaymentsController : Controller
    {
        [Authorize]
        // GET: Payments/History
        public ActionResult History(SortPaymentDto dto, int? page)
        {
            const int pageSize = 13;
            var pageNumber = page ?? 1;

            var branchId = new PermissionsSetting(GetEmployeeRepo(), User.Identity.Name).BranchId;

            PagedList<OrderPaymentDto> vms = GetPaymentLists(dto, pageNumber, pageSize, branchId);

            ViewBag.TotalPages = vms.Pagination.TotalPages;
            ViewBag.CurrentPage = vms.Pagination.CurrentPage;

            //保留篩選值
            ViewBag.Name = dto.Name;
            ViewBag.StartTime = dto.StartTime ?? DateTime.Now.AddMonths(-3);
            ViewBag.EndTime = dto.EndTime ?? DateTime.Now;

            List<OrderPaymentDto> data = vms.Data.ToList();

            IEnumerable<PaymentsHistoryVm> vm = AutoMapperHelper.MapperObj.Map<IEnumerable<PaymentsHistoryVm>>(data);

            return View(vm);
        }

        [Authorize]
        //GET: Payments/Management
        public ActionResult Management()
        {
            var service = new CartSettingService(GetCartSettingRepo());

            var branchId = new PermissionsSetting(GetEmployeeRepo(), User.Identity.Name).BranchId;

            var data = service.GetAll().Where(c => c.BranchId == branchId);

            ViewBag.PaymentMethodLists = GetPaymentMethods();
            ViewBag.PaymentStatusLists = GetPaymentStatuses();
            ViewBag.MemberLists = GetMembers();
            ViewBag.CouponLists = GetCoupons();


            IEnumerable<CartSettingVm> vm = AutoMapperHelper.MapperObj.Map<IEnumerable<CartSettingVm>>(data);

            return View(vm);
        }
        private IEmployeeRepository GetEmployeeRepo()
        {
            return new DapperEmployeeRepository();
        }

        private List<CouponListDto> GetCoupons()
        {
            var service = new SeatCartService(GetSeatCartRepository());
            return service.GetCoupons();
        }

        private List<PaymentMethodDto> GetPaymentMethods()
        {
            var service = new PaymentMethodService(GetPaymentMethodRepository());
            return service.GetAll();
        }
        private List<MemberListDto> GetMembers()
        {
            var service = new SeatCartService(GetSeatCartRepository());
            return service.GetMembers();
        }
        private List<PaymentStatusDto> GetPaymentStatuses()
        {
            var service = new PaymentStatusService(GetPaymentStatusRepository());
            return service.GetAll();
        }

        private ISeatCartRepository GetSeatCartRepository()
        {
            return new DapperSeatCartRepository();
        }

        private IPaymentMethodRepository GetPaymentMethodRepository()
        {
            return new DapperPaymentMethodRepository();
        }

        private IPaymentStatusRepository GetPaymentStatusRepository()
        {
            return new DapperPaymentStatusRepository();
        }

        private ICartSettingRepository GetCartSettingRepo()
        {
            return new DapperCartSettingRepository();
        }

        //聯合篩選
        private PagedList<OrderPaymentDto> GetPaymentLists(SortPaymentDto dto, int pageNumber, int pageSize, int branchId)
        {
            var repo = GetOrderPaymentRepository();
            var service = new OrderPaymentService(repo);
            return service.Search(dto, pageNumber, pageSize, branchId);
        }

        private IOrderPaymentRepository GetOrderPaymentRepository()
        {
            return new DapperOrderPaymentRepository();
        }
    }
}