using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;
using System.Text.Json;

namespace RaceDay.MVC.Controllers;

public class AccountController(IRaceDayApi api) : Controller
{
    private static JsonElement Find(JsonElement e, string name) =>
        e.EnumerateObject().FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)).Value;

    [HttpGet] public IActionResult Login() => View(new LoginForm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginForm f)
    {
        if (!ModelState.IsValid) return View(f);
        try
        {
            var x = await api.Post<JsonElement>("api/auth/login", new { f.Email, f.Password });
            var id = Find(x, "id");
            var role = Find(x, "role");
            HttpContext.Session.SetString("uid", id.ToString());
            HttpContext.Session.SetString("role", role.GetString() ?? "");
            var first = Find(x, "firstName");
            var last = Find(x, "lastName");
            HttpContext.Session.SetString("displayName", $"{first.GetString()} {last.GetString()}");
            return RedirectToAction("Index", HttpContext.Session.GetString("role") == "Organiser" ? "Organiser" : "Participant");
        }
        catch { ModelState.AddModelError("", "Login failed. Check your details and confirm the API is running."); return View(f); }
    }

    [HttpGet] public IActionResult Register() => View(new RegisterForm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterForm f)
    {
        if (!ModelState.IsValid) return View(f);
        try
        {
            await api.Post<JsonElement>("api/auth/register", new { f.FirstName, f.LastName, f.Email, f.Password, f.Role });
            TempData["Success"] = "Account created. Please log in.";
            return RedirectToAction(nameof(Login));
        }
        catch { ModelState.AddModelError("", "Registration failed. The email may already exist."); return View(f); }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        try { await api.Post<JsonElement>("api/auth/logout", new { }); } catch { }
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
