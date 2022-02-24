[assembly: HostingStartup(typeof(EngineerApplication.Areas.Identity.IdentityHostingStartup))]
namespace EngineerApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}