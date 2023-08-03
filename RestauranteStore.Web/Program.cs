using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
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

var builder = WebApplication.CreateBuilder(args);

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
	options.LoginPath = "/Identity/Account/Login";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
//builder.Services.AddSingleton<IEmailSender, EmailService>();



builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddSignalR();
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Welcom}/{id?}");
app.MapRazorPages();
app.MapHub<NotificationHub>("/NotificationHub");

app.Run();
