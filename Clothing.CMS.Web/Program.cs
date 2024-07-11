using Clothing.CMS.EntityFrameworkCore.Pattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// For Entity Framework
var connectionString = builder.Configuration["ConnectionStrings:Database"];
builder.Services.AddDbContext<CMSDbContext>(o => o.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOption => { sqlOption.EnableRetryOnFailure(); }));

//Identity
builder.Services.AddIdentity<CMSIdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<CMSDbContext>()
				.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
	name: "Admin",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
