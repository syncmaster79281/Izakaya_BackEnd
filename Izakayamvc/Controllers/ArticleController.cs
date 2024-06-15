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
    public class ArticleController : Controller
    {
        [Authorize]
        //編輯文章
        public ActionResult Edit(int id)
        {
            var service = new EmployeeService(GetEmployeeRepo());
            ViewBag.Employees = service.GetList();

            var data = new ArticleService(GetRepo()).GetCategoriers();
            ViewBag.Categories = data;

            ArticleVm model = LoadArticle(id); // 根據提供的 id 從資料庫中加載文章數據
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleVm model)
        {
            var service = new EmployeeService(GetEmployeeRepo());
            ViewBag.Employees = service.GetList();

            var data = new ArticleService(GetRepo()).GetCategoriers();
            ViewBag.Categories = data;

            if (!ModelState.IsValid) return View(model);
            try
            {
                UpdateArticle(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        private void UpdateArticle(ArticleVm model)
        {
            var service = new ArticleService(Update());
            ArticleDto dto = AutoMapperHelper.MapperObj.Map<ArticleDto>(model);
            service.Update(dto);
        }

        private IArticleRepository Update()
        {
            return new DapperArticleRepository();
        }

        private ArticleVm LoadArticle(int id)
        {
            var service = new ArticleService(GetRepo());
            var query = service.GetAll();

            if (id > 0)
            {
                query = query.Where(x => x.Id == id).ToList();

            }

            IEnumerable<ArticleVm> vms = AutoMapperHelper.MapperObj.Map<IEnumerable<ArticleVm>>(query);
            return vms.FirstOrDefault();
        }

        // 刪除文章
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                DeleteArticle(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "刪除操作失敗：" + ex.Message;
            }
            return RedirectToAction("Index");

        }

        private void DeleteArticle(int id)
        {
            var service = new ArticleService(Delete());
            service.Delete(id);
        }

        private IArticleRepository Delete()
        {
            return new DapperArticleRepository();
        }

        [Authorize]
        // GET: Activity/Create
        public ActionResult Create()
        {
            var service = new EmployeeService(GetEmployeeRepo());
            ViewBag.Employees = service.GetList();

            var data = new ArticleService(GetRepo()).GetCategoriers();
            ViewBag.Categories = data;

            return View();
        }

        private IEmployeeRepository GetEmployeeRepo()
        {
            return new DapperEmployeeRepository();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleVm model)
        {
            var service = new EmployeeService(GetEmployeeRepo());
            ViewBag.Employees = service.GetList();

            var data = new ArticleService(GetRepo()).GetCategoriers();
            ViewBag.Categories = data;

            // 欄位驗證
            if (!ModelState.IsValid) return View(model);
            try
            {
                // 新增紀錄
                CreateArticle(model);
                // 導轉至 index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }

        private void CreateArticle(ArticleVm model)
        {
            var service = new ArticleService(GetRepo());
            ArticleDto dto = AutoMapperHelper.MapperObj.Map<ArticleDto>(model);
            service.Create(dto);
        }

        [Authorize]
        // 查詢文章
        public ActionResult Index(string name)
        {
            List<ArticleVm> vms = GetArticles(name);
            return View(vms);
        }

        private List<ArticleVm> GetArticles(string name)
        {
            var service = new ArticleService(GetRepo());
            var query = service.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Contents.Contains(name)).ToList();
            }

            List<ArticleVm> vms = AutoMapperHelper.MapperObj.Map<List<ArticleVm>>(query);
            return vms;
        }

        private IArticleRepository GetRepo()
        {
            return new DapperArticleRepository();
        }
    }
}