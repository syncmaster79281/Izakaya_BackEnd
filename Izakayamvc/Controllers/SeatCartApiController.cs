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
    [RoutePrefix("api/SeatCartApiController")]
    public class SeatCartApiController : ApiController
    {
        // POST: api/SeatCartApiController/UpdateStatus
        [HttpPost]
        [Route("UpdateStatus")]
        public IHttpActionResult UpdateStatus(StatusDto dto)
        {
            try
            {
                //取得狀態Id
                int id = dto.Id;
                int statusId = GetCartStatusId(dto.Status);

                var service = new SeatCartService(GetSeatCartRepo());

                service.Update(id, statusId);

                var data = service.Get(id);

                return Ok(new { success = true, message = $"{data.ProductName}\n{data.CartStatus}" });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = "無法更新狀態", error = ex.Message });
            }
        }

        // POST: api/SeatCartApiController/UpdateAllStatus
        [HttpPost]
        [Route("UpdateAllStatus")]
        public IHttpActionResult UpdateAllStatus(CartStatusChangeDto dto)
        {
            try
            {
                //取得狀態Id
                int oldStatusId = dto.OldStatusId;
                int newStatusId = dto.NewStatusId;

                var service = new SeatCartService(GetSeatCartRepo());

                service.UpdateAll(oldStatusId, newStatusId);


                return Ok(new { success = true, message = $"狀態已更新" });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = "無法更新狀態", error = ex.Message });
            }
        }



        // GET api/SeatCartApiController/GetMealsByTable/{seatId}
        [HttpGet]
        [Route("GetSeatCartList/{seatId}")]
        public IHttpActionResult GetSeatCartList(string seatId)
        {
            try
            {
                if (!int.TryParse(seatId, out int id))
                {
                    return BadRequest("座位ID格式錯誤");
                }

                var mealDetail = GetMealDetailsBySeatId(id);

                if (mealDetail == null)
                {
                    return NotFound();
                }

                return Ok(new { success = true, content = mealDetail });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message });
            }
        }

        // POST: api/SeatCartApiController/Checkout
        [HttpPost]
        [Route("Checkout")]
        public IHttpActionResult Checkout([FromBody] CheckoutDto checkoutDto)
        {
            try
            {
                if (!checkoutDto.SeatIds.Any() || checkoutDto.PaymentMethodId == 0 || checkoutDto.PaymentStatusId == 0 || checkoutDto.MemberId == 0 || checkoutDto.CouponId == 0)
                {
                    return BadRequest("資料錯誤");
                }

                var data = CheckOutBill(checkoutDto);
                return Ok(new { success = true, content = data });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex, error = ex.Message });
            }
        }

        //結帳
        private ShowInfoDto CheckOutBill(CheckoutDto checkoutDto)
        {
            var combinedOrderId = CreateCombinedOrder();

            var orderIds = new List<int>();
            var seatsNames = new List<string>();
            var productItem = new List<OrderItem>();
            var totalAmount = 0;

            foreach (var seatId in checkoutDto.SeatIds)
            {
                //更新待上菜
                UpdateMealsStatus(seatId, "待上菜");

                var data = GetMealDetailsBySeatId(seatId);
                int subtotal = data.Sum(x => x.UnitPrice * x.Qty);
                var createTime = DateTime.Now;
                totalAmount += subtotal;
                OrderDto dtoOrder = new OrderDto
                {
                    SeatId = seatId,
                    CombinedOrderId = combinedOrderId,
                    CreateTime = createTime,
                    Subtotal = subtotal,
                };

                int orderId = CreateOrder(dtoOrder);
                string seatName = GetSeatName(seatId);

                orderIds.Add(orderId);
                seatsNames.Add(seatName);

                foreach (var item in data)
                {
                    int productId = GetProductId(item.ProductName);

                    OrderDetailDto dto = new OrderDetailDto
                    {
                        OrderId = orderId,
                        ProductId = productId,
                        UnitPrice = item.UnitPrice,
                        Qty = item.Qty,
                    };

                    CreateOrderDetails(dto);

                    OrderItem orderItem = new OrderItem
                    {
                        Name = item.ProductName,
                        Price = item.UnitPrice,
                        Qty = item.Qty
                    };

                    productItem.Add(orderItem);
                }

            }

            (int discount, int netAmount) = CheckPayment(combinedOrderId, checkoutDto.PaymentMethodId, checkoutDto.PaymentStatusId, totalAmount, checkoutDto.CouponId);

            OrderPaymentDto bill = new OrderPaymentDto
            {
                MemberId = checkoutDto.MemberId,
                CombinedOrderId = combinedOrderId,
                PaymentMethodId = checkoutDto.PaymentMethodId,
                PaymentStatusId = checkoutDto.PaymentStatusId,
                TotalAmount = totalAmount,
                Discount = discount,
                NetAmount = netAmount,
                PaymentTime = DateTime.Now,
            };

            CreatePayment(bill);

            DeleteSeats(checkoutDto.SeatIds); //刪除購物車

            ShowInfoDto result = new ShowInfoDto
            {
                CombinedOrderId = combinedOrderId,
                OrderIds = orderIds,
                SeatsName = seatsNames,
                CreateTime = DateTime.Now,
                Items = productItem,
            };

            return result;

        }

        private void DeleteSeats(List<int> Ids)
        {
            var service = new SeatCartService(GetSeatCartRepo());
            foreach (var seatId in Ids)
            {
                service.Delete(seatId);
            }
        }

        //折扣邏輯
        private (int, int) CheckPayment(int combinedOrderId, int paymentMethodId, int paymentStatusId, int totalAmount, int couponId)
        {
            int discount = 0;  //折扣
            int netAmount = totalAmount; //實付

            if (paymentMethodId == 1 && paymentStatusId == 2 && couponId == 1)  //現金付款成功 滿500 打九折 
            {
                discount = Convert.ToInt32(Math.Ceiling(totalAmount * 0.1));                         //打九折
                netAmount = totalAmount - discount;
            }

            if (paymentMethodId == 4 && paymentStatusId == 2)  //餐券 付款成功
            {
                discount = 0;
                netAmount = totalAmount;
                couponId = 999;
            }

            OrderDiscountDto dto = new OrderDiscountDto
            {
                OrderPaymentId = combinedOrderId,
                CouponId = couponId,
                AppliedValue = Convert.ToDecimal(discount),
            };

            CreateOrderDiscount(dto);

            return (discount, netAmount);
        }

        //新增折扣明細
        private void CreateOrderDiscount(OrderDiscountDto dto)
        {
            var service = new OrderDiscountService(GetOrderDiscountRepo());
            service.Create(dto);
        }

        private IOrderDiscountRepository GetOrderDiscountRepo()
        {
            return new DapperOrderDiscountRepository();
        }

        //更新全部待上菜
        private void UpdateMealsStatus(int seatId, string status)
        {
            var service = new SeatCartService(GetSeatCartRepo());
            int statusId = GetCartStatusId(status);
            int completeId = GetCartStatusId("已完成");
            service.UpdateAll(seatId, statusId, completeId);
        }



        private string GetSeatName(int seatId)
        {
            var service = new SeatCartService(GetSeatCartRepo());
            return service.GetSeatName(seatId);
        }
        private int GetProductId(string productName)
        {
            var service = new SeatCartService(GetSeatCartRepo());
            return service.GetProductId(productName);
        }

        //創立合併訂單
        private int CreateCombinedOrder()
        {
            var service = new CombinedOrderService(GetCombinedOrderRepo());

            CombinedOrderDto dto = new CombinedOrderDto
            {
                CreateTime = DateTime.Now,
            };

            int combinedOrderId = service.Create(dto);

            return combinedOrderId;
        }

        //創立訂單
        private int CreateOrder(OrderDto dto)
        {
            var service = new OrderService(GetOrderRepo());

            int orderId = service.Create(dto);

            return orderId;
        }

        //創立明細
        private void CreateOrderDetails(OrderDetailDto dto)
        {
            var service = new OrderDetailService(GetOrderDetailRepo());
            service.Create(dto);
        }

        //創立結帳單
        private void CreatePayment(OrderPaymentDto dto)
        {
            var service = new OrderPaymentService(GetOrderPaymentRepo());
            service.Create(dto);
        }


        private IOrderPaymentRepository GetOrderPaymentRepo()
        {
            return new DapperOrderPaymentRepository();
        }


        private IOrderDetailRepository GetOrderDetailRepo()
        {
            return new DapperOrderDetailRepository();
        }

        private IOrderRepository GetOrderRepo()
        {
            return new DapperOrderRepository();
        }

        private ICombinedOrderRepository GetCombinedOrderRepo()
        {
            return new DapperCombinedOrderRepository();
        }

        private List<MealDetailDto> GetMealDetailsBySeatId(int seatId)
        {
            var service = new SeatCartService(GetSeatCartRepo());

            string status = "已完成";
            int statusId = GetCartStatusId(status);

            var data = service.SearchMealList(seatId, statusId);

            return data;
        }


        private int GetCartStatusId(string status)
        {
            var service = new CartStatusService(GetCartStatusRepo());
            return service.Get(status).Id;

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
    public class CheckoutDto
    {
        public List<int> SeatIds { get; set; }
        public int MemberId { get; set; }
        public int PaymentMethodId { get; set; }
        public int PaymentStatusId { get; set; }
        public int CouponId { get; set; }
    }

    public class StatusDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public class ShowInfoDto
    {
        public int CombinedOrderId { get; set; }
        public List<int> OrderIds { get; set; }
        public List<string> SeatsName { get; set; }
        public DateTime CreateTime { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
