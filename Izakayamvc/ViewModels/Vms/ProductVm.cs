using System.ComponentModel.DataAnnotations;

namespace Izakayamvc.ViewModels.Vms
{
    public class ProductVm
    {
        public int Id { get; set; }
        [Display(Name = "品名")]
        public string Name { get; set; }
        [Display(Name = "單價")]
        public int UnitPrice { get; set; }
        [Display(Name = "照片")]
        public string Image { get; set; }
        [Display(Name = "排序")]
        public int DisplayOrder { get; set; }
        public bool IsLaunched { get; set; }
        [Display(Name = "是否提供餐點")]
        public string Launched => IsLaunched ? "販售中" : "已下架";
        [Display(Name = "分類名稱")]
        public string CategoryName { get; set; }
    }
}