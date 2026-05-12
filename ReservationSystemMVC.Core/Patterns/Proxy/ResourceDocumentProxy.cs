using System;
using System.Threading;

namespace ReservationSystemMVC.Core.Patterns.Proxy;

// Interfața comună
public interface IResourceDocument
{
    void DisplayDocument();
}

// Obiectul Real, greu de instanțiat și accesat
public class RealResourceDocument : IResourceDocument
{
    private readonly string _documentName;

    public RealResourceDocument(string documentName)
    {
        _documentName = documentName;
        LoadFromDisk();
    }

    private void LoadFromDisk()
    {
        Console.WriteLine($"[RealResourceDocument] Loading heavy document '{_documentName}' from disk... (simulating delay)");
        Thread.Sleep(1000); // Simulăm o resursă "grea"
    }

    public void DisplayDocument()
    {
        Console.WriteLine($"[RealResourceDocument] Displaying sensitive document: {_documentName}");
    }
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

    public void DisplayDocument()
    {
        // 1. Protection Proxy: Controlăm accesul pe baza rolului
        if (_userRole != "Admin")
        {
            Console.WriteLine($"[Proxy] Access Denied: User role '{_userRole}' cannot view document '{_documentName}'.");
            return;
        }

        // 2. Virtual Proxy: Creăm instanța "grea" doar la prima cerere (Lazy Initialization)
        if (_realDocument == null)
        {
            Console.WriteLine($"[Proxy] Initializing real document '{_documentName}' for the first time...");
            _realDocument = new RealResourceDocument(_documentName);
        }

        // Delegăm cererea obiectului real
        _realDocument.DisplayDocument();
    }
}
