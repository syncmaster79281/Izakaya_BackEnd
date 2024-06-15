using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class SeatVm
    {
        public int Id { get; set; }

        [Display(Name = "店家編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int BranchId { get; set; }

        [Display(Name = "桌號")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(5, ErrorMessage = DaHelper.MaxLength)]
        public string Name { get; set; }

        [Display(Name = "QR Code")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(50, ErrorMessage = DaHelper.MaxLength)]
        public string QRCodeLink { get; set; }

        [Display(Name = "容納人數")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Capacity { get; set; }

        [Display(Name = "可否預定")]
        [Required(ErrorMessage = DaHelper.Required)]
        public bool Status { get; set; }
    }
}