using Serilog;

namespace CheckedAppProject.API.StartConfiguration
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogging(WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
    }
}
