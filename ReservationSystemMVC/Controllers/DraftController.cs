using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Patterns.Command;
using ReservationSystemMVC.Core.Patterns.Memento;
using ReservationSystemMVC.Models;

namespace ReservationSystemMVC.Controllers;

[Authorize]
public class DraftController : Controller
{
    private const string DRAFT_SESSION_KEY = "BookingDraft";

    // 1. SAVE DRAFT (Command + Memento Patterns)
    [HttpPost]
    public IActionResult SaveDraft([FromBody] BookingCreateViewModel vm)
    {
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

        // Uses Command to orchestrate saving the Memento
        var saveCommand = new SaveDraftCommand(originator);
        saveCommand.Execute();

        // Persist the Memento's state json to Session
        HttpContext.Session.SetString(DRAFT_SESSION_KEY, saveCommand.GetStateJson());

        return Json(new { success = true, message = "Draft saved successfully!" });
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
        return Json(new { success = true });
    }
}
