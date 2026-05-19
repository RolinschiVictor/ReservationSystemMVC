using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Patterns.Command;
using ReservationSystemMVC.Core.Patterns.Memento;
using ReservationSystemMVC.Models;
using ReservationSystemMVC.Services;

namespace ReservationSystemMVC.Controllers;

[Authorize]
public class DraftController : Controller
{
    private const string DRAFT_SESSION_KEY = "BookingDraft";
    private const string DRAFT_PREVIOUS_SESSION_KEY = "BookingDraft.Previous";

    // 1. SAVE DRAFT (Command + Memento Patterns)
    [HttpPost]
    public IActionResult SaveDraft([FromBody] BookingCreateViewModel vm)
    {
        var store = new SessionDraftStateStore(HttpContext.RequestServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());
        var originator = new BookingDraftOriginator
        {
            ResourceId = vm.ResourceId,
            FullName = vm.FullName,
            Email = vm.Email,
            PhoneNumber = vm.PhoneNumber,
            StartDate = vm.StartDate,
            EndDate = vm.EndDate,
            GuestsCount = vm.GuestsCount,
            SpecialRequests = vm.SpecialRequests
        };

        // Command Pattern: executes the save, and keeps the previous state for Undo
        var saveCommand = new SaveDraftCommand(originator, store);
        saveCommand.Execute();

        return Json(new { success = true, message = "Draft saved successfully!" });
    }

    // 1b. UNDO last SAVE (Command Pattern)
    [HttpPost]
    public IActionResult UndoDraft()
    {
        var store = new SessionDraftStateStore(HttpContext.RequestServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());

        var current = store.GetCurrent();
        var originator = new BookingDraftOriginator();
        if (!string.IsNullOrEmpty(current))
        {
            originator.RestoreFromMemento(new BookingDraftMemento(current));
        }

        var undoCommand = new UndoDraftCommand(originator, store);
        undoCommand.Execute();

        var restored = store.GetCurrent();
        return Json(new { success = !string.IsNullOrEmpty(restored), hasDraft = !string.IsNullOrEmpty(restored) });
    }

    // 2. RESTORE DRAFT (Memento Pattern)
    [HttpGet]
    public IActionResult RestoreDraft()
    {
        var json = HttpContext.Session.GetString(DRAFT_SESSION_KEY);
        if (string.IsNullOrEmpty(json))
        {
            return RedirectToAction("Index", "Home");
        }

        var memento = new BookingDraftMemento(json);
        var originator = new BookingDraftOriginator();
        originator.RestoreFromMemento(memento);

        // Map Originator back to ViewModel and redirect to Booking Create
        return RedirectToAction("Create", "Booking", new 
        { 
            resourceId = originator.ResourceId,
            startDate = originator.StartDate,
            endDate = originator.EndDate,
            fullName = originator.FullName,
            email = originator.Email,
            phoneNumber = originator.PhoneNumber,
            guestsCount = originator.GuestsCount,
            specialRequests = originator.SpecialRequests
        });
    }

    [HttpPost]
    public IActionResult ClearDraft()
    {
        HttpContext.Session.Remove(DRAFT_SESSION_KEY);
        HttpContext.Session.Remove(DRAFT_PREVIOUS_SESSION_KEY);
        return Json(new { success = true });
    }
}
