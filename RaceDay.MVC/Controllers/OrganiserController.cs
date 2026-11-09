using Microsoft.AspNetCore.Mvc;using RaceDay.MVC.Models;using RaceDay.MVC.Services;using System.Text.Json;
namespace RaceDay.MVC.Controllers;
public class OrganiserController(IRaceDayApi api):Controller{
bool OkRole()=>HttpContext.Session.GetString("role")=="Organiser";
public async Task<IActionResult> Index(){if(!OkRole())return RedirectToAction("Login","Account");try{return View(await api.Get<List<EventForm>>("api/events")??[]);}catch{return View(new List<EventForm>());}}
[HttpGet]public IActionResult CreateEvent()=>OkRole()?View(new CreateEventForm()):RedirectToAction("Login","Account");
[HttpPost]public async Task<IActionResult> CreateEvent(CreateEventForm f){if(!OkRole())return RedirectToAction("Login","Account");if(!ModelState.IsValid)return View(f);try{string? url=null;if(f.Banner!=null){var u=await api.Upload<JsonElement>("api/uploads/event-banner",f.Banner);url=u.GetProperty("url").GetString();}await api.Post<JsonElement>("api/events",new{f.Name,f.Description,f.EventDate,f.Location,f.Distance,f.EventType,BannerUrl=url});return RedirectToAction("Index");}catch{ModelState.AddModelError("","Unable to save event. Check API and Azure Blob configuration.");return View(f);}}
[HttpPost]public async Task<IActionResult> Delete(int id){if(!OkRole())return RedirectToAction("Login","Account");await api.Delete($"api/events/{id}");return RedirectToAction("Index");}}
