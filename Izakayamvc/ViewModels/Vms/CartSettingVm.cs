using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class CartSettingVm
    {
        public int Id { get; set; }

        [Display(Name = "桌子編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        [Range(1, 50, ErrorMessage = DaHelper.SeatRange)]
        public int SeatId { get; set; }

        [Display(Name = "用餐開始")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Display(Name = "用餐結束")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(2);

        [Display(Name = "桌號")]
        public string SeatName { get; set; }
        [Display(Name = "分店名稱")]
        public string BranchName { get; set; }
        [Display(Name = "分店編號")]
        public int BranchId { get; set; }
        [Display(Name = "營業結束時間")]
        public DateTime ClosingTime { get; set; }
    }
}