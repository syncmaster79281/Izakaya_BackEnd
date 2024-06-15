using System;
using System.ComponentModel.DataAnnotations;

namespace Izakayamvc.ViewModels.Vms
{
    public class PaymentsHistoryVm
    {
        [Display(Name = "訂單編號")]
        public int CombinedOrderId { get; set; }

        [Display(Name = "會員名稱")]
        public string MemberName { get; set; }

        [Display(Name = "用餐明細")]
        public string OrderIds { get; set; }

        [Display(Name = "桌號")]
        public string SeatNames { get; set; }
        [Display(Name = "總價錢")]
        public int TotalAmount { get; set; }
        [Display(Name = "折扣")]
        public int Discount { get; set; }
        [Display(Name = "實付金額")]
        public int NetAmount { get; set; }

        [Display(Name = "付款方式")]
        public string PaymentMethod { get; set; }

        [Display(Name = "付款狀態")]
        public string PaymentStatus { get; set; }

        [Display(Name = "店家編號")]
        public int BranchId { get; set; }


        [Display(Name = "付款時間")]
        public DateTime PaymentTime { get; set; }
    }
}