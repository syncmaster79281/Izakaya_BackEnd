using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class OrderDetailVm
    {
        public int Id { get; set; }

        [Display(Name = "訂單編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int OrderId { get; set; }

        [Display(Name = "產品編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int ProductId { get; set; }

        [Display(Name = "單價")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int UnitPrice { get; set; }

        [Display(Name = "數量")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Qty { get; set; }
    }
}