using System.Globalization;
using IdentityService.Application.Extensions;
using IdentityService.Infrastructure.Extensions;
using IdentityService.Persistence.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLevel(builder.Configuration);

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Services.CreateAsyncScope().UseInfrastructureDataAccess();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();