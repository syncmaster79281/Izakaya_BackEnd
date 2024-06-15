using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class SortPaymentEntity
    {
        public string Name { get; set; }
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
    }
    public static class SortPaymentTransferExtensions
    {
        public static SortPaymentEntity ToEntity(this SortPaymentDto dto)
        {
            //欄位驗證

            if (dto.StartTime > dto.EndTime) throw new ArgumentException("StartTime 不可以大於 EndTime");

            return new SortPaymentEntity
            {
                Name = dto.Name,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };
        }
        public static SortPaymentDto ToDto(this SortPaymentEntity entity)
        {
            return new SortPaymentDto
            {
                Name = entity.Name,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }
    }
}
