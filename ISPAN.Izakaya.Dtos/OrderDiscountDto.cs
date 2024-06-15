namespace ISPAN.Izakaya.Dtos
{
    public class OrderDiscountDto
    {
        public int Id { get; set; }
        public int OrderPaymentId { get; set; }
        public int CouponId { get; set; }
        public decimal? AppliedValue { get; set; }
    }
}
