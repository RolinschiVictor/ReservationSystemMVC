using System;
using System.Collections.Generic;

namespace ReservationSystemMVC.Core.Patterns.Flyweight;

// Flyweight: Partajarea stării intrinseci (nume, descriere, iconiță) pentru a reduce memoria
public class Amenity
{
    public string Name { get; }
    public string Description { get; }
    public string IconUrl { get; }

    public Amenity(string name, string description, string iconUrl)
    {
        Name = name;
        Description = description;
        IconUrl = iconUrl;
    }

    public void Display(string resourceName)
    {
        // Starea extrinsecă este resourceName
        Console.WriteLine($"[Amenity: {Name}] attached to Resource: {resourceName}. Icon: {IconUrl}");
    }
}

// Flyweight Factory: Gestionează și creează obiectele Flyweight
public class AmenityFactory
{
    private readonly Dictionary<string, Amenity> _amenities = new();

    public Amenity GetAmenity(string name, string description, string iconUrl)
    {
        if (!_amenities.ContainsKey(name))
        {
            Console.WriteLine($"[Flyweight] Creating new Amenity: {name}");
            _amenities[name] = new Amenity(name, description, iconUrl);
        }
        else
        {
            Console.WriteLine($"[Flyweight] Reusing existing Amenity: {name}");
        }
        
        return _amenities[name];
    }
    
    public int TotalAmenitiesCreated => _amenities.Count;
}
