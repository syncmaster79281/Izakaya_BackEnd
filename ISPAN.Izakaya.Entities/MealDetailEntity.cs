using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class MealDetailEntity
    {
        public int Id { get; set; }
        public string SeatName { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public DateTime OrderTime { get; set; }
    }

    public static class MealDetailTransferExtensions
    {
        public static MealDetailEntity ToEntity(this MealDetailDto dto)
        {

            return new MealDetailEntity
            {
                Id = dto.Id,
                SeatName = dto.SeatName,
                ProductName = dto.ProductName,
                Status = dto.Status,
                UnitPrice = dto.UnitPrice,
                Qty = dto.Qty,
                OrderTime = dto.OrderTime,
            };
        }
        public static MealDetailDto ToDto(this MealDetailEntity entity)
        {
            return new MealDetailDto
            {
                Id = entity.Id,
                SeatName = entity.SeatName,
                ProductName = entity.ProductName,
                Status = entity.Status,
                UnitPrice = entity.UnitPrice,
                Qty = entity.Qty,
                OrderTime = entity.OrderTime,
            };
        }
    }

}
