using CheckedAppProject.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

StartupConfiguration.ConfigureLogging(builder);
StartupConfiguration.ConfigureServices(builder);

var app = builder.Build();

StartupConfiguration.ConfigureApp(app);

app.Run();
