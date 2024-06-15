using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class OrderDetailEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public string ProductName { get; set; }
    }
    public static class OrderDetailTransferExtensions
    {
        public static OrderDetailEntity ToEntity(this OrderDetailDto dto)
        {
            //欄位驗證

            if (dto.OrderId < 0) throw new ArgumentException("OrderID 不可小於0");

            if (dto.ProductId < 0) throw new ArgumentException("ProductID 不可小於0");

            if (dto.UnitPrice < 0) throw new ArgumentException("UnitPrice 不可小於0");

            if (dto.Qty < 0) throw new ArgumentException("Qty 不可小於0");

            return new OrderDetailEntity
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                UnitPrice = dto.UnitPrice,
                Qty = dto.Qty,
                ProductName = dto.ProductName,
            };
        }
        public static OrderDetailDto ToDto(this OrderDetailEntity entity)
        {
            return new OrderDetailDto
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                UnitPrice = entity.UnitPrice,
                Qty = entity.Qty,
                ProductName = entity.ProductName,
            };
        }
    }
}
