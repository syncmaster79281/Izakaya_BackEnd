using System;

namespace ISPAN.Izakaya.Dtos
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public bool IsUsed { get; set; }

        public int Levels { get; set; }
        public string Description { get; set; }
    }
}
