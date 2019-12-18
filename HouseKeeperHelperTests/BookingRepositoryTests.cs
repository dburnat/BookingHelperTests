using BookingHelper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingHelper
{
    //verify
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _repository;
        private Booking _existingBooking;
        [SetUp]
        public void Setup()
        {
            _existingBooking = new Booking
            {
                Id = 50,
                ArrivalDate = ArriveOn(2019,11,5),
                DepartureDate = DepartOn(2019,11,10),
                Reference = "firstRef",
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(s => s.GetActiveBookings(1)).Returns(
               new List<Booking>
               {
                   _existingBooking
               }.AsQueryable());
        }


        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }


        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsBeforeAndFinishesDuringExistingBooking_ReturnsFirstRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id=1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days:2),
                DepartureDate = _existingBooking.DepartureDate,
                Reference = "ex"
                
            }, _repository.Object);

            Assert.That(result,  Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsDuringExistingBookingAndFinishesAfter_ReturnsFirstRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = _existingBooking.ArrivalDate,
                DepartureDate = After(_existingBooking.DepartureDate),
                Reference = "starts"

            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeExistingBookingAndFinishesAfter_ReturnsFirstRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Reference = "beforeafter"

            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        
        [Test]
        public void BookingStartsAfterExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate,2),
                DepartureDate = After(_existingBooking.DepartureDate, 3),
                Reference = "after"

            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsDuringAndFinishesBeforeExistingBooking_ReturnsFirstRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)

            }, _repository.Object);
            
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesDuringExistingBooking_ReturnsFirstRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = _existingBooking.ArrivalDate,
                DepartureDate = _existingBooking.DepartureDate,
                Reference = "same"
            }, _repository.Object);
            
            Assert.That(result, Is.EqualTo( _existingBooking.Reference));
        }
    }
}