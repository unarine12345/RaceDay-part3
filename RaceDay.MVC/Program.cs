using RaceDay.MVC.Services;
var b=WebApplication.CreateBuilder(args);
b.Services.AddControllersWithViews();
b.Services.AddDistributedMemoryCache();
b.Services.AddSession(o=>{o.IdleTimeout=TimeSpan.FromHours(8);o.Cookie.HttpOnly=true;o.Cookie.IsEssential=true;});
b.Services.AddHttpContextAccessor();
b.Services.AddHttpClient<IRaceDayApi,RaceDayApi>((sp,c)=>c.BaseAddress=new Uri(sp.GetRequiredService<IConfiguration>()["ApiBaseUrl"]??"http://localhost:63904/"));
var app=b.Build(); app.UseStaticFiles(); app.UseRouting(); app.UseSession();
app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}"); app.Run();
