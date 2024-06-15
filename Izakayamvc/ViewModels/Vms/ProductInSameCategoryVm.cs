using System.ComponentModel.DataAnnotations;

namespace Izakayamvc.ViewModels.Vms
{
    public class ProductInSameCategoryVm
    {
        public int Id { get; set; }
        [Display(Name = "品名")]
        public string Name { get; set; }
        [Display(Name = "單價")]
        public int UnitPrice { get; set; }
    }
}