
using firstAppAsp.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnnection") ?? throw  new InvalidOperationException("No Connection String was found");
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString) );

builder.Services.AddScoped<ICategoriesService,CategoriesService>();
builder.Services.AddScoped<IDevicesServices,DevicesServices>();
builder.Services.AddScoped<IGamesServices,GamesServices>();

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
