using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class CartStatusEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
    public static class CartStatusTransferExtensions
    {
        public static CartStatusEntity ToEntity(this CartStatusDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (string.IsNullOrEmpty(dto.Status)) throw new ArgumentException("Status 不可為空白");
            if (dto.Status.Length > 5) throw new ArgumentException("Status 長度不可為超過5");

            return new CartStatusEntity
            {
                Id = dto.Id,
                Status = dto.Status,
            };
        }
        public static CartStatusDto ToDto(this CartStatusEntity entity)
        {
            return new CartStatusDto
            {
                Id = entity.Id,
                Status = entity.Status,
            };
        }
    }
}
