using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class ProductCategoryVm
    {
        public int Id { get; set; }
        [Display(Name = "分類名稱")]
        [Required(ErrorMessage = DaHelper.Required)]
        [StringLength(20)]
        public string Name { get; set; }

    }
}