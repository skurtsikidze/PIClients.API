using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PIClients.API.ActionFilters;
using PIClients.API.Helpers;
using PIClients.API.Infrastructure;
using PIClients.API.Models;
using PIClients.API.Repositories;
using PIClients.API.Services;
using System.Collections.Generic;
using System.Globalization;

namespace PIClients.API
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
      var connection = Configuration.GetConnectionString("ClientsDatabase");
      services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
      services.AddDbContextPool<ClientsContext>(options => options.UseSqlServer(connection));
      services.AddScoped<ValidationActionFilter<ClientsContext>>();
      services.AddScoped<IClientRepository, ClientRepository>();
      services.AddTransient<IUnitOfWork, UnitOfWork>();

      services.AddLocalization(o =>
      {
        o.ResourcesPath = "Resources";
      });

      services.Configure<RequestLocalizationOptions>(
      opts =>
      {
        var supportedCultures = new List<CultureInfo>
          {
                  new CultureInfo("en-US"),
                  new CultureInfo("en"),
                  new CultureInfo("ka-GE"),
                  new CultureInfo("ka"),
          };

        opts.DefaultRequestCulture = new RequestCulture("en-US");
        opts.SupportedCultures = supportedCultures;
        opts.SupportedUICultures = supportedCultures;
      });

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseStaticFiles();

      app.UseLogging();
      
      app.UseHttpsRedirection();

      var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
      app.UseRequestLocalization(options.Value);

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
      
    }
  }
}
