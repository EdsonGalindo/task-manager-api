using TaskManager.WebApp.API.Configurations;

#region Services Configurations
var builder = WebApplication.CreateBuilder(args);

#region Application configurations
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetSwaggerConfig();
builder.Services.ResolveDependencies();
builder.Services.SetWebApiConfig();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddApplicationLogging();
#endregion

#region Task Manager Configurations
builder.Services.AddTaskAppSettings(builder.Configuration);
builder.Services.AddTasksInMemoryDbContext(builder.Configuration);
#endregion
#endregion

#region Application Configurations
var app = builder.Build();

app.UseSwaggerConfig();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
