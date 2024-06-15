namespace ISPAN.Izakaya.Entities
{
    public class PurchaseOrderEntity
    {
        public int Id { get; set; }

        public int RecordId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int UnitCost { get; set; }
        public int? Subtotal { get; set; }

        public PurchaseRecordEntity PurchaseRecord { get; set; }
    }
}
