using Serilog;

namespace Svd.Backend.PostOffice.StartupConfiguration;

public static class GlobalErrorHandler
{
    public static void HandleErrors()
    {
        AppDomain.CurrentDomain.UnhandledException += GlobalUnhandledExceptionsHandler;
    }

    private static void GlobalUnhandledExceptionsHandler
        (object sender, UnhandledExceptionEventArgs eventArgs)
    {
        var exception = (Exception)eventArgs.ExceptionObject;
        if (eventArgs.IsTerminating)
        {
            Log.Fatal(exception, "Critical error occurred");
            Log.Fatal("Terminating the backend project");
        }
        else
            Log.Error(exception, "Error occurred");
    }
}
