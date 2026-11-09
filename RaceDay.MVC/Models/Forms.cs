using System.ComponentModel.DataAnnotations;
namespace RaceDay.MVC.Models;
public class LoginForm{[Required,EmailAddress]public string Email{get;set;}="";[Required]public string Password{get;set;}="";}
public class RegisterForm{[Required]public string FirstName{get;set;}="";[Required]public string LastName{get;set;}="";[Required,EmailAddress]public string Email{get;set;}="";[Required,MinLength(8)]public string Password{get;set;}="";public string Role{get;set;}="Participant";}
public class EventForm{public int EventId{get;set;}public string Name{get;set;}="";public string Description{get;set;}="";public DateTime EventDate{get;set;}public string Location{get;set;}="";public decimal Distance{get;set;}public string EventType{get;set;}="run";public string? BannerUrl{get;set;}public List<CategoryForm> Categories{get;set;}=[];}
public class CategoryForm{public int CategoryId{get;set;}public string CategoryName{get;set;}="";public string CategoryType{get;set;}="Age";}
public class CreateEventForm{[Required]public string Name{get;set;}="";public string Description{get;set;}="";public DateTime EventDate{get;set;}=DateTime.Today.AddDays(14);[Required]public string Location{get;set;}="";[Range(.1,1000)]public decimal Distance{get;set;}=5;public string EventType{get;set;}="run";public IFormFile? Banner{get;set;}}
