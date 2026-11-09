using Microsoft.AspNetCore.Mvc; using RaceDay.MVC.Services;
namespace RaceDay.MVC.Controllers;
public class HomeController(IRaceDayApi api):Controller {public async Task<IActionResult> Index(){try{ViewBag.Events=await api.Get<List<System.Text.Json.JsonElement>>("api/events")??[];}catch{ViewBag.Events=new();}return View();}}
