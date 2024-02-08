using CheckedAppProject.API.StartConfiguration;

var builder = WebApplication.CreateBuilder(args);

LoggingConfiguration.ConfigureLogging(builder);
StartupConfiguration.ConfigureServices(builder);

var app = builder.Build();
// New Comment to delete

StartupConfiguration.ConfigureApp(app);

app.Run();