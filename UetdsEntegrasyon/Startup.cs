using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace UetdsEntegrasyon
{
   public class Startup
   {
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddMemoryCache();
         services.AddControllers();
         services.AddCors();

         services.AddSwaggerGen(setup =>
         {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
               Version = "v1",
               Title = "UETDS API",
               Description = "UETDS Entegrasyonu",
               License = new OpenApiLicense
               {
                  Name = "Webnolojik.com",
               }
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            setup.IncludeXmlComments(xmlPath);
         });
      }
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
         app.UseRouting();
         app.UseAuthentication();
         app.UseAuthorization();
         app.UseSwagger();
         app.UseSwaggerUI(c =>
         {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Webnolojik UETDS API V1");
            c.RoutePrefix = string.Empty;
         });
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
