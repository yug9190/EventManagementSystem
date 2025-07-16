using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Context;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.SignalR;
using EventManagementSystem.Hubs;

namespace EventManagementSystem.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsDbContext _context;
        private readonly IHubContext<BookingHub> _hubContext;

        public EventsController(EventsDbContext context, IHubContext<BookingHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Events
        public async Task<IActionResult> Index(string searchString = null)
        {
            var events = from e in _context.EventsTable select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                events = events.Where(e =>
                    e.EventName.ToLower().Contains(searchString) ||
                    e.Category.ToLower().Contains(searchString) ||
                    e.StartDate.ToString().ToLower().Contains(searchString) ||
                    e.EndDate.ToString().ToLower().Contains(searchString) ||
                    e.Venue.ToLower().Contains(searchString) ||
                    e.Status.ToLower().Contains(searchString) 
                );
            }

            return View(await events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evt = await _context.EventsTable.FirstOrDefaultAsync(e => e.EventId == id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,Category,StartDate,EndDate,Venue,Status,")] EventsTable evt)
        {
            if (ModelState.IsValid)
            {
                _context.EventsTable.Add(evt);
                await _context.SaveChangesAsync();

                // Notify clients via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveBooking", new
                {
                    action = "Created",
                    eventId = evt.EventId,
                    name = evt.EventName,
                    status = evt.Status
                });

                return RedirectToAction(nameof(Index));
            }

            return View(evt);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evt = await _context.EventsTable.FindAsync(id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,Category,StartDate,EndDate,Venue,Status")] EventsTable evt)
        {
            if (id != evt.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evt);
                    await _context.SaveChangesAsync();

                    // Notify clients via SignalR
                    await _hubContext.Clients.All.SendAsync("ReceiveBooking", new
                    {
                        action = "Updated",
                        eventId = evt.EventId,
                        name = evt.EventName, 
                        status = evt.Status
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(evt.EventId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(evt);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evt = await _context.EventsTable.FirstOrDefaultAsync(e => e.EventId == id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evt = await _context.EventsTable.FindAsync(id);
            if (evt != null)
            {
                _context.EventsTable.Remove(evt);
                await _context.SaveChangesAsync();

                // Notify clients via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveBooking", new
                {
                    action = "Deleted",
                    eventId = evt.EventId,
                    name = evt.EventName,
                    status = evt.Status
                });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.EventsTable.Any(e => e.EventId == id);
        }
    }
}
