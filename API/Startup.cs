using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
  public class Startup
  {
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
    
      services.AddDbContext<StoreContext>(x =>
      x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

      services.AddApplicationServices();
      services.AddSwaggerDocumentation();

      services.AddAutoMapper(typeof(MappingProfiles));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware<ExceptionMiddleware>();
      // So in the even that the request comes into our API server
      // but we don't have an end point this will redirect the call
      app.UseStatusCodePagesWithReExecute("/erros/{0}");

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseStaticFiles();

      app.UseAuthorization();

      app.UseSwaggerDocumentation();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
