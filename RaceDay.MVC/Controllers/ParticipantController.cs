using Microsoft.AspNetCore.Mvc;using RaceDay.MVC.Services;using System.Text.Json;
namespace RaceDay.MVC.Controllers;
public class ParticipantController(IRaceDayApi api):Controller{
bool OkRole()=>HttpContext.Session.GetString("role")=="Participant";
public async Task<IActionResult> Index(){if(!OkRole())return RedirectToAction("Login","Account");try{return View(await api.Get<List<JsonElement>>("api/events")??[]);}catch{return View(new List<JsonElement>());}}
public async Task<IActionResult> MyEvents(){if(!OkRole())return RedirectToAction("Login","Account");try{return View(await api.Get<List<JsonElement>>("api/enrolments/mine")??[]);}catch{return View(new List<JsonElement>());}}
public async Task<IActionResult> MyResults(){if(!OkRole())return RedirectToAction("Login","Account");try{return View(await api.Get<List<JsonElement>>("api/results/mine")??[]);}catch{return View(new List<JsonElement>());}}
[HttpPost]public async Task<IActionResult> Enrol(int id,int categoryId){if(!OkRole())return RedirectToAction("Login","Account");try{await api.Post<JsonElement>($"api/enrolments/events/{id}",new{CategoryId=categoryId});TempData["msg"]="Enrolment saved.";}catch{TempData["err"]="Enrolment failed; check if already enrolled.";}return RedirectToAction("Index");}}
