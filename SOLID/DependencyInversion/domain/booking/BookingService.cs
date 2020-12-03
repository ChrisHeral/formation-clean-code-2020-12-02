using SOLID.DependencyInversion.infrastructure;

namespace SOLID.DependencyInversion.domain.booking
{
    public class BookingService
    {
        private readonly IAvailabilityRepository bookings;

        public BookingService(IAvailabilityRepository bookings)
        {
            this.bookings = bookings;
        }

        public BookingOutcome Book()
        {
            bool successful = bookings.IsAvailable();
            return new BookingOutcome(successful);
        }
    }
}