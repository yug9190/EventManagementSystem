using EventManagementSystem.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    [Authorize(Roles = "Attendee")]
    public class AttendeeController : Controller
    {
        private readonly EventsDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AttendeeController(EventsDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.AppUserId == user.Id)
                .ToListAsync();

            return View(bookings);
        }
    }
}
