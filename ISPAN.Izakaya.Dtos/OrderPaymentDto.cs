using System;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.Dtos
{
    public class OrderPaymentDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int CombinedOrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public int PaymentStatusId { get; set; }
        public int TotalAmount { get; set; }
        public int Discount { get; set; }
        public int NetAmount { get; set; }
        public DateTime PaymentTime { get; set; }
        public string MemberName { get; set; }
        public DateTime OrderCreateTime { get; set; }
        public string OrderIds { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public int BranchId { get; set; }
        public string SeatNames { get; set; }
        public List<int> OrderLists => OrderIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();
        public List<string> SeatLists => SeatNames.Split(',').ToList();
    }
}
