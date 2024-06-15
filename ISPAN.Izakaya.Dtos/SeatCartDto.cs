using System;

namespace ISPAN.Izakaya.Dtos
{
    public class SeatCartDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int ProductId { get; set; }
        public int CartStatusId { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public string Notes { get; set; }
        public DateTime OrderTime { get; set; }
        public string SeatName { get; set; }
        public string ProductName { get; set; }
        public string CartStatus { get; set; }
        public int BranchId { get; set; }
    }
}
