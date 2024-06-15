using System;

namespace ISPAN.Izakaya.Dtos
{
    public class ArticleDto
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
}
