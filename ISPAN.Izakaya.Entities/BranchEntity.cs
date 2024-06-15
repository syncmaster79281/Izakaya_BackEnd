using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class BranchEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public int SeatingCapacity { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public DateTime RestDay { get; set; }
    }
    public static class BranchTransferExtensions
    {
        public static BranchEntity ToEntity(this BranchDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name 不可以空白");
            if (dto.Name.Length > 5) throw new ArgumentException("Name 長度不可以超過5");

            if (string.IsNullOrEmpty(dto.Address)) throw new ArgumentException("Address 不可以空白");
            if (dto.Address.Length > 50) throw new ArgumentException("Address 長度不可以超過50");

            if (string.IsNullOrEmpty(dto.Tel)) throw new ArgumentException("Tel 不可以空白");
            if (dto.Tel.Length > 10) throw new ArgumentException("Tel 長度不可以超過10");

            if (dto.SeatingCapacity < 0) throw new ArgumentException("SeatingCapacity 不可小於0");

            if (dto.RestDay > DateTime.Now) throw new ArgumentException("RestDay 不可以是未來時間");

            return new BranchEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Tel = dto.Tel,
                SeatingCapacity = dto.SeatingCapacity,
                OpeningTime = dto.OpeningTime,
                ClosingTime = dto.ClosingTime,
                RestDay = dto.RestDay,
            };
        }
        public static BranchDto ToDto(this BranchEntity entity)
        {
            return new BranchDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Tel = entity.Tel,
                SeatingCapacity = entity.SeatingCapacity,
                OpeningTime = entity.OpeningTime,
                ClosingTime = entity.ClosingTime,
                RestDay = entity.RestDay,
            };
        }
    }
}
