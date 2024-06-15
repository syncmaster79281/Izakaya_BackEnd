using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class PaymentStatusEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
    public static class PaymentStatusTransferExtensions
    {
        public static PaymentStatusEntity ToEntity(this PaymentStatusDto dto)
        {
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (string.IsNullOrEmpty(dto.Status)) throw new ArgumentException("Status 不可以空白");
            if (dto.Status.Length > 5) throw new ArgumentException("Status 長度不可以超過5");

            return new PaymentStatusEntity
            {
                Id = dto.Id,
                Status = dto.Status,
            };
        }
        public static PaymentStatusDto ToDto(this PaymentStatusEntity entity)
        {
            return new PaymentStatusDto
            {
                Id = entity.Id,
                Status = entity.Status,
            };
        }
    }
}
