using System.Globalization;
using BankAccountService.Application.Extensions;
using BankAccountService.Extensions;
using BankAccountService.Infrastructure.Extensions;
using BankAccountService.Persistence.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLevel(Environment.GetEnvironmentVariable("GRPCADDRESS") ?? "http://localhost:7002");

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

builder.Services.ConfigureJwt(builder.Configuration, builder.Environment);

WebApplication app = builder.Build();

app.UseRouting();

app.Services.CreateAsyncScope().UseInfrastructureDataAccess();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();