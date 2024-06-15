using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class MemberVm
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(10, ErrorMessage = DaHelper.MaxLength)]
        public string Name { get; set; }

        [Display(Name = "帳號")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(15, ErrorMessage = DaHelper.MaxLength)]
        public string Account { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(30, ErrorMessage = DaHelper.MaxLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "手機號碼")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(11, ErrorMessage = DaHelper.MaxLength)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "電子郵件")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(30, ErrorMessage = DaHelper.MaxLength)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "會員點數")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Points { get; set; }

        [Display(Name = "驗證碼")]
        [StringLength(6, ErrorMessage = DaHelper.MaxLength)]
        public string AuthenticatioCode { get; set; }

        [Display(Name = "生日")]
        [Required(ErrorMessage = DaHelper.Required)]
        [DataType(DataType.DateTime)]
        public string Birthday { get; set; }
    }
}