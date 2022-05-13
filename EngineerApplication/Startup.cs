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

namespace EngineerApplication
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
      });

      services.AddDbContext<EngineerDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("Database")));
      services.AddIdentity<IdentityUser, IdentityRole>()
          .AddEntityFrameworkStores<EngineerDbContext>()
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
      services.AddMvc().AddViewLocalization().AddRazorOptions(options =>
      {
        options.ViewLocationFormats.Add("/{0}.cshtml");
      });
      services.AddSingleton<CommonLocalizationService>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<ISeederToDatabase, SeederToDatabase>();
      services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
      });
      services.AddLocalization(options => options.ResourcesPath = "Resources");
      services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
      services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeederToDatabase seeder)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSession();
      app.UseCookiePolicy();

      app.UseRouting();
      seeder.HighlightDatabaseAsync().GetAwaiter();
      app.UseAuthentication();
      app.UseAuthorization();

#pragma warning disable CS8602 // Wy³uskanie odwo³ania, które mo¿e mieæ wartoœæ null.
      var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
#pragma warning restore CS8602 // Wy³uskanie odwo³ania, które mo¿e mieæ wartoœæ null.
      app.UseRequestLocalization(localizationOptions);
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
