using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class SeatEntity
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string QRCodeLink { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
    }
    public static class SeatTransferExtensions
    {
        public static SeatEntity ToEntity(this SeatDto dto)
        {
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (dto.BranchId < 0) throw new ArgumentException("BranchID 不可小於0");

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name 不可以空白");
            if (dto.Name.Length > 5) throw new ArgumentException("Name 長度不可以超過5");

            if (string.IsNullOrEmpty(dto.QRCodeLink)) throw new ArgumentException("QRCodeLink 不可以空白");
            if (dto.QRCodeLink.Length > 50) throw new ArgumentException("QRCodeLink 長度不可以超過50");

            if (dto.Capacity < 0) throw new ArgumentException("Capacity 不可小於0");

            return new SeatEntity
            {
                Id = dto.Id,
                BranchId = dto.BranchId,
                Name = dto.Name,
                QRCodeLink = dto.QRCodeLink,
                Capacity = dto.Capacity,
                Status = dto.Status
            };
        }
        public static SeatDto ToDto(this SeatEntity entity)
        {
            return new SeatDto
            {
                Id = entity.Id,
                BranchId = entity.BranchId,
                Name = entity.Name,
                QRCodeLink = entity.QRCodeLink,
                Capacity = entity.Capacity,
                Status = entity.Status
            };
        }
    }
}
