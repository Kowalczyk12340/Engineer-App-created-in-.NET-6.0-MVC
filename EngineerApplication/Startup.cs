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

    // This method gets called by the runtime. Use this method to add Services to the container.
    public void ConfigureServices(IServiceCollection Services)
    {
      Services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
      });

      Services.AddDbContext<EngineerDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("Database")));
      Services.AddIdentity<IdentityUser, IdentityRole>()
          .AddEntityFrameworkStores<EngineerDbContext>()
          .AddDefaultTokenProviders();

      Services.Configure<RequestLocalizationOptions>(options =>
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

      Services.AddSingleton<IEmailSender, EmailSender>();
      Services.AddMvc().AddViewLocalization().AddRazorOptions(options =>
      {
        options.ViewLocationFormats.Add("/{0}.cshtml");
      });
      Services.AddSingleton<CommonLocalizationService>();
      Services.AddScoped<IUnitOfWork, UnitOfWork>();
      Services.AddScoped<ISeederToDatabase, SeederToDatabase>();
      Services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
      });
      Services.AddLocalization(options => options.ResourcesPath = "Resources");
      Services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
      Services.AddRazorPages();
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
      seeder.HighlightDatabase();
      app.UseAuthentication();
      app.UseAuthorization();

      var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
      app.UseRequestLocalization(localizationOptions);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{area=Employee}/{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
