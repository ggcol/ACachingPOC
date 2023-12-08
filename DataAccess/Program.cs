using FakeDataSource;
using FakeDataSource.DataSources.Fakes;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder
    .Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddSingleton<IDataSource, FakeDb>()
        .AddSingleton<IMemoryCache, MemoryCache>();

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