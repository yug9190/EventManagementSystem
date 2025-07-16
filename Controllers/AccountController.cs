using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [Route("account")] // Prefix route to avoid ambiguity
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /account/login
        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /account/login
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var roles = await _userManager.GetRolesAsync(user);

                // ❌ Block admin login through regular login page
                if (roles.Contains("Admin") && !model.AllowAdminLogin)
                {
                    ModelState.AddModelError(string.Empty, "Admin login is not allowed through this page.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName!,
                    model.Password!,
                    model.RememberMe,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    if (roles.Contains("Admin"))
                        return RedirectToAction("Dashboard", "Admin");

                    if (roles.Contains("Organizer"))
                        return RedirectToAction("index", "Events");

                    return RedirectToAction("Dashboard", "Attendee");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // GET: /account/register
        [HttpGet("register")]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var model = new RegisterVM
            {
                AvailableRoles = new List<string> { "Organizer", "Attendee" }
            };

            return View(model);
        }

        // POST: /account/register
        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                if (model.Role == "Admin")
                {
                    ModelState.AddModelError(string.Empty, "Admin registration is not allowed.");
                    model.AvailableRoles = new List<string> { "Organizer", "Attendee" };
                    return View(model);
                }

                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync(model.Role))
                        await _userManager.AddToRoleAsync(user, model.Role);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (model.Role == "Organizer")
                        return RedirectToAction("index", "Events");

                    return RedirectToAction("Dashboard", "Attendee");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.AvailableRoles = new List<string> { "Organizer", "Attendee" };
            return View(model);
        }

        // POST: /account/logout
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
