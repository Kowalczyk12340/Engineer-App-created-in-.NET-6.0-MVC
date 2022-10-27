using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Initializer;
using Microsoft.AspNetCore.Identity.UI.Services;
using EngineerApplication.Helpers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.ContextStructure.Data.Service;
using EngineerApplication.Resources;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Security.Principal;
using EngineerApplication.Entities;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;

#if DEBUG
services.AddRazorPages().AddRazorRuntimeCompilation();
#else
    services.AddMvc(options => options.EnableEndpointRouting = false);
#endif

services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
});

services.AddDbContext<EngineerDbContext>(options =>
    options.UseSqlServer(
        config.GetConnectionString("Database")))
  .AddDbContext<EngineerDbContext>(options =>
    options.UseSqlServer(
        config.GetConnectionString("AzureDatabase")));

services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
  options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<EngineerDbContext>()
  .AddDefaultTokenProviders();

services.Configure<RequestLocalizationOptions>(options =>
{
  var supportedCultures = new[]
  {
          new CultureInfo("pl"),
          new CultureInfo("en"),
          new CultureInfo("de")
        };
  options.DefaultRequestCulture = new RequestCulture("en");
  options.SupportedCultures = supportedCultures;
  options.SupportedUICultures = supportedCultures;
});

services.AddSingleton<IEmailSender, EmailSender>();
services.AddRazorPages().AddViewLocalization().AddRazorOptions(options =>
{
  options.ViewLocationFormats.Add("/{0}.cshtml");
});
services.AddSingleton<CommonLocalizationService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
// Inject IPrincipal
services.AddHttpContextAccessor();
services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
services.AddScoped<ISeederToDatabase, SeederToDatabase>();
services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(30);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});

services.Configure<IdentityOptions>(options =>
{
  options.Password.RequireDigit = true;
  options.Password.RequiredLength = 8;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireUppercase = true;
  options.Password.RequireLowercase = false;
  options.Password.RequiredUniqueChars = 6;

  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
  options.Lockout.MaxFailedAccessAttempts = 10;
  options.Lockout.AllowedForNewUsers = true;

  options.User.RequireUniqueEmail = true;
});

services.ConfigureApplicationCookie(options =>
{
  options.Cookie.HttpOnly = true;
  options.ExpireTimeSpan = TimeSpan.FromDays(300);
  options.LoginPath = "/Account/Login";
  options.AccessDeniedPath = "/Account/AccessDenied";
  options.SlidingExpiration = true;
});

services.AddLocalization(options => options.ResourcesPath = "Resources");
services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
services.AddRazorPages();

services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
  app.UseHttpsRedirection();
}

await SeedDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable CS8602 // Wy³uskanie odwo³ania, które mo¿e mieæ wartoœæ null.
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
#pragma warning restore CS8602 // Wy³uskanie odwo³ania, które mo¿e mieæ wartoœæ null.
app.UseRequestLocalization(localizationOptions);

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllerRoute(
            name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
  endpoints.MapRazorPages();
});

app.Run();

async Task SeedDatabase()
{
  using var scope = app.Services.CreateScope();
  var dbSeeder = scope.ServiceProvider.GetRequiredService<ISeederToDatabase>();
  await dbSeeder.HighlightDatabaseAsync();
}

#pragma warning disable S1118 // Utility classes should not have public constructors
#pragma warning disable S3903 // Types should be defined in named namespaces
#pragma warning disable CA1050 // Zadeklaruj typy w przestrzeniach nazw
public partial class Program { }
#pragma warning restore CA1050 // Zadeklaruj typy w przestrzeniach nazw
#pragma warning restore S3903 // Types should be defined in named namespaces
#pragma warning restore S1118 // Utility classes should not have public constructors