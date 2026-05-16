using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Patterns.Memento;

namespace ReservationSystemMVC.Core.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class SaveDraftCommand : ICommand
    {
        private readonly BookingDraftOriginator _originator;
        private readonly BookingDraftMemento _memento;

        public SaveDraftCommand(BookingDraftOriginator originator)
        {
            _originator = originator;
            _memento = originator.SaveToMemento();
        }

        public void Execute()
        {
            // The state is already captured in the Memento during object creation.
            // Executing the command could mean persisting the Memento's state somewhere (e.g. Session/File/DB).
        }

        public void Undo()
        {
            // Restore state to originator
            _originator.RestoreFromMemento(_memento);
        }

        public string GetStateJson() => _memento.StateJson;
    }
}
