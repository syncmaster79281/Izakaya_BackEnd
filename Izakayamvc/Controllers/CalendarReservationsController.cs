using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;


namespace Izakayamvc.Controllers
{
    public class CalendarReservationsController : Controller
    {
        private readonly ICalendarReservationsRepository _calendarReservationsRepository;
        // GET: CalendarReservations

        public CalendarReservationsController()
        {
            _calendarReservationsRepository = new DapperCalendarReservationsRepository();
        }


        [Authorize]
        // 刪除點選資料(TodayReservation)
        public ActionResult DeleteTodayReservation(int reservationId)
        {
            // 使用 _calendarReservationsRepository 來刪除預約
            bool isDeleted = _calendarReservationsRepository.DeleteReservation(reservationId);

            if (isDeleted)
            {   // 若成功刪除，重新導向至原畫面 TodayReservation
                return RedirectToAction("TodayReservation");
            }
            else
            {   // 若刪除失敗，可以返回一個錯誤頁面或其他處理方式
                return View("怎麼會刪除失敗呢!!");
            }
        }


        //GET: CalendarReservations/Edit/5     returnUrl 複製前一頁網址
        [Authorize]
        public ActionResult EditReservation(int id, string returnUrl)
        {

            // 根據 ID 從數據庫中獲取要編輯的數據
            var reservation = _calendarReservationsRepository.GetReservationById(id);

            if (reservation == false)
            {
                return HttpNotFound();
            }

            var entity = _calendarReservationsRepository.GetReservation(id);

            var vm = new ReservationVM
            {
                Id = entity.Id,
                Name = entity.Name,
                Qty = entity.Qty,
                Tel = entity.Tel,
                ReservationTime = entity.ReservationTime,
                Status = entity.Status,
                BranchId = entity.BranchId,
                SeatId = entity.SeatId
            };
            // 前一頁網址
            ViewBag.ReturnUrl = returnUrl;

            // 將數據傳遞給 View
            return View(vm);
        }

        // POST: CalendarReservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReservation(ReservationVM reservation, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                // 更新數據庫中的數據
                bool updateSuccess = _calendarReservationsRepository.UpdateReservation(ToEntity(reservation));

