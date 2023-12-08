using DataAccess.Utils;
using FakeDataSource;
using FakeDataSource.DataSources;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IDataSource, SharedWithOtherSystemsDb>()
    // .AddSingleton<IMemoryCache, MemoryCache>()
    .AddScoped<IControllerMonitor, ControllerMonitor>()
    .AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();