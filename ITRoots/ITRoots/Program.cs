using AutoMapper;
using ITRoots.BL.Interface;
using ITRoots.BL.Mapper;
using ITRoots.BL.Repository;
using ITRoots.DAL.Database;
using ITRoots.DAL.Entities;
using ITRoots.language;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()

         //////language
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
});
        //////End language

builder.Services.AddDbContext<ITRootsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connectionstr")); // Connection String
});
// Auto Mapper Configurations *****************
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
builder.Services.AddScoped<IProductRep, ProductRep>(); //dependency injection
builder.Services.AddScoped<IInvoicesRep, InvoicesRep>(); //dependency injection
builder.Services.AddScoped<IProductInvoicesRep, ProductInvoicesRep>(); //dependency injection



//Identity
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     options.LoginPath = new PathString("/Account/Login");
     options.AccessDeniedPath = new PathString("/Home/Error");
 });
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 5;
    options.SignIn.RequireConfirmedAccount = false;
})
            .AddEntityFrameworkStores<ITRootsContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

// End Identity

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    app.UseHsts();
}
////////////////////////////      language
var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});

///////////////////////////// End language






app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
