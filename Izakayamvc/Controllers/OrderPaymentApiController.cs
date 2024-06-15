using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Izakayamvc.Controllers
{
    [RoutePrefix("api/OrderPaymentApiController")]
    public class OrderPaymentApiController : ApiController
    {
        [HttpGet]
        [Route("GetDetails")]
        public IHttpActionResult GetDetails(int combinedOrderId)
        {
            try
            {
                // 檢查訂單是否存在
                (bool orderExists, DateTime createTime) = OrderExists(combinedOrderId);

                if (!orderExists)
                {
                    return NotFound(); // 返回HTTP 404錯誤
                }
                //取得資料
                var infos = GetOrderDetails(combinedOrderId, createTime);

                return Ok(new { success = true, data = infos });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message, error = ex.Message });
            }
        }
        //取得資料
        private OrderDetail GetOrderDetails(int combinedOrderId, DateTime createTime)
        {
            var repo = GetOrderDetailRepository();
            var service = new OrderDetailService(repo);

            List<OrderItem> items = service.GetDetails(combinedOrderId).Select(x => new OrderItem { Name = x.ProductName, Price = x.UnitPrice, Qty = x.Qty }).ToList();

            var orderDetail = new OrderDetail(combinedOrderId.ToString(), createTime, items);
            return orderDetail;
        }
        // 檢查訂單是否存在
        private (bool, DateTime) OrderExists(int combinedOrderId)
        {
            var repo = GetCombinedOrderRepository();
            var service = new CombinedOrderService(repo);
            var order = service.Get(combinedOrderId);
            bool isExits = order != null;
            return (isExits, isExits ? order.CreateTime : DateTime.MinValue);
        }

        //CombinedOrder
        private ICombinedOrderRepository GetCombinedOrderRepository()
        {
            return new DapperCombinedOrderRepository();
        }

        //OrderDetail
        private IOrderDetailRepository GetOrderDetailRepository()
        {
            return new DapperOrderDetailRepository();
        }
    }
    public class OrderDetail
    {
        public OrderDetail(string orderNumber, DateTime date, List<OrderItem> items)
        {
            OrderNumber = orderNumber;
            Date = date;
            Items = items;
            Total = TotalPrice;
        }
        public string OrderNumber { get; }
        public DateTime Date { get; }
        public int TotalPrice => Items.Sum(x => x.Price * x.Qty);
        public int Total { get; }
        public List<OrderItem> Items { get; }
    }


}
