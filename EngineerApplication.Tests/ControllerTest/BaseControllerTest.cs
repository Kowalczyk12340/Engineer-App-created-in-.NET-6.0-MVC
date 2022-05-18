using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace EngineerApplication.Tests.ControllerTest
{
  public abstract class BaseControllerTest
  {
    private readonly IHostBuilder _hostBuilder;
    protected IHost Application;
    public HttpClient Client;

    protected BaseControllerTest()
    {
      _hostBuilder = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(config =>
        {
          config.AddInMemoryCollection(new Dictionary<string, string>() { ["ConnectionStrings:Database"] = @"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True" });
          config.AddEnvironmentVariables();
        })
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Program>(); })
        .ConfigureWebHost(webHost => { webHost.UseIISIntegration(); });
    }


    [OneTimeSetUp]
    public void StartApplication()
    {
      var webApplicationFactory = new WebApplicationFactory<Program>();
      TestServer testServer = webApplicationFactory.Server;
      Client = webApplicationFactory.WithWebHostBuilder(builder
          => builder.ConfigureTestServices(services =>
          {
            services.AddMvc(opt => opt.Filters.Add(new AllowAnonymousFilter()));
          })).CreateClient();
    }

    [OneTimeTearDown]
    public void StopApplication()
    {
      Application.Dispose();
    }
  }
}

