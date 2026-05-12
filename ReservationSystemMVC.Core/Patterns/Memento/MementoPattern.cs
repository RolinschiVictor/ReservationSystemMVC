using System;

namespace ReservationSystemMVC.Core.Patterns.Memento
{
    public class BookingMemento
    {
        public int Id { get; }
        public string ResourceId { get; }
        public string Status { get; }

        // Save relevant state

        public BookingMemento(int id, string resourceId, string status)
        {
            Id = id;
            ResourceId = resourceId;
            Status = status;
        }
    }

    public class BookingOriginator
    {
        public int Id { get; set; }
        public string ResourceId { get; set; }
        public string Status { get; set; }

        public BookingMemento SaveState()
        {
            return new BookingMemento(Id, ResourceId, Status);
        }

        public void RestoreState(BookingMemento memento)
        {
            Id = memento.Id;
            ResourceId = memento.ResourceId;
            Status = memento.Status;
        }
    }
}
