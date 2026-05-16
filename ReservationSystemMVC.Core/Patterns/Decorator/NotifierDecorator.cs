using System;

namespace ReservationSystemMVC.Core.Patterns.Decorator;

// Componenta de bază
public interface INotifier
{
    void Send(string message);
}

// Implementarea concretă
public class BaseNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"[Base System Log] Sending baseline notification: {message}");
    }
}

// Decoratorul de bază
public abstract class NotifierDecorator : INotifier
{
    protected INotifier _wrappee;
    
    public NotifierDecorator(INotifier wrappee)
    {
        _wrappee = wrappee;
    }

    public virtual void Send(string message)
    {
        _wrappee.Send(message);
    }
}

// Decorator concret 1
public class EmailNotifier : NotifierDecorator
{
    public EmailNotifier(INotifier wrappee) : base(wrappee) { }
    
    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"[Email Module] Sending Email Notification: {message}");
    }
}

// Decorator concret 2
public class SmsNotifier : NotifierDecorator
{
    public SmsNotifier(INotifier wrappee) : base(wrappee) { }
    
    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"[SMS Module] Sending SMS Notification: {message}");
    }
}
