using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class LoginVm
    {
        [Display(Name = "帳號")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(10, ErrorMessage = DaHelper.MaxLength)]
        public string Name { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(8, ErrorMessage = DaHelper.MaxLength)]
        [DataType(DataType.Password)]

        public string EmployeePassword { get; set; }
    }
}