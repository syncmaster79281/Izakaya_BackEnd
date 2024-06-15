using ISPAN.Izakaya.Dtos;

namespace ISPAN.Izakaya.Entities
{
    public class MemberListEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static class MemberListTransferExtensions
    {
        public static MemberListEntity ToEntity(this MemberListDto dto)
        {

            return new MemberListEntity
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
        public static MemberListDto ToDto(this MemberListEntity entity)
        {
            return new MemberListDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
