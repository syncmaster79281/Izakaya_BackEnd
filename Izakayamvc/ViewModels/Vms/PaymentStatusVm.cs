using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class PaymentStatusVm
    {
        public int Id { get; set; }

        [Display(Name = "付款狀態")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(5, ErrorMessage = DaHelper.MaxLength)]
        public string Status { get; set; }
    }
}