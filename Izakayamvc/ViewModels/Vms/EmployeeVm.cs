using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class EmployeeVm
    {
        public int Id { get; set; }

        [Display(Name = "店家編號")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int BranchId { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Name { get; set; }

        [Display(Name = "職稱")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Department { get; set; }

        [Display(Name = "雇用日期")]
        [Required(ErrorMessage = DaHelper.Required)]
        public DateTime HireDate { get; set; }

        [Display(Name = "薪水")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int Salary { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = DaHelper.Required)]
        public string EmployeePassword { get; set; }



    }
}