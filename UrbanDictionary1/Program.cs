using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using UrbanDictionary1.Data;
using UrbanDictionary1.Data.Services;
using Microsoft.AspNetCore.Identity;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext service
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>();
#region Authorization

AddAuthorizationPolicies();

#endregion


builder.Services.Configure<IdentityOptions>(options =>
{
    
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric= false;
    
});

//Services configuration
builder.Services.AddScoped<IExpressionsService, ExpressionsService>();
builder.Services.AddScoped<ISidebarService, SidebarService>();
builder.Services.AddScoped<IContactService, ContactService>();


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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expressions}/{action=Index}/{id?}");


app.Run();

void AddAuthorizationPolicies()
{
   
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdmx", policy => policy.RequireRole("Admx"));
    });
}