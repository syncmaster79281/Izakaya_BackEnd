using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class SeatCartEntity
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int ProductId { get; set; }
        public int CartStatusId { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public string Notes { get; set; }
        public DateTime OrderTime { get; set; }

        public string SeatName { get; set; }
        public string ProductName { get; set; }
        public string CartStatus { get; set; }
        public int BranchId { get; set; }
    }
    public static class SeatCartTransferExtensions
    {
        public static SeatCartEntity ToEntity(this SeatCartDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (dto.SeatId < 0) throw new ArgumentException("SeatID 不可小於0");

            if (dto.ProductId < 0) throw new ArgumentException("ProductID 不可小於0");

            if (dto.CartStatusId < 0) throw new ArgumentException("CartStatusID 不可小於0");

            if (dto.UnitPrice < 0) throw new ArgumentException("UnitPrice 不可小於0");

            if (dto.Qty < 0) throw new ArgumentException("Qty 不可小於0");

            if (dto.Notes.Length > 50) throw new ArgumentException("Notes 長度不可以超過50");

            if (dto.OrderTime > DateTime.Now) throw new ArgumentException("OrderTime 不可以是未來時間");

            return new SeatCartEntity
            {
                Id = dto.Id,
                SeatId = dto.SeatId,
                ProductId = dto.ProductId,
                CartStatusId = dto.CartStatusId,
                UnitPrice = dto.UnitPrice,
                Qty = dto.Qty,
                Notes = dto.Notes,
                OrderTime = dto.OrderTime,
                ProductName = dto.ProductName,
                SeatName = dto.SeatName,
                CartStatus = dto.CartStatus,
                BranchId = dto.BranchId
            };
        }
        public static SeatCartDto ToDto(this SeatCartEntity entity)
        {
            return new SeatCartDto
            {
                Id = entity.Id,
                SeatId = entity.SeatId,
                ProductId = entity.ProductId,
                CartStatusId = entity.CartStatusId,
                UnitPrice = entity.UnitPrice,
                Qty = entity.Qty,
                Notes = entity.Notes,
                OrderTime = entity.OrderTime,
                ProductName = entity.ProductName,
                SeatName = entity.SeatName,
                CartStatus = entity.CartStatus,
                BranchId = entity.BranchId
            };
        }
    }
}
