using ISPAN.Izakaya.Entities;
using System;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface ICalendarReservationsRepository
    {
        IEnumerable<ReservationEntity> GetReservationsForDateRange(DateTime startDate, DateTime endDate);


        bool GetReservationById(int reservationId);

        bool UpdateReservation(ReservationEntity reservationEntity);

        bool DeleteReservation(int reservationId);

        ReservationEntity GetReservation(int id);

        void CreateReservation(ReservationEntity reservationEntity);

        IEnumerable<SeatDropList> GetSeatDropList();

        bool UpdateFillUp(int reservationId);

        bool ClearFillUp(int reservationId);
    }
}
