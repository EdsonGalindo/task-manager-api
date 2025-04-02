using TaskManager.WebApp.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetSwaggerConfig();

#region Task Manager Configurations
builder.Services.SetWebApiConfig();
builder.Services.ResolveDependencies();
builder.Services.AddTaskAppSettings(builder.Configuration);
builder.Services.AddTasksInMemoryDbContext(builder.Configuration);
builder.Services.AddAutoMapperProfiles();
#endregion

var app = builder.Build();

app.UseSwaggerConfig();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
