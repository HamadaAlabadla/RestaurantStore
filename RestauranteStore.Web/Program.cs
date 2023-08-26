using Excel.Infrastructure.Services.ProductServices;
using Hangfire;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NToastNotify;
using OfficeOpenXml;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.CategoryService;
using RestauranteStore.Infrastructure.Services.FileService;
using RestauranteStore.Infrastructure.Services.OrderItemsService;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.ProductService;
using RestauranteStore.Infrastructure.Services.QuantityService;
using RestauranteStore.Infrastructure.Services.RestoranteService;
using RestauranteStore.Infrastructure.Services.UnitPriceService;
using RestauranteStore.Infrastructure.Services.UserService;
using RestaurantStore.Infrastructure.AutoMapper;
using RestaurantStore.Infrastructure.Hubs;
using RestaurantStore.Infrastructure.Services.EmailService;
using RestaurantStore.Infrastructure.Services.NotificationService;
using RestaurantStore.Infrastructure.Services.OrderService;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("RestauranteStoreConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Home/Welcom";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
	ProgressBar = true,
	CloseButton = true,
	TimeOut = 5000,
	HideDuration = 3000,
	ExtendedTimeOut = 3000,
	ShowDuration = 3000,
	TapToDismiss = true,
	CloseOnHover = true,
	EscapeHtml = false,

});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IQuantityService, QuantityService>();
builder.Services.AddScoped<IUnitPriceService, UnitPriceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<IManagementProductService, ManagementProductService>();

builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddSignalR();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(1);
});

builder.Services.Configure<IISServerOptions>(options =>
{
	options.MaxRequestBodySize = int.MaxValue; // Maximum request body size in bytes (80 MB)
});

builder.Services.Configure<FormOptions>(options =>
{
	options.ValueCountLimit = int.MaxValue; // Set to a desired value or int.MaxValue for no limit
	options.MultipartBodyLengthLimit = long.MaxValue; // Set to a desired value or long.MaxValue for no limit
	options.MemoryBufferThreshold = int.MaxValue; // Set to a desired value or int.MaxValue for no limit
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
	options.Limits.MaxRequestBodySize = int.MaxValue; // Set to desired value or int.MaxValue for no limit
});

ServicePointManager.MaxServicePointIdleTime = 10000; // Adjust this value as needed
ServicePointManager.DefaultConnectionLimit = 100;

// Add framework services.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
GlobalConfiguration.Configuration.UseSerializerSettings(new JsonSerializerSettings
{
	ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
	// Add other JSON settings as needed
});
app.UseHangfireDashboard("/dashboard");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Users}/{action=Welcom}/{id?}");


app.MapRazorPages();
app.MapHub<NotificationHub>("/NotificationHub");
app.MapHub<FileUploadHub>("/fileUploadHub");


app.Run();
