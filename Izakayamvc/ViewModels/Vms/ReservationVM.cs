using System;
using System.ComponentModel.DataAnnotations;

namespace Izakayamvc.ViewModels.Vms
{
    public class ReservationVM
    {
        [Display(Name = "訂位日期")]
        [Required]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime ReservationTime { get; set; }

        [Display(Name = "訂位人數")]
        [Required]
        [Range(1, 8, ErrorMessage = "人數必須介於 1 到 8 之間")]
        public int Qty { get; set; }

        [Display(Name = "訂位大名")]
        [Required]
        public string Name { get; set; }


        [Display(Name = "電話")]
        [Required]
        public string Tel { get; set; }

        [Display(Name = "訂位狀態")]
        public string Status { get; set; }

        public int Id { get; set; }

        [Display(Name = "店名")]
        [Required]
        [Range(1, 2, ErrorMessage = "店名 1 或 2 之間")]
        public int BranchId { get; set; }

        [Display(Name = "桌號")]
        [Required]
        [Range(1, 10, ErrorMessage = "桌號必須介於 1 到 10 之間")]
        public int SeatId { get; set; }


        [Display(Name = "就位確認")]
        public string FillUp { get; set; }

        [Display(Name = "會員編號")]
        public int MemberId { get; set; }
    }
}