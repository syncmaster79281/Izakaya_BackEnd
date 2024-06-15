using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class OrderPaymentVm
    {
        [Display(Name = "付款編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Id { get; set; }

        [Display(Name = "會員編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int MemberId { get; set; }

        [Display(Name = "組合訂單編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int CombinedOrderId { get; set; }

        [Display(Name = "付款方式編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int PaymentMethodId { get; set; }

        [Display(Name = "付款狀態編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int PaymentStatusId { get; set; }

        [Display(Name = "總金額")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int TotalAmount { get; set; }

        [Display(Name = "折扣金額")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Discount { get; set; }

        [Display(Name = "實付金額")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int NetAmount { get; set; }

        [Display(Name = "付款時間")]
        [Required(ErrorMessage = DaHelper.Required)]
        public DateTime PaymentTime { get; set; } = DateTime.Now;

        [Display(Name = "會員名稱")]
        public string MemberName { get; set; }

        [Display(Name = "訂單時間")]
        public DateTime OrderCreateTime { get; set; }

        [Display(Name = "組合訂單明細")]
        public string OrderIds { get; set; }
        [Display(Name = "付款方式")]
        public string PaymentMethod { get; set; }
        [Display(Name = "付款狀態")]
        public string PaymentStatus { get; set; }

        [Display(Name = "店家編號")]
        public int BranchId { get; set; }

        [Display(Name = "桌號")]
        public string SeatNames { get; set; }

        public List<int> OrderLists => OrderIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();
        public List<string> SeatLists => SeatNames.Split(',').ToList();
    }
}