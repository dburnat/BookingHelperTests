using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingHelper
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;
            var unitOfWork = new UnitOfWork();
            var bookings = repository.GetActiveBookings(booking.Id);


            var overlappingBooking =
                bookings.FirstOrDefault(
                b =>
                booking.DepartureDate >= b.ArrivalDate && booking.ArrivalDate < b.DepartureDate || booking.DepartureDate > b.ArrivalDate && booking.DepartureDate <= b.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }


    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();

        }
    }
   
}
