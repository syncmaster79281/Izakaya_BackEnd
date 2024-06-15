using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels;
using Izakayamvc.ViewModels.Vms;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static ISPAN.Izakaya.Entities.ReportChartEntity;

namespace Izakayamvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly DapperReportRepository _reportRepository;

        public HomeController()
        {
            _reportRepository = new DapperReportRepository();
        }

        [Authorize]
        public ActionResult Index(string start, string end, int day = 0)
        {
            //驗證
            var userInfo = HtmlExts.GetUserData();
            ViewBag.Role = userInfo.UserData;
            var role = userInfo.UserData.Substring(Math.Max(0, userInfo.UserData.Length - 2));

            if (role != "店長")
            {
                return RedirectToAction("StartPage", "Home");
            }
            else
            {

                if (start == null || end == null)
                {
                    //根據 day 參數的值，設定 ViewBag.Filter 以在view中顯示適當的標題
                    switch (day)
                    {
                        case 0:
                            ViewBag.Filter = "Today";
                            break;
                        case -30:
                            ViewBag.Filter = "This Month";
                            break;
                        case -365:
                            ViewBag.Filter = "This Year";
                            break;
                    }
                    // 計算起始日期和結束日期，依據 day 參數決定查詢的時間範圍
                    var startTime = DateTime.Now.AddDays(day); //-365 0當天 -30當月
                    var endTime = DateTime.Now;

                    // 從報表存儲庫中獲取報表數據，包括 Line Chart 和 Website Traffic
                    // Line Chart
                    var reportsChartData = _reportRepository.GetReportData(startTime, endTime);

                    // Website Traffic
                    PaymentWayReport data = _reportRepository.GetPaymentWay(startTime, endTime);

                    //將 Line Chart 相關數據存儲在 ViewBag 中，以供視圖使用

                    ViewBag.Categories = reportsChartData.Categories;
                    ViewBag.TotalAmount = reportsChartData.TotalAmount;
                    ViewBag.NetAmount = reportsChartData.NetAmount;
                    ViewBag.Customers = reportsChartData.Customers;

                    // 計算總金額、淨額和來客數，並將格式化後的金額存儲在 ViewBag 中
                    //Customers TotalAmount NetAmount
                    ViewBag.TotalMoney = $"{Convert.ToInt32(reportsChartData.TotalAmount.Sum()):C0}";
                    ViewBag.NetMoney = $"{Convert.ToInt32(reportsChartData.NetAmount.Sum()):C0}";
                    ViewBag.People = reportsChartData.Customers.Sum();

                    // 從 HtmlExts.GetUserData() 獲取使用者資訊，並將角色存儲在 ViewBag 中

                    return View(data);
                }
                else
                {
                    var startDate = Convert.ToDateTime(start);
                    var endDate = Convert.ToDateTime(end);
                    ViewBag.StartDate = startDate;
                    ViewBag.EndDate = endDate;


                    //  從報表存儲庫中獲取報表數據，包括 Line Chart 和 Website Traffic
                    //  Line Chart
                    var reportsChartData1 = _reportRepository.GetReportData(startDate, endDate);
                    // Website Traffic
                    var data1 = _reportRepository.GetPaymentWay(startDate, endDate);

                    // 將 Line Chart 相關數據存儲在 ViewBag 中，以供視圖使用
                    ViewBag.Categories = reportsChartData1.Categories;
                    ViewBag.TotalAmount = reportsChartData1.TotalAmount;
                    ViewBag.NetAmount = reportsChartData1.NetAmount;
                    ViewBag.Customers = reportsChartData1.Customers;


                    // 計算總金額、淨額和來客數，並將格式化後的金額存儲在 ViewBag 中
                    //Customers TotalAmount NetAmount
                    ViewBag.TotalMoney = $"{Convert.ToInt32(reportsChartData1.TotalAmount.Sum()):C0}";
                    ViewBag.NetMoney = $"{Convert.ToInt32(reportsChartData1.NetAmount.Sum()):C0}";
                    ViewBag.People = reportsChartData1.Customers.Sum();

                    return View(data1);
                }
            }

        }

        [Authorize]
        public ActionResult StartPage()
        {
            return View();
        }


        public ActionResult Login()
        {
            var currentUserAccount = User.Identity.Name;
            if (!string.IsNullOrEmpty(currentUserAccount))
            {
                return RedirectToAction("StartPage", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                ValidLogin(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "帳號或密碼有誤");
                Session.Abandon();
                FormsAuthentication.SignOut();
                return View(vm);
            }

            var processResult = ProcessLogin(vm);
            Response.Cookies.Add(processResult.Cookie);
            return Redirect(processResult.ReturnUrl);
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("StartPage", "Home");
        }

        private (string ReturnUrl, HttpCookie Cookie) ProcessLogin(LoginVm vm)
        {
            var rememberMe = false;
            var account = vm.Name;
            var roles = new EmployeeService(GetEmployeeRepo()).GetEmployeeByName(vm.Name).Department;
            //ticket
            var ticket =
                new FormsAuthenticationTicket(
                    1,
                    account,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    rememberMe,
                    roles,
                    "/"
                );

            var value = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);
            var url = FormsAuthentication.GetRedirectUrl(account, true);
            return (url, cookie);
        }

        private IEmployeeRepository GetEmployeeRepo()
        {
            return new DapperEmployeeRepository();
        }

        private void ValidLogin(LoginVm vm)
        {
            var service = new EmployeeService(GetEmployeeRepo());

            var member = service.GetEmployeeByName(vm.Name);

            if (member == null)
            {
                throw new Exception("帳號或密碼有誤");
            }

            if (string.Compare(member.EmployeePassword, vm.EmployeePassword, true) != 0)
            {
                throw new Exception("帳號或密碼有誤");
            }
        }

    }
}
