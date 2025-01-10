using FrontendService.Models.Users;
using FrontendService.Service.userService;
using Microsoft.AspNetCore.Mvc;

namespace FrontendService.Controllers.Users
{
    public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();
        return View(users);
    }

    public async Task<IActionResult> Details(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            await _userService.AddUserAsync(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
}
}

