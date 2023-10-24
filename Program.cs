using Admin.Controllers;
using Admin.Models;
using Admin.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<QLBanDTContext>(options => options.UseSqlServer
                        (builder.Configuration.GetConnectionString("QLBanDT")));

//builder.Services.AddSingleton<QLBanDTContext>();
builder.Services.AddTransient<ProductServices>();
// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

/*using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
   
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TSps}/{action=Index}/{id?}");

app.Run();
