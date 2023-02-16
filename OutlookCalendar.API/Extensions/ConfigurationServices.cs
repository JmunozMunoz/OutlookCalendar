using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OutlookCalendar.Domain.Core.Repositories;
using OutlookCalendar.Infaestructure.Repositories;

namespace OutlookCalendar.API.Extensions
{
    public static class ConfigurationServices
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }

        public static IServiceCollection AddConfigureController(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        public static void AddConfigureCors(this IServiceCollection services)
        {
            services.AddCors(setup =>
            {
                setup.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void AddConfigureSerializationJson(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public static void AddConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo() { Title = "OutlookCalendar.API", Version = "V1" });
            });
        }

        public static IServiceCollection AddTransients(this IServiceCollection services)
        {
            services.AddTransient<IOutlookCalendarRequestRepository, OutlookCalendarRepository>();
        
            return services;
        }

        public static void UseConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "OutlookCalendar.API API");
                }
            );
        }

    }
}
