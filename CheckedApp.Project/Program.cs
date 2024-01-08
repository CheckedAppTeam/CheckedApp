
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.LOGIC.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IItemListService, ItemListService>();
builder.Services.AddScoped<IItemListRepository, ItemListRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserItemContext>(
    option => option.UseNpgsql(builder.Configuration["CheckedAppDbConnection"])
    );

//configuring serilog extension
//tu trzeba zrobic warunek dla iMaca
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File($"C:/Users/micha/source/repos/CheckedApp/CheckedApp.Project/Controllers/myapp.log", rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

