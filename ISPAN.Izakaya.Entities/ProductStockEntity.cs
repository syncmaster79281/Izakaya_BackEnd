namespace ISPAN.Izakaya.Entities
{
    public class ProductStockEntity
    {
        public int Id { get; set; }
        public ProductEntity Product { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int SafetyStock { get; set; }
        public int Stock { get; set; }
        public int MaxAlertStock { get; set; }
        public ProductCategoryEntity ProductCategory { get; set; }
    }
}
