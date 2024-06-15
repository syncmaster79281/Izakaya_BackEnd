using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Izakayamvc.Controllers
{
    public class HistoryReservationsController : Controller
    {
        // GET: HistoryReservations
        public ActionResult Index()
        {
            return View();
        }

        private readonly ICalendarReservationsRepository _calendarReservationsRepository;
        // GET: CalendarReservations

        public HistoryReservationsController()
        {
            _calendarReservationsRepository = new DapperCalendarReservationsRepository();
        }


        //private ISeatCartRepository GetSeatCartRepo()
        //{
        //	return new DapperSeatCartRepository();
        //}



        //private IEmployeeRepository GetEmployeeRepo()
        //{
        //	return new DapperEmployeeRepository();
        //}

        //private ICartStatusRepository GetCartStatusRepo()
        //{
        //	return new DapperCartStatusRepository();
        //}

        //private int[] GetStatusId()
        //{
        //	var service = new CartStatusService(GetCartStatusRepo());
        //	var data = service.GetAll();

        //	return data.Where(c => c.Status.Trim() == "已確認" || c.Status.Trim() == "製作中" || c.Status.Trim() == "待上菜").Select(c => c.Id).ToArray();

        //}


        //public ActionResult ReservationService()
        //{
        //	var service = new SeatCartService(GetSeatCartRepo());

        //	//取得 已確認 製作中 待上菜 的狀態編號
        //	int[] statusId = GetStatusId();

        //	var branchId = new PermissionsSetting(GetEmployeeRepo(), User.Identity.Name).BranchId;

        //	//搜尋資料
        //	var data = service.Search(statusId).Where(d => d.BranchId == branchId);

        //	IEnumerable<SeatCartVm> vm = AutoMapperHelper.MapperObj.Map<IEnumerable<SeatCartVm>>(data);

        //	return View(vm);
        //}












        [Authorize]
        // 居酒屋1號店 2024年歷史訂單 (OK)
        public ActionResult History2024Branch01Reservation()
        {
            DateTime startDate = DateTime.Parse("2024-01-01");
            DateTime endDate = DateTime.Parse("2025-01-01");
            int targetBranchId = 1; // 你想要搜尋的 BranchID

            var bookings = GetReservationsForDateBranchRange(startDate, endDate, targetBranchId);

            // 將資料傳遞給 View
            return View(bookings);
        }

        [Authorize]
        // 居酒屋1號店 2023年歷史訂單 (OK)
        public ActionResult History2023Branch01Reservation()
        {
            DateTime startDate = DateTime.Parse("2023-01-01");
            DateTime endDate = DateTime.Parse("2024-01-01");
            int targetBranchId = 1; // 你想要搜尋的 BranchID


            var bookings = GetReservationsForDateBranchRange(startDate, endDate, targetBranchId);

            // 將資料傳遞給 View
            return View(bookings);
        }

        [Authorize]
        // 居酒屋2號店 2024年歷史訂單 (OK)
        public ActionResult History2024Branch02Reservation()
        {
            DateTime startDate = DateTime.Parse("2024-01-01");
            DateTime endDate = DateTime.Parse("2025-01-01");
            int targetBranchId = 2; // 你想要搜尋的 BranchID

            var bookings = GetReservationsForDateBranchRange(startDate, endDate, targetBranchId);

            // 將資料傳遞給 View
            return View(bookings);
        }

        [Authorize]
        // 居酒屋2號店 2023年歷史訂單 (OK)
        public ActionResult History2023Branch02Reservation()
        {
            DateTime startDate = DateTime.Parse("2023-01-01");
            DateTime endDate = DateTime.Parse("2024-01-01");
            int targetBranchId = 2; // 你想要搜尋的 BranchID

            var bookings = GetReservationsForDateBranchRange(startDate, endDate, targetBranchId);

            // 將資料傳遞給 View
            return View(bookings);

        }

        // 公用公式
        private List<ReservationVM> GetReservationsForDateBranchRange(DateTime startDate, DateTime endDate, int targetBranchId)
        {
            // 使用 _calendarReservationsRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                        .Where(c => c.BranchId == targetBranchId)
                                                        .Select(c => new ReservationVM
                                                        {
                                                            Name = c.Name,
                                                            Qty = c.Qty,
                                                            Tel = c.Tel,
                                                            ReservationTime = c.ReservationTime,
                                                            Status = c.Status,
                                                            BranchId = c.BranchId,
                                                            SeatId = c.SeatId
                                                        })
                                                        .ToList(); // 重要：在這裡轉換成列表，以免多次查詢資料庫
            return bookings;
        }







        [Authorize]
        // 2024年歷史訂單 (OK)
        public ActionResult History2024TotalReservation()
        {
            DateTime startDate = DateTime.Parse("2024-01-01");
            DateTime endDate = DateTime.Parse("2025-01-01");

            var bookings = GetReservationsForDateRange(startDate, endDate);

            return View(bookings);
        }
        [Authorize]
        // 2023年歷史訂單 (OK)
        public ActionResult History2023TotalReservation()
        {
            DateTime startDate = DateTime.Parse("2023-01-01");
            DateTime endDate = DateTime.Parse("2024-01-01");

            var bookings = GetReservationsForDateRange(startDate, endDate);

            return View(bookings);
        }

        [Authorize]
        //  全歷史訂單 (OK)
        public ActionResult HistoryTotalReservation()
        {
            // 假設日期區間為 '2023-01-01' 到 '2099-12-31'
            DateTime startDate = DateTime.Parse("2023-01-01");
            DateTime endDate = DateTime.Parse("2100-01-01");

            var bookings = GetReservationsForDateRange(startDate, endDate);

            return View(bookings);
        }

        // 公用程式
        private List<ReservationVM> GetReservationsForDateRange(DateTime startDate, DateTime endDate)
        {
            // 使用 _calendarReservationsRepository 來取得資料
            return _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                    .Select(c => new ReservationVM
                                                    {
                                                        Name = c.Name,
                                                        Qty = c.Qty,
                                                        Tel = c.Tel,
                                                        ReservationTime = c.ReservationTime,
                                                        Status = c.Status,
                                                        BranchId = c.BranchId,
                                                        SeatId = c.SeatId
                                                    })
                                                    .ToList();
        }
    }
}