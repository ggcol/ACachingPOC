using DataAccess.Utils;
using FakeDataSource;
using FakeDataSource.DataSources;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IDataSource, FakeSlowDb>()
    .AddSingleton<IMemoryCache, MemoryCache>()
    .AddScoped<IControllerMonitor, ControllerMonitor>();

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