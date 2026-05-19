using Microsoft.AspNetCore.Http;
using ReservationSystemMVC.Core.Patterns.Command;

namespace ReservationSystemMVC.Services;

public sealed class SessionDraftStateStore : IDraftStateStore
{
    private readonly ISession _session;

    private const string CurrentKey = "BookingDraft";
    private const string PreviousKey = "BookingDraft.Previous";

    public SessionDraftStateStore(IHttpContextAccessor accessor)
    {
        _session = accessor.HttpContext?.Session
            ?? throw new InvalidOperationException("Session is not available. Ensure session middleware is enabled and the request has an HttpContext.");
    }

    public string? GetCurrent() => _session.GetString(CurrentKey);

    public string? GetPrevious() => _session.GetString(PreviousKey);

    public void SetCurrent(string stateJson) => _session.SetString(CurrentKey, stateJson);

    public void SetPrevious(string stateJson) => _session.SetString(PreviousKey, stateJson);

    public void ClearCurrent() => _session.Remove(CurrentKey);

    public void ClearPrevious() => _session.Remove(PreviousKey);
}
