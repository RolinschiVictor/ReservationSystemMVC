namespace ReservationSystemMVC.Core.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Example implementation for booking a resource
    /*
    public class BookResourceCommand : ICommand
    {
        private readonly BookingService _service;
        private readonly Booking _booking;

        public BookResourceCommand(BookingService service, Booking booking)
        {
            _service = service;
            _booking = booking;
        }

        public void Execute()
        {
            _service.CreateBooking(_booking);
        }

        public void Undo()
        {
            _service.CancelBooking(_booking.Id);
        }
    }
    */
}
