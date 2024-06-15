using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class CartStatusVm
    {
        public int Id { get; set; }

        [Display(Name = "餐點狀態")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(5, ErrorMessage = DaHelper.MaxLength)]
        public string Status { get; set; }
    }
}