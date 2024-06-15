using System.ComponentModel.DataAnnotations;

namespace Izakayamvc.ViewModels.Vms
{
    public class ProductStockVm
    {
        public int Id { get; set; }
        [Display(Name = "品名")]
        public string ProductName { get; set; }
        [Display(Name = "分類名稱")]
        public string ProductCategoryName { get; set; }
        [Display(Name = "分店")]
        public string BranchName { get; set; }
        [Display(Name = "安全庫存量")]
        public int SafetyStock { get; set; }
        [Display(Name = "目前庫存量")]
        public int Stock { get; set; }
        [Display(Name = "最大警戒庫存量")]
        public int MaxAlertStock { get; set; }
    }
}