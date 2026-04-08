namespace ReservationSystemMVC.Core.Patterns.Composite;

public interface IMenuComponent
{
    string Name { get; }
    decimal GetPrice();
    void Display(int depth = 0);
}