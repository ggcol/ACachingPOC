using FakeDataSource;
using FakeDataSource.DataSources.Fakes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder
    .Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddSingleton<IDataSource, FakeDb>();

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