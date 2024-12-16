using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Workloopz.Data;
using Workloopz.Helpers;

using Microsoft.AspNet.SignalR;
using Workloopz;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NexTasksContext>(options => { 
        options.UseSqlServer(builder.Configuration.GetConnectionString("NexTask"));
});
builder.Services.AddAuthentication("Cookies")
	.AddCookie(options => {
		options.LoginPath = "/Home/Login";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
	});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Cấu hình Session
builder.Services.AddDistributedMemoryCache();  // Dùng bộ nhớ để lưu trữ session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Thời gian hết hạn session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Real-time
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapHub<ChatHub>("/chatHub");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
