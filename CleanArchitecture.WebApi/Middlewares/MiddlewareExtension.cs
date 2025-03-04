namespace CleanArchitecture.WebApi.Middlewares;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseErrorHandlingMiddlewareExtensions(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionErrorMiddleware>();
        return app;
    }
}