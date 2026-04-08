using System;

namespace ReservationSystemMVC.Core.Patterns.Composite;

public class FoodItem : IMenuComponent
{
    public string Name { get; }
    private readonly decimal _price;

    public FoodItem(string name, decimal price)
    {
        Name = name;
        _price = price;
    }

    public decimal GetPrice() => _price;

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + $" {Name} - {_price:C}");
    }
}