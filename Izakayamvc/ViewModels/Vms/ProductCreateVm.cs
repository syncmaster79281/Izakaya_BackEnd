using Izakayamvc.ViewModels.Infra;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class ProductCreateVm
    {
        public int Id { get; set; }

        [Display(Name = "品名")]
        [StringLength(20, ErrorMessage = DaHelper.MaxLength)]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Name { get; set; }

        [Display(Name = "單價")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int UnitPrice { get; set; }

        [Display(Name = "照片")]
        [StringLength(200, ErrorMessage = DaHelper.MaxLength)]
        public string Image = UploadFileHelper.NoImage;

        [Display(Name = "介紹")]
        [StringLength(200, ErrorMessage = DaHelper.MaxLength)]
        [Required(ErrorMessage = DaHelper.Required)]
        public string Present { get; set; }

        [Display(Name = "排序")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int DisplayOrder { get; set; }

        [Display(Name = "是否提供餐點")]
        [Required(ErrorMessage = DaHelper.Required)]
        public bool IsLaunched { get; set; }

        public int CategoryId { get; set; }
    }
}