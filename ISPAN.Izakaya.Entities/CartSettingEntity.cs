using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class CartSettingEntity
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SeatName { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public DateTime ClosingTime { get; set; }
    }
    public static class CartSettingTransferExtensions
    {
        public static CartSettingEntity ToEntity(this CartSettingDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (dto.SeatId < 0) throw new ArgumentException("SeatID 不可小於0");

            if (dto.EndTime <= dto.StartTime) throw new Exception("結束時間不可早於等於開始時間");

            return new CartSettingEntity
            {
                Id = dto.Id,
                SeatId = dto.SeatId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                BranchName = dto.BranchName,
                BranchId = dto.BranchId,
                ClosingTime = dto.ClosingTime,
                SeatName = dto.SeatName,
            };
        }
        public static CartSettingDto ToDto(this CartSettingEntity entity)
        {
            return new CartSettingDto
            {
                Id = entity.Id,
                SeatId = entity.SeatId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                BranchName = entity.BranchName,
                BranchId = entity.BranchId,
                ClosingTime = entity.ClosingTime,
                SeatName = entity.SeatName,
            };
        }
    }

}
