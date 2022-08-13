using Svd.Backend.PostOffice.Middleware;

namespace Svd.Backend.PostOffice.StartupConfiguration;

public static class ExceptionHandler
{
    public static IApplicationBuilder UseCustomExceptionHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
