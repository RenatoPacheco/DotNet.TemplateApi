using TemplateApi.Api;
using TemplateApi.Compartilhado.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AppSettingsConfig.Config(builder.Configuration);

builder.Services.AddHttpContextAccessor();

CorsConfig.Config(builder.Services);
IdCConfig.Config(builder.Services);

builder.Services.AddControllers(options => {
    // Aplicando filtrdo customizados
    FilterConfig.Config(options);
    // Aplicando binders customizados
    ModelBinderProviderConfig.Config(options);
}).ConfigureApiBehaviorOptions(options => {
    // Desabilitando o filtro que intecepta erros do ModelState
    options.SuppressModelStateInvalidFilter = true;
}).AddJsonOptions(options => {
    ContratoJson.Configurar(options.JsonSerializerOptions);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

SwashbuckleConfig.Config(builder.Services);
AutoMapperConfig.Config(builder.Services);

var app = builder.Build();

AppSettingsConfig.Config(app.Environment);
CultureInfoConfig.Config(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    SwashbuckleConfig.Config(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Tive de adicionar essa linha para rodar o teste de integração
public partial class Program { }