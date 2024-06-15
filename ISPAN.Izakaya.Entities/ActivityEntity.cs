using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class ActivityEntity
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public bool IsUsed { get; set; }

        public int Levels { get; set; }
        public string Description { get; set; }

    }
    public static class ActivityTransferExtensions
    {
        public static ActivityEntity ToEntity(this ActivityDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("ID 不可小於0");

            if (dto.BranchId < 0) throw new ArgumentException("BranchID 不可小於0");

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name 不可為空");

            if (string.IsNullOrEmpty(dto.Type)) throw new ArgumentException("Type 不可為空");

            if (dto.Discount < 0) throw new ArgumentException("Discount 不可小於0");

            if (dto.StartTime < DateTime.Now) throw new ArgumentException("開始時間不可以是過去時間");
            if (dto.EndTime <= dto.StartTime) throw new ArgumentException("結束時間不能小於等於開始時間");

            if (dto.Levels < 0) throw new ArgumentException("Levels 不可小於0");


            if (string.IsNullOrEmpty(dto.Description)) throw new ArgumentException("Description 不可為空");

            return new ActivityEntity
            {
                Id = dto.Id,
                BranchId = dto.BranchId,
                Name = dto.Name,
                Type = dto.Type,
                Discount = dto.Discount,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsUsed = dto.IsUsed,
                Levels = dto.Levels,
                Description = dto.Description
            };
        }
        public static ActivityDto ToDto(this ActivityEntity entity)
        {
            return new ActivityDto
            {
                Id = entity.Id,
                BranchId = entity.BranchId,
                Name = entity.Name,
                Type = entity.Type,
                Discount = entity.Discount,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                IsUsed = entity.IsUsed,
                Levels = entity.Levels,
                Description = entity.Description
            };
        }
    }
}
