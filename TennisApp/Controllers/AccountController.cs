using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TennisApp.Data.Models;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    // Injecting UserManager<ApplicationUser> into the constructor
    public AccountController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User); // Example of how to use UserManager
        return View(user);
    }
}