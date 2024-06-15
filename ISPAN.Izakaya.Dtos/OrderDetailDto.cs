namespace ISPAN.Izakaya.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public string ProductName { get; set; }
    }
}
