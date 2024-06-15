using System;

namespace ISPAN.Izakaya.Dtos
{
    public class SortPaymentDto
    {
        public string Name { get; set; }
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
    }
}
