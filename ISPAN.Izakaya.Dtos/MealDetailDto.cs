using System;

namespace ISPAN.Izakaya.Dtos
{
    public class MealDetailDto
    {
        public int Id { get; set; }
        public string SeatName { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public DateTime OrderTime { get; set; }

    }

    public class OrderItem
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
    }

}
