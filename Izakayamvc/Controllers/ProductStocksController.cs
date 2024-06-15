using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Vms;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Izakayamvc.Controllers
{
    public class ProductStocksController : Controller
    {
        //[Authorize]
        //// GET: ProductStocks
        //public ActionResult Index()
        //{
        //	var vms = GetProductStocks();
        //	return View(vms);
        //}
        private IEnumerable<ProductStockVm> GetProductStocks()
        {
            var service = new ProductStockService(GetRepo());
            var query = service.Search();
            return query.Select(x => ToVm(x));
        }
        private ProductStockVm ToVm(ProductStockDto dto)
        {
            return new ProductStockVm
            {
                Id = dto.Id,
                ProductName = dto.Product.Name,
                BranchName = dto.BranchName,
                SafetyStock = dto.SafetyStock,
                Stock = dto.Stock,
                MaxAlertStock = dto.MaxAlertStock,
                ProductCategoryName = dto.ProductCategory.Name
            };
        }
        private IProductStockRepository GetRepo()
        {
            return new ProductStockDapperRepository();
        }
    }
}