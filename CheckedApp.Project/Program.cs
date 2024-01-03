
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.LOGIC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



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

