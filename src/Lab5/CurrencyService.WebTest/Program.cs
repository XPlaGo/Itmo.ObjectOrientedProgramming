using CurrencyService.Application.Extensions;
using CurrencyService.Persistence.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
/*builder.Services.AddInfrastructureLevel(builder.Configuration);*/
builder.Services.AddPersistenceLayer(configuration =>
{
    configuration.Host = "localhost";
    configuration.Port = 5432;
    configuration.Username = "postgres";
    configuration.Password = "tAnk11xY";
    configuration.Database = "lab5";
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