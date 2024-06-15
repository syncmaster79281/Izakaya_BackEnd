using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int CombinedOrderId { get; set; }
        public DateTime CreateTime { get; set; }
        public int Subtotal { get; set; }
    }
    public static class OrderTransferExtensions
    {
        public static OrderEntity ToEntity(this OrderDto dto)
        {
            //欄位驗證

            if (dto.SeatId < 0) throw new ArgumentException("SeatID 不可小於0");


            if (dto.CombinedOrderId < 0) throw new ArgumentException("CombinedOrderID 不可小於0");

            if (dto.CreateTime > DateTime.Now) throw new ArgumentException("CreateTime 不可以是未來時間");

            if (dto.Subtotal < 0) throw new ArgumentException("Subtotal 不可小於0");

            return new OrderEntity
            {
                SeatId = dto.SeatId,
                CombinedOrderId = dto.CombinedOrderId,
                CreateTime = dto.CreateTime,
                Subtotal = dto.Subtotal,
            };
        }
        public static OrderDto ToDto(this OrderEntity entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                SeatId = entity.SeatId,
                CombinedOrderId = entity.CombinedOrderId,
                CreateTime = entity.CreateTime,
                Subtotal = entity.Subtotal,
            };
        }
    }
}
