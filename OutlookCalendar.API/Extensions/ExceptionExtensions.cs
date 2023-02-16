using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using OutlookCalendar.Domain.Core.Responses;
using System;
using System.Net;

namespace OutlookCalendar.API.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Guid guidLog = Guid.NewGuid();

                        await context.Response.WriteAsync(
                         new ResponseBindingModel<ErrorMessageBindingModel>
                         {
                             Succeeded = false,
                             Result = new ErrorMessageBindingModel { Code = "001", Message = $"No se ha podido procesar la solicitud , revise el id: {context.TraceIdentifier}" }
                         }.ToString());
                    }
                });
            });
        }
    }
}
