using CheckedAppProject.API.StartConfiguration;

var builder = WebApplication.CreateBuilder(args);

LoggingConfiguration.ConfigureLogging(builder);
StartupConfiguration.ConfigureServices(builder);

var app = builder.Build();

StartupConfiguration.ConfigureApp(app);

await ExtensionIdentityService.InitializeDatabaseAsync(app);

app.Run();