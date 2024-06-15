using System;

namespace ISPAN.Izakaya.Dtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public string Condition { get; set; }
        public decimal DiscountMethod { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsUsed { get; set; }
        public string Description { get; set; }
    }
}
