using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class PaymentMethodVm
    {
        public int Id { get; set; }

        [Display(Name = "付款方式")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(5, ErrorMessage = DaHelper.MaxLength)]
        public string Method { get; set; }
    }
}