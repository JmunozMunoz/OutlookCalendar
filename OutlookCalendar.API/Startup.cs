using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OutlookCalendar.API.Configuration;
using OutlookCalendar.API.Extensions;
using OutlookCalendar.Application.OutlookCalendar.Handlers;
using OutlookCalendar.Domain.Core.Repositories;
using System.Reflection;

namespace OutlookCalendar.API
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
            services.AddConfigureController();

            services.AddConfigureCors();

            services.AddConfigureSerializationJson();

            services.AddConfigureSwagger();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(GetCalendarsHandlers).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteEventHandlers).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateEventHnadlers).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateEventHnadlers).GetTypeInfo().Assembly);
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddConfigureServiceLogger();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.AddSingleton<IOutlookCalendarConfiguration, OutlookCalendarConfiguration>();
            services.BuildServiceProvider().GetService<IOutlookCalendarConfiguration>();

            services.AddHttpContextAccessor();

            services.AddTransients();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OutlookCalendar.API API"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }       
    }
}
