using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;
using System.Net;

namespace CleanArchitecture.WebApi.Middlewares;

public sealed class ExceptionErrorMiddleware(AppDbContext context) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await LogExceptionToDatabaseAsync(e, context.Request);
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (e.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ((ValidationException)e).Errors.Select(s => s.PropertyName)
            }.ToString());
        }

        return context.Response.WriteAsync(new ErrorResult
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = e.Message
        }.ToString());
    }

    private async Task LogExceptionToDatabaseAsync(Exception e, HttpRequest request)
    {
        ErrorLog errorLog = new()
        {
            ErrorMessage = e.Message,
            StackTrace = e.StackTrace,
            RequestMethod = request.Method,
            RequestPath = request.Path,
            Timestamp = DateTime.Now
        };

        await context.Set<ErrorLog>().AddAsync(errorLog);
        await context.SaveChangesAsync();
    }
}