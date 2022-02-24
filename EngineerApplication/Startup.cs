using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Initializer;
using Microsoft.AspNetCore.Identity.UI.Services;
using EngineerApplication.Helpers;

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

      Services.AddSingleton<IEmailSender, EmailSender>();

      Services.AddScoped<IUnitOfWork, UnitOfWork>();
      Services.AddScoped<ISeederToDatabase, SeederToDatabase>();
      Services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
      });

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
