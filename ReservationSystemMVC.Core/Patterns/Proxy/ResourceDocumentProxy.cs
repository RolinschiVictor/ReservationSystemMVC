using System;
using System.Threading;

namespace ReservationSystemMVC.Core.Patterns.Proxy;

// Interfața comună
public interface IResourceDocument
{
    string GetContent();
}

// Obiectul Real, greu de instanțiat și accesat
public class RealResourceDocument : IResourceDocument
{
    private readonly string _documentName;
    private readonly string _content;

    public RealResourceDocument(string documentName)
    {
        _documentName = documentName;
        LoadFromDisk();
        _content = $"[INTERNAL DOCUMENT]\nName: {_documentName}\nGeneratedAtUtc: {DateTime.UtcNow:O}\n\nThis is an internal document available only to Admin users.";
    }

    private void LoadFromDisk()
    {
        Console.WriteLine($"[RealResourceDocument] Loading heavy document '{_documentName}' from disk... (simulating delay)");
        Thread.Sleep(1000); // Simulăm o resursă "grea"
    }

    public string GetContent() => _content;
}

// Proxy-ul care controlează accesul (Încărcare leneșă + Protecție)
public class ProxyResourceDocument : IResourceDocument
{
    private RealResourceDocument _realDocument;
    private readonly string _documentName;
    private readonly string _userRole;

    public ProxyResourceDocument(string documentName, string userRole)
    {
        _documentName = documentName;
        _userRole = userRole;
    }

    public string GetContent()
    {
        // 1. Protection Proxy: Controlăm accesul pe baza rolului
        if (_userRole != "Admin")
        {
            Console.WriteLine($"[Proxy] Access Denied: User role '{_userRole}' cannot view document '{_documentName}'.");
            return "Access denied. This document is available only for Admin users.";
        }

        // 2. Virtual Proxy: Creăm instanța "grea" doar la prima cerere (Lazy Initialization)
        if (_realDocument == null)
        {
            Console.WriteLine($"[Proxy] Initializing real document '{_documentName}' for the first time...");
            _realDocument = new RealResourceDocument(_documentName);
        }

        // Delegăm cererea obiectului real
        return _realDocument.GetContent();
    }
}
