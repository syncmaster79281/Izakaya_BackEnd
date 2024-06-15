using System;

namespace ISPAN.Izakaya.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int CombinedOrderId { get; set; }
        public DateTime CreateTime { get; set; }
        public int Subtotal { get; set; }
    }
}
