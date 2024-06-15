using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; } = "男性";
        public string EmployeePassword { get; set; }
    }
    public static class EmployeeTransferExtensions
    {
        public static EmployeeEntity ToEntity(this EmployeeDto dto)
        {
            //欄位驗證
            if (dto.Id < 0) throw new ArgumentException("ID 不可小於0");

            if (dto.BranchId < 0) throw new ArgumentException("BrenchId 不可小於0");

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name 不可為空");

            if (string.IsNullOrEmpty(dto.Department)) throw new ArgumentException("Department 不可為空");


            if (dto.Salary < 0) throw new ArgumentException("Salary 不可小於0");


            if (string.IsNullOrEmpty(dto.EmployeePassword)) throw new ArgumentException("EmployeePassword 不可為空");

            return new EmployeeEntity
            {
                Id = dto.Id,
                BranchId = dto.BranchId,
                Name = dto.Name,
                Department = dto.Department,
                HireDate = dto.HireDate,
                Salary = dto.Salary,
                EmployeePassword = dto.EmployeePassword
            };
        }
        public static EmployeeDto ToDto(this EmployeeEntity entity)
        {
            return new EmployeeDto
            {
                Id = entity.Id,
                BranchId = entity.BranchId,
                Name = entity.Name,
                Department = entity.Department,
                Salary = entity.Salary,
                EmployeePassword = entity.EmployeePassword,
                HireDate = Convert.ToDateTime(entity.HireDate),
            };
        }

    }
}

