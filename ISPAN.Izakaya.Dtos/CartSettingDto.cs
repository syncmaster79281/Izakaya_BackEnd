using System;

namespace ISPAN.Izakaya.Dtos
{
    public class CartSettingDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SeatName { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}
