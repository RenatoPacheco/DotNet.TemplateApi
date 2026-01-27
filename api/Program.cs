using TemplateApi.Api;
using TemplateApi.Compartilhados.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AppSettingsConfig.Config(builder.Configuration);

// Configuração para MVC
builder.Services.AddControllersWithViews();
// ----------------------

builder.Services.AddHttpContextAccessor();

CorsConfig.Config(builder.Services);
IdCConfig.Config(builder.Services);

builder.Services.AddControllers(options => {
    // Aplicando filtros customizados
    FilterConfig.Config(options);
    // Aplicando binders customizados
    ModelBinderProviderConfig.Config(options);
}).ConfigureApiBehaviorOptions(options => {
    // Desabilitando o filtro que intecepta erros do ModelState
    options.SuppressModelStateInvalidFilter = true;
}).AddNewtonsoftJson(options => {
    ConfiguracaoJson.Escrita(options.SerializerSettings);
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
    app.UseDeveloperExceptionPage();
    SwashbuckleConfig.Config(app);
}

FileConfig.Config(app);

app.UseHttpsRedirection();

// Configuração para MVC
app.UseRouting();
MiddlewareConfig.Config(app);
// ----------------------

app.UseAuthorization();

app.MapControllers();

app.Run();

// Tive de adicionar essa linha para rodar o teste de integra��o
public partial class Program { }