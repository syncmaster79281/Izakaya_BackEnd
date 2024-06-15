using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class PaymentMethodEntity
    {
        public int Id { get; set; }
        public string Method { get; set; }
    }
    public static class PaymentMethodTransferExtensions
    {
        public static PaymentMethodEntity ToEntity(this PaymentMethodDto dto)
        {
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (string.IsNullOrEmpty(dto.Method)) throw new ArgumentException("Method 不可以空白");
            if (dto.Method.Length > 5) throw new ArgumentException("Method 長度不可以超過5");

            return new PaymentMethodEntity
            {
                Id = dto.Id,
                Method = dto.Method,
            };
        }
        public static PaymentMethodDto ToDto(this PaymentMethodEntity entity)
        {
            return new PaymentMethodDto
            {
                Id = entity.Id,
                Method = entity.Method,
            };
        }
    }
}
