using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class SeatCartVm
    {
        public int Id { get; set; }

        [Display(Name = "桌子編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int SeatId { get; set; }

        [Display(Name = "餐點編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int ProductId { get; set; }

        [Display(Name = "點餐狀態編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int CartStatusId { get; set; }

        [Display(Name = "單價")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int UnitPrice { get; set; }

        [Display(Name = "數量")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Qty { get; set; }

        [Display(Name = "特殊需求")]
        [StringLength(50, ErrorMessage = DaHelper.MaxLength)]
        public string Notes { get; set; }

        [Display(Name = "點餐時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; } = DateTime.Now;

        [Display(Name = "桌號")]
        [StringLength(5, ErrorMessage = DaHelper.MaxLength)]
        public string SeatName { get; set; }
        [Display(Name = "菜品")]
        [StringLength(20, ErrorMessage = DaHelper.MaxLength)]
        public string ProductName { get; set; }
        [Display(Name = "點餐狀態")]
        [StringLength(5, ErrorMessage = DaHelper.MaxLength)]
        public string CartStatus { get; set; }

        [Display(Name = "分店Id")]
        public int BranchId { get; set; }
    }
}