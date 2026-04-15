using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseInMemoryDatabase("InMem"));

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); ;

var app = builder.Build();



app.UseHttpsRedirection();
app.MapControllers();

PrepDB.PrepPopulation(app);


app.Run();


