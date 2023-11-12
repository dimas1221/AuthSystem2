using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem2.Data;
using AuthSystem2.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthSystem2ContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthSystem2ContextConnection' not found.");

builder.Services.AddDbContext<AuthSystem2Context>(options => options.UseSqlServer(connectionString));

//to confim acc email true or false
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthSystem2Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//razor pages
builder.Services.AddRazorPages();

//validate
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//routeRazorPages
app.MapRazorPages();

app.Run();
