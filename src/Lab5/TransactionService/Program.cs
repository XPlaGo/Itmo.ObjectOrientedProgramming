using System.Globalization;
using TransactionService.Application.Extensions;
using TransactionService.Extensions;
using TransactionService.Persistence.Extensions;
using TransactionService.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();

var ci = new CultureInfo("us-US");

builder.Services.AddPersistenceLayer(configuration =>
{
    configuration.Host = Environment.GetEnvironmentVariable("PGHOST") ?? "localhost";
    configuration.Port = int.Parse(Environment.GetEnvironmentVariable("PGPORT") ?? "5432", ci);
    configuration.Username = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
    configuration.Password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "postgres";
    configuration.Database = Environment.GetEnvironmentVariable("PGDBNAME") ?? "lab5";
    configuration.SslMode = "Prefer";
});
builder.Services.AddGrpc();
builder.Services.AddControllers();

builder.Services.AddPresentationLevel(builder.Configuration, builder.Environment);

WebApplication app = builder.Build();

app.UseRouting();

app.Services.CreateAsyncScope().UseInfrastructureDataAccess();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcTransactionService>();
app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();