using System.Net.Http.Json; using System.Net.Http.Headers;
namespace RaceDay.MVC.Services;
public class RaceDayApi(HttpClient h,IHttpContextAccessor a):IRaceDayApi {
 void Cookie(){h.DefaultRequestHeaders.Remove("Cookie");var c=a.HttpContext?.Session.GetString("api-cookie");if(c!=null)h.DefaultRequestHeaders.Add("Cookie",c);}
 async Task<HttpResponseMessage> Send(HttpRequestMessage r){Cookie();var x=await h.SendAsync(r);if(x.Headers.TryGetValues("Set-Cookie",out var vals)){var c=vals.FirstOrDefault(v=>v.StartsWith("RaceDay.Auth="));if(c!=null)a.HttpContext?.Session.SetString("api-cookie",c.Split(';')[0]);}return x;}
 public async Task<T?> Get<T>(string p){using var r=await Send(new(HttpMethod.Get,p));r.EnsureSuccessStatusCode();return await r.Content.ReadFromJsonAsync<T>();}
 public async Task<T?> Post<T>(string p,object d){using var r=await Send(new(HttpMethod.Post,p){Content=JsonContent.Create(d)});r.EnsureSuccessStatusCode();return await r.Content.ReadFromJsonAsync<T>();}
 public async Task<T?> Put<T>(string p,object d){using var r=await Send(new(HttpMethod.Put,p){Content=JsonContent.Create(d)});r.EnsureSuccessStatusCode();return await r.Content.ReadFromJsonAsync<T>();}
 public async Task<bool> Delete(string p){using var r=await Send(new(HttpMethod.Delete,p));return r.IsSuccessStatusCode;}
 public async Task<T?> Upload<T>(string p,IFormFile f){using var m=new MultipartFormDataContent();await using var s=f.OpenReadStream();using var c=new StreamContent(s);c.Headers.ContentType=new MediaTypeHeaderValue(f.ContentType);m.Add(c,"file",Path.GetFileName(f.FileName));using var r=await Send(new(HttpMethod.Post,p){Content=m});r.EnsureSuccessStatusCode();return await r.Content.ReadFromJsonAsync<T>();}
}
