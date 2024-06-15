using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class CombinedOrderEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public static class CombinedOrderTransferExtensions
    {
        public static CombinedOrderEntity ToEntity(this CombinedOrderDto dto)
        {

            if (dto.CreateTime > DateTime.Now) throw new ArgumentException("CreateTime 不可以為未來時間");


            return new CombinedOrderEntity
            {
                CreateTime = dto.CreateTime,
            };
        }
        public static CombinedOrderDto ToDto(this CombinedOrderEntity entity)
        {
            return new CombinedOrderDto
            {
                Id = entity.Id,
                CreateTime = entity.CreateTime,
            };
        }
    }
}
