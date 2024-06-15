using System;

namespace ISPAN.Izakaya.Entities
{
    public class ReservationEntity
    {

        public DateTime ReservationTime { get; set; }

        public int Qty { get; set; }

        public string Name { get; set; }

        public string Tel { get; set; }

        public string Status { get; set; }

        public int Id { get; set; }

        public int SeatId { get; set; }

        public int BranchId { get; set; }

        public string FillUp { get; set; }

    }
}