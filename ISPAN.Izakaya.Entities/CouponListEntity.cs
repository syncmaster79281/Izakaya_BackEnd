using ISPAN.Izakaya.Dtos;

namespace ISPAN.Izakaya.Entities
{
    public class CouponListEntity
    {
        public int Id { get; set; }
        public string Condition { get; set; }
    }
    public static class CouponListTransferExtensions
    {
        public static CouponListEntity ToEntity(this CouponListDto dto)
        {

            return new CouponListEntity
            {
                Id = dto.Id,
                Condition = dto.Condition,
            };
        }
        public static CouponListDto ToDto(this CouponListEntity entity)
        {
            return new CouponListDto
            {
                Id = entity.Id,
                Condition = entity.Condition,
            };
        }
    }
}
