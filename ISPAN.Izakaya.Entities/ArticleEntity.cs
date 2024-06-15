using ISPAN.Izakaya.Dtos;
using System;

namespace ISPAN.Izakaya.Entities
{
    public class ArticleEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }

        public string Contents { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime HideTime { get; set; }
        public bool Status { get; set; }
        public string ImageURL { get; set; }
    }

    public static class ArticleTransferExtensions
    {
        public static ArticleEntity ToEntity(this ArticleDto dto)
        {
            if (dto.Id < 0) throw new ArgumentException("Id 不可小於0");

            if (dto.EmployeeId < 0) throw new ArgumentException("EmployeeId 不可小於0");

            if (dto.CategoryId < 0) throw new ArgumentException("CategoryId 不可小於0");

            if ((dto.Title != null) && (dto.Title.Length > 50)) throw new ArgumentException("Title 不可以空白或超過50");

            if (string.IsNullOrEmpty(dto.Contents)) throw new ArgumentException("Content 不可以空白");

            if (dto.PublishDate < DateTime.Now) throw new ArgumentException("PublishDate 不可以是過期時間");

            if (dto.HideTime < dto.PublishDate) throw new ArgumentException("HideTime 不可以是過期時間或早於發布日期");

            //if (string.IsNullOrEmpty(dto.ImageURL)) throw new ArgumentException("ImageURL 不可以為空");


            return new ArticleEntity
            {
                Id = dto.Id,
                EmployeeId = dto.EmployeeId,
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                Contents = dto.Contents,
                PublishDate = dto.PublishDate,
                HideTime = dto.HideTime,
                Status = dto.Status,
                ImageURL = dto.ImageURL
            };
        }

        public static ArticleDto ToDto(this ArticleEntity entity)
        {
            return new ArticleDto
            {
                Id = entity.Id,
                EmployeeId = entity.EmployeeId,
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                Contents = entity.Contents,
                PublishDate = entity.PublishDate,
                HideTime = entity.HideTime,
                Status = entity.Status,
                ImageURL = entity.ImageURL

            };
        }

    }
}
