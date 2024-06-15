using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class OrderVm
    {
        public int Id { get; set; }

        [Display(Name = "桌子編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int SeatId { get; set; }


        [Display(Name = "組合訂單編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int CombinedOrderId { get; set; }

        [Display(Name = "訂單時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Display(Name = "小計")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Subtotal { get; set; }
    }
}