namespace ReservationSystemMVC.Core.Domain.Abstractions;

/// PROTOTYPE PATTERN
/// Defines the contract for cloning objects.

public interface IPrototype<out T>
{
    /// Creates a deep copy of this object (with a new unique Id).

    T Clone();
}
