using B3.API.Extensions;
using B3.API.Middlewares;
using B3.Application.Extensions;
using B3.Domain.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // <-- necessário para funcionar no Docker
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddAPIServices();
builder.Services.AddCorsConfig(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCorsConfig();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CDB API V1");
});
// 
app.UseDefaultFiles();     // permite index.html automático
app.UseStaticFiles();      // serve wwwroot

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Fallback para Angular
app.MapFallbackToFile("index.html");

app.Run();
public partial class Program { }
