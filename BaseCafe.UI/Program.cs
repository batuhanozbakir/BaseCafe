using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.BLL.Managers.Concrete;
using BaseCafe.DAL.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDalService();
builder.Services.AddScoped(typeof(IGenericManager<,>) , typeof(GenericManager<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
