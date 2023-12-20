using System.Globalization;
using CurrencyService.Application.Extensions;
using CurrencyService.Persistence.Extensions;
using CurrencyService.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
/*builder.Services.AddInfrastructureLevel(builder.Configuration);*/

var ci = new CultureInfo("us-US");

builder.Services.AddPersistenceLayer(configuration =>
{
    configuration.Host = Environment.GetEnvironmentVariable("PGHOST") ?? "localhost";
    configuration.Port = int.Parse(Environment.GetEnvironmentVariable("PGPORT") ?? "5432", ci);
    configuration.Username = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
    configuration.Password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "tAnk11xY";
    configuration.Database = Environment.GetEnvironmentVariable("PGDBNAME") ?? "lab5";
    configuration.SslMode = "Prefer";
});
builder.Services.AddGrpc();
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseRouting();

app.Services.CreateAsyncScope().UseInfrastructureDataAccess();

app.UseHttpsRedirection();

app.MapControllers();

app.MapGrpcService<ConversionService>();
app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();