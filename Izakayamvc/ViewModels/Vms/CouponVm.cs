using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class CouponVm
    {
        public int Id { get; set; }

        [Display(Name = "店家編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int BranchId { get; set; }

        [Display(Name = "優惠券名稱")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Name { get; set; }

        [Display(Name = "餐點編號")]
        public int? ProductId { get; set; }

        [Display(Name = "優惠券類別")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int TypeId { get; set; }

        [Display(Name = "限制條件")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(50, ErrorMessage = DaHelper.MaxLength)]
        public string Condition { get; set; }

        [Display(Name = "折扣方式")]
        [Required(ErrorMessage = DaHelper.Required)]
        public decimal DiscountMethod { get; set; }

        [Display(Name = "起始時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Display(Name = "有效時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; } = DateTime.Now.AddMonths(6);

        [Display(Name = "是否啟用")]
        [Required(ErrorMessage = DaHelper.Required)]
        public bool IsUsed { get; set; }

        [Display(Name = "描述")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(50, ErrorMessage = DaHelper.MaxLength)]
        public string Description { get; set; }
    }
}