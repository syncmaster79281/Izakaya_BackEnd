namespace ISPAN.Izakaya.Dtos
{
    public class ProductStockDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int SafetyStock { get; set; }
        public int Stock { get; set; }
        public int MaxAlertStock { get; set; }
        public ProductCategoryDto ProductCategory { get; set; }

    }
}
