using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Izakayamvc.ViewModels.Vms
{
    public class ArticleVm
    {
        public int Id { get; set; }

        [Display(Name = "發布者")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int EmployeeId { get; set; }

        [Display(Name = "分類")]
        [Required(ErrorMessage = DaHelper.Required)]
        public int CategoryId { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = DaHelper.Required)]
        [MaxLength(50, ErrorMessage = DaHelper.MaxLength)]
        public string Title { get; set; }

        [Display(Name = "內文")]
        [Required(ErrorMessage = DaHelper.Required)]
        [MaxLength(500, ErrorMessage = DaHelper.MaxLength)]
        public string Contents { get; set; }

        [Display(Name = "發布日期")]
        [Required(ErrorMessage = DaHelper.Required)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "下架日期")]
        public DateTime HideTime { get; set; }

        [Display(Name = "狀態")]
        public bool Status { get; set; }

        [Display(Name = "圖片網址")]
        public string ImageURL { get; set; }
    }
}