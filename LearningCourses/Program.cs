using LearningCourses.Data;
using LearningCourses.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ICourseRepository, CourseRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // Add API controllers
builder.Services.AddEndpointsApiExplorer(); // Enables API endpoint exploration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

Log.Logger=new LoggerConfiguration().WriteTo.Console().WriteTo.File("Logs/log-.txt",rollingInterval:RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map API Controllers
app.MapControllers();

// Map MVC Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
