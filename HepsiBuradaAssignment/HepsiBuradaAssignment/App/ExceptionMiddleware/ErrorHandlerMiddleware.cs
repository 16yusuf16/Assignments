using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using HepsiBuradaAssignment.Application.Response;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace HepsiBuradaAssignment.Api.App.ExceptionMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var statusCode = httpContext.Response.StatusCode;


            string message = "Internal Server Error";
            if (ex.GetType() == typeof(ValidationException))
            {
                ValidationException e = (ValidationException)ex;
                message = e.Errors.Any() ? string.Empty : message;
                foreach (var item in e.Errors)
                {
                    message += item.ErrorMessage + " \r\n ";
                }
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            var result = Response<string>.Fail(message);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result, serializerSettings));
        }
    }
}
