namespace RaceDay.MVC.Services;
public interface IRaceDayApi { Task<T?> Get<T>(string p); Task<T?> Post<T>(string p,object d); Task<T?> Put<T>(string p,object d); Task<bool> Delete(string p); Task<T?> Upload<T>(string p,IFormFile f); }
