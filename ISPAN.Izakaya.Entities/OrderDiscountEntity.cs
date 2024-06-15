using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class OrderDiscountEntity
    {
        public int Id { get; set; }
        public int OrderPaymentId { get; set; }
        public int CouponId { get; set; }
        public decimal? AppliedValue { get; set; }
    }
    public static class OrderDiscountTransferExtensions
    {
        public static OrderDiscountEntity ToEntity(this OrderDiscountDto dto)
        {
            //欄位驗證

            if (dto.OrderPaymentId < 0) throw new ArgumentException("OrderPaymentID 不可小於0");

            if (dto.CouponId < 0) throw new ArgumentException("CouponID 不可小於0");

            return new OrderDiscountEntity
            {
                OrderPaymentId = dto.OrderPaymentId,
                CouponId = dto.CouponId,
                AppliedValue = dto.AppliedValue,
            };
        }
        public static OrderDiscountDto ToDto(this OrderDiscountEntity entity)
        {
            return new OrderDiscountDto
            {
                Id = entity.Id,
                OrderPaymentId = entity.OrderPaymentId,
                CouponId = entity.CouponId,
                AppliedValue = entity.AppliedValue,
            };
        }
    }
}
