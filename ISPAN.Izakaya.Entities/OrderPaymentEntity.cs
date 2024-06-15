using ISPAN.Izakaya.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.Entities
{
    public class OrderPaymentEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int CombinedOrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public int PaymentStatusId { get; set; }
        public int TotalAmount { get; set; }
        public int Discount { get; set; }
        public int NetAmount { get; set; }
        public DateTime PaymentTime { get; set; }
        public string MemberName { get; set; }
        public DateTime OrderCreateTime { get; set; }
        public string OrderIds { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public int BranchId { get; set; }
        public string SeatNames { get; set; }
        public List<int> OrderLists => OrderIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();
        public List<string> SeatLists => SeatNames.Split(',').ToList();
    }
    public static class OrderPaymentTransferExtensions
    {
        public static OrderPaymentEntity ToEntity(this OrderPaymentDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (dto.MemberId < 0) throw new ArgumentException("MemberID 不可小於0");

            if (dto.CombinedOrderId < 0) throw new ArgumentException("CombinedOrderID 不可小於0");

            if (dto.PaymentMethodId < 0) throw new ArgumentException("PaymentMethodID 不可小於0");

            if (dto.PaymentStatusId < 0) throw new ArgumentException("PaymentStatusID 不可小於0");

            if (dto.TotalAmount < 0) throw new ArgumentException("TotalAmount 不可小於0");

            if (dto.Discount < 0) throw new ArgumentException("Discount 不可小於0");

            if (dto.NetAmount < 0) throw new ArgumentException("NetAmount 不可小於0");

            if (dto.PaymentTime > DateTime.Now) throw new ArgumentException("PaymentTime 不可以是未來時間");

            return new OrderPaymentEntity
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                CombinedOrderId = dto.CombinedOrderId,
                PaymentMethodId = dto.PaymentMethodId,
                PaymentStatusId = dto.PaymentStatusId,
                TotalAmount = dto.TotalAmount,
                Discount = dto.Discount,
                NetAmount = dto.NetAmount,
                PaymentTime = dto.PaymentTime,
                MemberName = dto.MemberName,
                OrderCreateTime = dto.OrderCreateTime,
                OrderIds = dto.OrderIds,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus,
                BranchId = dto.BranchId,
                SeatNames = dto.SeatNames,
            };
        }
        public static OrderPaymentDto ToDto(this OrderPaymentEntity entity)
        {
            return new OrderPaymentDto
            {
                Id = entity.Id,
                MemberId = entity.MemberId,
                CombinedOrderId = entity.CombinedOrderId,
                PaymentMethodId = entity.PaymentMethodId,
                PaymentStatusId = entity.PaymentStatusId,
                TotalAmount = entity.TotalAmount,
                Discount = entity.Discount,
                NetAmount = entity.NetAmount,
                PaymentTime = entity.PaymentTime,
                MemberName = entity.MemberName,
                OrderCreateTime = entity.OrderCreateTime,
                OrderIds = entity.OrderIds,
                PaymentMethod = entity.PaymentMethod,
                PaymentStatus = entity.PaymentStatus,
                BranchId = entity.BranchId,
                SeatNames = entity.SeatNames,
            };
        }
    }
}
