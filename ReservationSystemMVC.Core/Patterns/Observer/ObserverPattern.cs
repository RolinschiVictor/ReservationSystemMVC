using ReservationSystemMVC.Core.Domain.Entities;
using System.Collections.Generic;

namespace ReservationSystemMVC.Core.Patterns.Observer
{
    public interface IObserver
    {
        void Update(string message);
    }

    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }

    public class ResourceAvailabilityNotifier : IObservable
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }

    // Simple concrete observer used by the application to react to booking events
    public class BookingLoggerObserver : IObserver
    {
        public void Update(string message)
        {
            // In a real application this could send emails, push notifications or persist to a log store.
            System.Console.WriteLine($"[BookingObserver] {message}");
        }
    }
}
