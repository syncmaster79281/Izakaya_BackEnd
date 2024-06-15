using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class ActivityVm
    {
        public int Id { get; set; }

        [Display(Name = "店家編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int BranchId { get; set; }

        [Display(Name = "活動名稱")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Name { get; set; }

        [Display(Name = "活動類別")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Type { get; set; }

        [Display(Name = "折扣方式")]
        [Required(ErrorMessage = DaHelper.Required)]
        public decimal Discount { get; set; }

        [Display(Name = "起始時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "有效時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Display(Name = "是否啟用")]
        [Required(ErrorMessage = DaHelper.Required)]
        public bool IsUsed { get; set; }

        [Display(Name = "優先級")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Levels { get; set; }

        [Display(Name = "描述")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(50, ErrorMessage = DaHelper.MaxLength)]
        public string Description { get; set; }
    }
}