using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Cms.Infrastructure.Configurations;
using Cms.Application.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cms.Domain.Entities;
using Microsoft.Extensions.FileProviders;
using Cms.Web;


try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddCmsDbContext(builder.Configuration)
      .AddCRMServers()
      .AddMediator();

    builder.Services.AddDefaultIdentity<User>()
      .AddEntityFrameworkStores<CmsDbContext>();

    builder.Services.AddAuthorization();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    });


  builder.Services.AddControllers();

  var app = builder.Build();

   
  // Configure the HTTP request pipeline.
  if (!app.Environment.IsDevelopment())
  {
      app.UseExceptionHandler("/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
  }


  app.UseHttpsRedirection();

  app.UseStaticFiles();



    var path = Path.Combine(Directory.GetCurrentDirectory(), "Content");
    if (!Directory.Exists(path))
    {
        Directory.CreateDirectory(path);
    }
    app.Seed();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(path),
        RequestPath = "/Content"
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
    app.UseRouting();

  app.UseEndpoints(endpoint => {
    endpoint.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );
  });

  app.Run();
} 
catch (Exception ex)
{
    Console.WriteLine(ex.Message, ex);
    Console.WriteLine(ex.Message, ex);
}