                if (updateSuccess)
                {   // 編輯成功，導到原畫面
                    //return RedirectToAction("前一頁網址");
                    return Redirect(returnUrl);
                }
                else
                {   // 編輯失敗，顯示錯誤信息
                    ModelState.AddModelError("", "怎麼會編輯失敗呢!!");
                }
            }
            // 如果 ModelState 驗證失敗，返回編輯頁面
            return View(reservation);
        }

        private ReservationEntity ToEntity(ReservationVM reservation)
        {
            return new ReservationEntity
            {
                Id = reservation.Id,
                Name = reservation.Name,
                Qty = reservation.Qty,
                Tel = reservation.Tel,
                ReservationTime = reservation.ReservationTime,
                Status = reservation.Status,
                BranchId = reservation.BranchId,
                SeatId = reservation.SeatId
            };
        }


        // 刪除資料(TheDay)
        [Authorize]
        public ActionResult DeleteTheDayReservation(int reservationId, string returnUrl)
        {
            // 使用 _calendarReservationsRepository 來刪除預約
            bool isDeleted = _calendarReservationsRepository.DeleteReservation(reservationId);

            // 前一頁網址
            ViewBag.ReturnUrl = returnUrl;

            if (isDeleted)
            {   // 若成功刪除，重新導向至原畫面 TodayReservation
                return Redirect(returnUrl);
            }
            else
            {   // 若刪除失敗，可以返回一個錯誤頁面或其他處理方式
                return View("怎麼會刪除失敗呢!!");
            }
        }


        [Authorize]
        // 新增訂位資料
        public ActionResult CreateReservation(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParse(date, out parsedDate))
            {
                ViewBag.Date = parsedDate;
            }
            else
            {
                // 處理無法解析的日期，例如設置一個默認值或返回錯誤消息
                ViewBag.Date = DateTime.Now; // 或者選擇其他合適的處理方式
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReservation(ReservationVM newReservation)
        {
            if (ModelState.IsValid)
            {

                CreateData(newReservation);

                // 在這裡處理和保存 newReservation 到數據庫
                // 例如，使用 Entity Framework:
                // _calendarReservationsRepository.CreateReservation(newReservation);
                // 這裡需要根據你的 Repository 實現進行相應的操作

                // 可以重定向到詳細信息頁面或其他需要的頁面
                return RedirectToAction("Calendar");
            }

            return View(newReservation);
        }

        public void CreateData(ReservationVM reservation)
        {
            var data = new ReservationEntity
            {
                Id = reservation.Id,
                Name = reservation.Name,
                Qty = reservation.Qty,
                Tel = reservation.Tel,
                ReservationTime = reservation.ReservationTime,
                Status = reservation.Status,
                SeatId = reservation.SeatId,
                BranchId = reservation.BranchId
            };
            _calendarReservationsRepository.CreateReservation(data);
        }

        // 1號店新增 限定BranchID = 1
        [Authorize]
        public ActionResult CreateBranch01Reservation(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParse(date, out parsedDate))
            {
                ViewBag.Date = parsedDate;
            }
            else
            {
                // 處理無法解析的日期，例如設置一個默認值或返回錯誤消息
                ViewBag.Date = DateTime.Now; // 或者選擇其他合適的處理方式
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranch01Reservation(ReservationVM newReservation)
        {
            if (ModelState.IsValid)
            {
                if (newReservation.BranchId != 1)
                {
                    ModelState.AddModelError("BranchId", "店名號必須為台北");
                    return View(newReservation);
                }

                CreateBranch01Data(newReservation);

                // 在這裡處理和保存 newReservation 到數據庫
                // 例如，使用 Entity Framework:
                // _calendarReservationsRepository.CreateReservation(newReservation);
                // 這裡需要根據你的 Repository 實現進行相應的操作

                // 可以重定向到詳細信息頁面或其他需要的頁面
                return RedirectToAction("CalendarBranch01");
            }
            return View(newReservation);
        }

        public void CreateBranch01Data(ReservationVM reservation)
        {
            var data = new ReservationEntity
            {
                Id = reservation.Id,
                Name = reservation.Name,
                Qty = reservation.Qty,
                Tel = reservation.Tel,
                ReservationTime = reservation.ReservationTime,
                Status = reservation.Status,
                SeatId = reservation.SeatId,
                BranchId = reservation.BranchId,
                FillUp = reservation.FillUp
            };
            _calendarReservationsRepository.CreateReservation(data);
        }

        [Authorize]
        // 2號店新增 限定BranchID = 2
        public ActionResult CreateBranch02Reservation(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParse(date, out parsedDate))
            {
                ViewBag.Date = parsedDate;
            }
            else
            {
                // 處理無法解析的日期，例如設置一個默認值或返回錯誤消息
                ViewBag.Date = DateTime.Now; // 或者選擇其他合適的處理方式
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranch02Reservation(ReservationVM newReservation)
        {
            if (ModelState.IsValid)
            {
                if (newReservation.BranchId != 2)
                {
                    ModelState.AddModelError("BranchId", "店名號必須為台中");
                    return View(newReservation);
                }

                CreateBranch02Data(newReservation);

                // 在這裡處理和保存 newReservation 到數據庫
                // 例如，使用 Entity Framework:
                // _calendarReservationsRepository.CreateReservation(newReservation);
                // 這裡需要根據你的 Repository 實現進行相應的操作

                // 可以重定向到詳細信息頁面或其他需要的頁面
                return RedirectToAction("CalendarBranch02");
            }

            return View(newReservation);
        }

        public void CreateBranch02Data(ReservationVM reservation)
        {
            var data = new ReservationEntity
            {
                Id = reservation.Id,
                Name = reservation.Name,
                Qty = reservation.Qty,
                Tel = reservation.Tel,
                ReservationTime = reservation.ReservationTime,
                Status = reservation.Status,
                SeatId = reservation.SeatId,
                BranchId = reservation.BranchId
            };
            _calendarReservationsRepository.CreateReservation(data);
        }
        [Authorize]
        // 本日訂單
        public ActionResult TodayReservation()
        {
            // 用DateTime.Today抓今日日期 減1分鐘避免凌晨12點前後兩天出現
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1).AddMinutes(-1);

            // 使用 _empReservationRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                            .Select(c => new ReservationVM
                                                            {
                                                                Name = c.Name,
                                                                Qty = c.Qty,
                                                                Tel = c.Tel,
                                                                ReservationTime = c.ReservationTime,
                                                                Status = c.Status,
                                                                Id = c.Id,
                                                                SeatId = c.SeatId,
                                                                BranchId = c.BranchId
                                                            });

            // 將資料傳遞給 View
            return View(bookings);
        }
        [Authorize]
        // 1號店本日訂單
        public ActionResult TodayBranch01Reservation()
        {
            // 用DateTime.Today抓今日日期 減1分鐘避免凌晨12點前後兩天出現
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1).AddMinutes(-1);
            int targetBranchId = 1; // 你想要查詢的 BranchID

            // 使用 _empReservationRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                            .Where(c => c.BranchId == targetBranchId)
                                                            .Select(c => new ReservationVM
                                                            {
                                                                Name = c.Name,
                                                                Qty = c.Qty,
                                                                Tel = c.Tel,
                                                                ReservationTime = c.ReservationTime,
                                                                Status = c.Status,
                                                                Id = c.Id,
                                                                SeatId = c.SeatId,
                                                                BranchId = c.BranchId,
                                                                FillUp = c.FillUp
                                                            });
            // 將資料傳遞給 View
            return View(bookings);
        }
        [Authorize]
        // 2號店本日訂單
        public ActionResult TodayBranch02Reservation()
        {
            // 用DateTime.Today抓今日日期 減1分鐘避免凌晨12點前後兩天出現
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1).AddMinutes(-1);
            int targetBranchId = 2; // 你想要查詢的 BranchID

            // 使用 _empReservationRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                            .Where(c => c.BranchId == targetBranchId)
                                                            .Select(c => new ReservationVM
                                                            {
                                                                Name = c.Name,
                                                                Qty = c.Qty,
                                                                Tel = c.Tel,
                                                                ReservationTime = c.ReservationTime,
                                                                Status = c.Status,
                                                                Id = c.Id,
                                                                SeatId = c.SeatId,
                                                                BranchId = c.BranchId
                                                            });

            // 將資料傳遞給 View
            return View(bookings);
        }

        [Authorize]
        // 行事曆樣板
        public ActionResult Calendar()
        {
            return View();
        }
        [Authorize]
        public ActionResult CalendarBranch01()
        {
            return View();
        }
        [Authorize]
        public ActionResult CalendarBranch02()
        {
            return View();
        }

        [Authorize]
        // 由行事曆點選後連接到該日期的訂單
        // 由TheDay的View直接啟動會錯誤,因為要從行事曆點選的日期回傳數據
        public ActionResult TheDay(string date)
        {
            // 從View傳遞選擇的日期
            var dateDt = DateTime.Parse(date);
            DateTime startDate = dateDt;
            DateTime endDate = dateDt.AddDays(1).AddMinutes(-1);

            // 使用 _empReservationRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                            .Select(c => new ReservationVM
                                                            {
                                                                Name = c.Name,
                                                                Qty = c.Qty,
                                                                Tel = c.Tel,
                                                                ReservationTime = c.ReservationTime,
                                                                Status = c.Status,
                                                                Id = c.Id,
                                                                BranchId = c.BranchId,
                                                                SeatId = c.SeatId
                                                            });

            // 將資料傳遞給 View
            return View(bookings);
        }

        [Authorize]
        // 由行事曆點選後連接到該日期的訂單(1號店) OK
        public ActionResult TheDayBranch01(string date)
        {
            // 從View傳遞選擇的日期
            var dateDt = DateTime.Parse(date);
            DateTime startDate = dateDt;
            DateTime endDate = dateDt.AddDays(1).AddMinutes(-1);
            int targetBranchId = 1; // 你想要查詢的 BranchID

            // 使用 _empReservationRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                            .Where(c => c.BranchId == targetBranchId)
                                                            .Select(c => new ReservationVM
                                                            {
                                                                Name = c.Name,
                                                                Qty = c.Qty,
                                                                Tel = c.Tel,
                                                                ReservationTime = c.ReservationTime,
                                                                Status = c.Status,
                                                                Id = c.Id,
                                                                BranchId = c.BranchId,
                                                                SeatId = c.SeatId
                                                            });
            // 將資料傳遞給 View
            return View(bookings);
        }
        [Authorize]
        // 由行事曆點選後連接到該日期的訂單(2號店) OK
        public ActionResult TheDayBranch02(string date)
        {
            // 從View傳遞選擇的日期
            var dateDt = DateTime.Parse(date);
            DateTime startDate = dateDt;
            DateTime endDate = dateDt.AddDays(1).AddMinutes(-1);
            int targetBranchId = 2; // 你想要查詢的 BranchID

            // 使用 _empReservationRepository 來取得資料
            var bookings = _calendarReservationsRepository.GetReservationsForDateRange(startDate, endDate)
                                                            .Where(c => c.BranchId == targetBranchId)
                                                            .Select(c => new ReservationVM
                                                            {
                                                                Name = c.Name,
                                                                Qty = c.Qty,
                                                                Tel = c.Tel,
                                                                ReservationTime = c.ReservationTime,
                                                                Status = c.Status,
                                                                Id = c.Id,
                                                                BranchId = c.BranchId,
                                                                SeatId = c.SeatId
                                                            });
            // 將資料傳遞給 View
            return View(bookings);
        }







        [Authorize]
        [HttpPost]
        // 更改點選就位狀態(TodayReservation)
        public ActionResult UpdateFillUp(int reservationId)
        {
            // 使用 _calendarReservationsRepository 來刪除預約
            bool isUpdated = _calendarReservationsRepository.UpdateFillUp(reservationId);

            if (isUpdated)
            {   // 若成功刪除，重新導向至原畫面 TodayReservation
                return RedirectToAction("TodayReservation");
            }
            else
            {   // 若刪除失敗，可以返回一個錯誤頁面或其他處理方式
                return View("怎麼會更改失敗呢!!");
            }
        }


        [Authorize]
        [HttpPost]
        // 更改點選就位狀態(TodayReservation)
        public ActionResult ClearFillUp(int reservationId)
        {
            // 使用 _calendarReservationsRepository 來刪除預約
            bool isUpdated = _calendarReservationsRepository.ClearFillUp(reservationId);

            if (isUpdated)
            {   // 若成功刪除，重新導向至原畫面 TodayReservation
                return RedirectToAction("TodayReservation");
            }
            else
            {   // 若刪除失敗，可以返回一個錯誤頁面或其他處理方式
                return View("怎麼會更改失敗呢!!");
            }
        }

    }
}










