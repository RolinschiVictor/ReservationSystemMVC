using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystemMVC.Core.Patterns.Composite;

public class MenuComposite : IMenuComponent
{
    private readonly List<IMenuComponent> _components = new();
    public string Name { get; }

    public MenuComposite(string name)
    {
        Name = name;
    }

    public void AddComponent(IMenuComponent component)
    {
        _components.Add(component);
    }

    public void RemoveComponent(IMenuComponent component)
    {
        _components.Remove(component);
    }

    public decimal GetPrice()
    {
        return _components.Sum(c => c.GetPrice());
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + $" + {Name} [Total: {GetPrice():C}]");
        foreach (var component in _components)
        {
            component.Display(depth + 2);
        }
    }
}