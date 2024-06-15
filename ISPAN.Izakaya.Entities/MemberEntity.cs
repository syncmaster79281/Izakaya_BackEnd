using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class MemberEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public string AuthenticatioCode { get; set; }
        public DateTime Birthday { get; set; }
    }
    public static class MemberTransferExtensions
    {
        public static MemberEntity ToEntity(this MemberDto dto)
        {
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name 不可空白");
            if (dto.Name.Length > 10) throw new ArgumentException("Name 長度不可以超過10");

            if (string.IsNullOrEmpty(dto.Account)) throw new ArgumentException("Account 不可空白");
            if (dto.Account.Length > 15) throw new ArgumentException("Account 長度不可以超過15");

            if (string.IsNullOrEmpty(dto.Password)) throw new ArgumentException("Password 不可空白");
            if (dto.Password.Length > 10) throw new ArgumentException("Password 長度不可以超過30");

            if (string.IsNullOrEmpty(dto.Phone)) throw new ArgumentException("Phone 不可空白");
            if (dto.Phone.Length == 11) throw new ArgumentException("Phone 長度不可以超過11");

            if (string.IsNullOrEmpty(dto.Email)) throw new ArgumentException("Email 不可空白");
            if (dto.Email.Length > 30) throw new ArgumentException("Email 長度不可以超過30");

            if (dto.Points < 0) throw new ArgumentException("Points 不可小於0");

            if (dto.Birthday > DateTime.Now) throw new ArgumentException("Birthday 不可以是未來時間");

            return new MemberEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Account = dto.Account,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                Points = dto.Points,
                AuthenticatioCode = dto.AuthenticatioCode,
                Birthday = dto.Birthday
            };
        }
        public static MemberDto ToDto(this MemberEntity entity)
        {
            return new MemberDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Account = entity.Account,
                Password = entity.Password,
                Phone = entity.Phone,
                Email = entity.Email,
                Points = entity.Points,
                AuthenticatioCode = entity.AuthenticatioCode,
                Birthday = entity.Birthday
            };
        }
    }




}

