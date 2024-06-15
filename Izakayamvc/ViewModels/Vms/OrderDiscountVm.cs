using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class OrderDiscountVm
    {
        public int Id { get; set; }

        [Display(Name = "結帳單號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int OrderPaymentId { get; set; }

        [Display(Name = "優惠券編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int CouponId { get; set; }

        [Display(Name = "應用折扣")]
        public decimal? AppliedValue { get; set; }
    }
}