using Clothing.CMS.Entities.Authorization.Roles;
using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Clothing.CMS.Logging;
using Clothing.CMS.Web.Areas.Admin.Data;
using Clothing.CMS.Web.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// For Entity Framework
var connectionString = builder.Configuration["ConnectionStrings:Database"];
builder.Services.AddDbContext<CMSDbContext>(o => o.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOption => { sqlOption.EnableRetryOnFailure(); }));

// Duplicate role names and user names
builder.Services.AddScoped<IRoleValidator<Role>, CustomRoleValidator>();
builder.Services.AddScoped<IUserValidator<User>, CustomUserValidator>();

//Identity
builder.Services.AddIdentity<User, Role>()
				.AddRoleValidator<CustomRoleValidator>()
				.AddUserValidator<CustomUserValidator>()
				.AddEntityFrameworkStores<CMSDbContext>()
				.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromDays(30);
	options.LoginPath = $"/Admin/Account/Login";
	options.LogoutPath = $"/Admin/Account/Logout";
	options.AccessDeniedPath = $"/Admin/Account/AccessDenied";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromDays(1);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

//Add LogEvent
builder.Logging.AddDbLogger(options =>
{
	builder.Configuration.GetSection("Logging").GetSection("Database").GetSection("Options").Bind(options);
});

GlobalHelper.RegisterServiceLifetimer(builder.Services);
GlobalHelper.RegisterAutoMapper(builder.Services);

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
	name: "Admin",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

// Populate default user admin
DataSeed.Seed(app.Services).Wait();

app.Run();
