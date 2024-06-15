using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class ProductEditVm
    {
        public int Id { get; set; }
        [Display(Name = "分類名稱")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "品名")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Name { get; set; }
        [Display(Name = "單價")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int UnitPrice { get; set; }
        [Display(Name = "照片")]
        public string Image { get; set; }
        [Display(Name = "介紹")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Present { get; set; }
        [Display(Name = "排序")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int DisplayOrder { get; set; }
        [Display(Name = "是否提供餐點")]
        public bool IsLaunched = false;
    }
}