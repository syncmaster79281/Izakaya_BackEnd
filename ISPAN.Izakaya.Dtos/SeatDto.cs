namespace ISPAN.Izakaya.Dtos
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string QRCodeLink { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }

    }
}
