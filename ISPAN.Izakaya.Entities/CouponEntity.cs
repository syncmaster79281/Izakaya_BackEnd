using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class CouponEntity
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public string Condition { get; set; }
        public decimal DiscountMethod { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsUsed { get; set; }
        public string Description { get; set; }
    }
    public static class CouponTransferExtensions
    {
        public static CouponEntity ToEntity(this CouponDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (dto.BranchId < 0) throw new ArgumentException("BranchID 不可小於0");

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name 不可為空");


            if (dto.ProductId < 0) throw new ArgumentException("ProductID 不可小於0");

            if (dto.TypeId < 0) throw new ArgumentException("TypeID 不可小於0");

            if (string.IsNullOrEmpty(dto.Condition)) throw new ArgumentException("Condition 不可為空");

            if (string.IsNullOrEmpty(dto.Description)) throw new ArgumentException("Description 不可為空");

            if (dto.StartTime < DateTime.Now) throw new ArgumentException("開始時間不可以是過去時間");
            if (dto.EndTime <= dto.StartTime) throw new ArgumentException("結束時間不能小於等於開始時間");

            return new CouponEntity
            {
                Id = dto.Id,
                BranchId = dto.BranchId,
                Name = dto.Name,
                ProductId = dto.ProductId,
                TypeId = dto.TypeId,
                Condition = dto.Condition,
                DiscountMethod = dto.DiscountMethod,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsUsed = dto.IsUsed,
                Description = dto.Description,
            };
        }
        public static CouponDto ToDto(this CouponEntity entity)
        {
            return new CouponDto
            {
                Id = entity.Id,
                BranchId = entity.BranchId,
                Name = entity.Name,
                ProductId = entity.ProductId,
                TypeId = entity.TypeId,
                Condition = entity.Condition,
                DiscountMethod = entity.DiscountMethod,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                IsUsed = entity.IsUsed,
                Description = entity.Description,
            };
        }
    }
}
