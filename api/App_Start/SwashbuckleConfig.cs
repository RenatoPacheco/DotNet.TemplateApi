using System;
using System.IO;
using System.Text;
using BitHelp.Core.Type.pt_BR;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.App_Start.Swashbuckles;
using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;
using Microsoft.OpenApi.Any;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace TemplateApi.Api
{
    public static class SwashbuckleConfig
    {
        public static void Config(IServiceCollection services)
        {
            Sobre sobre = new Sobre();

            services.AddSwaggerGen(options =>
            {

                StringBuilder texto = new StringBuilder();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppSettings.Nome,
                    Version = "v1",
                    Description = $@"<p>Um projeto para montrar uma estrutura base de reursos e 
                        configuraçãções para montar uma API em .Net Core 6.</p> 
                        <p>Para mais informações consulte o projeto no <strong>Github</strong>, 
                        pelo link do website do swagger.</p>
                        <h3>Sobre a aplicação</h3>
                        <p>
                            <ul>
                                <li><strong>Nome</strong>: {sobre.Nome}</li>
                                <li><strong>Versao</strong>: {sobre.Versao}</li>
                                <li><strong>Ambiente</strong>: {sobre.Ambiente}</li>
                                <li><strong>Desenvolvimento</strong>: {sobre.EhDesenvolvimento}</li>
                            </ul>
                        </p>"
                    ,
                    Contact = new OpenApiContact()
                    {
                        Name = AppSettings.Autor.Nome,
                        Email = AppSettings.Autor.Email,
                        Url = new Uri(AppSettings.Autor.Url)
                    },
                });

                options.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                options.DocInclusionPredicate((name, api) => true);

                texto.Clear();
                texto.Append("Insira sua chave de acesso diretamente: <strong>{sua chave de acesso}</strong>");
                texto.Append($"<br>Chave-Publica: <strong>{AppSettings.ChavePublica}</strong>");
                texto.Append("<br><br><center><strong>!!! IMPORTANTE !!!</strong></center>");
                texto.Append("<br/>Esse é só um template, não deixe os valores aparecerem aqui nem use esses valores!<br>&nbsp;");

                options.AddSecurityDefinition("Chave pública", new OpenApiSecurityScheme
                {
                    Description = texto.ToString(),
                    Name = "Chave-Publica",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Chave pública"
                            }
                        },
                        new string[] {}
                    }
                });

                texto.Clear();
                texto.Append("Insira o token desta maneira: <strong>Bearer {seu token}</strong>");
                texto.Append($"<br>Authorization: <strong>Bearer {AppSettings.Autorizacao}</strong>");
                texto.Append("<br><br><center><strong>!!! IMPORTANTE !!!</strong></center>");
                texto.Append("<br/>Esse é só um template, não deixe os valores aparecerem aqui nem use esses valores!<br>&nbsp;");

                options.AddSecurityDefinition("Autorização", new OpenApiSecurityScheme
                {
                    Description = texto.ToString(),
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Autorização"
                            }
                        },
                        new string[] {}
                    }
                });

                options.SchemaFilter<FiltroSchemaFilter>();
                options.DocumentFilter<FiltroDocumentFilter>();
                options.ParameterFilter<FiltroParameterFilter>();
                options.OperationFilter<FiltroOperationFilter>();
                options.RequestBodyFilter<FiltroRequestBodyFilter>();

                // https://swagger.io/docs/specification/data-models/data-types/

                options.MapType<PhoneType>(
                    () => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "phone",
                        Example = new OpenApiString("(12) 93456-7890")
                    });
                options.MapType<PhoneType?>(
                    () => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "phone",
                        Nullable = true,
                        Example = new OpenApiString("(12) 93456-7890")
                    });

                options.MapType<IntInput>(
                    () => new OpenApiSchema
                    {
                        Type = "number",
                        Format = "int32",
                        Nullable = true
                    });

                options.MapType<LongInput>(
                    () => new OpenApiSchema
                    {
                        Type = "number",
                        Format = "int64",
                        Nullable = true
                    });

                options.MapType<DecimalInput>(
                    () => new OpenApiSchema
                    {
                        Type = "number",
                        Format = "decimal",
                        Nullable = true
                    });

                options.MapType<DoubleInput>(
                    () => new OpenApiSchema
                    {
                        Type = "number",
                        Format = "double",
                        Nullable = true
                    });

                options.MapType<FloatInput>(
                    () => new OpenApiSchema
                    {
                        Type = "number",
                        Format = "float",
                        Nullable = true
                    });

                options.MapType<DateTimeInput>(
                    () => new OpenApiSchema
                    {
                        Type = "strting",
                        Format = "date-time",
                        Nullable = true,
                        Example = new OpenApiString(DateTime.Now.ToString())
                    });

                options.MapType<TimeSpan>(
                    () => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "time",
                        Example = new OpenApiString(TimeSpan.FromSeconds(5346).ToString())
                    });
                options.MapType<TimeSpan?>(
                    () => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "time",
                        Nullable = true,
                        Example = new OpenApiString(TimeSpan.FromSeconds(5346).ToString())
                    });

                options.MapType<TimeSpanInput>(
                    () => new OpenApiSchema
                    {
                        Type = "strting",
                        Format = "time",
                        Nullable = true,
                        Example = new OpenApiString(TimeSpan.FromSeconds(5346).ToString())
                    });

                options.MapType<GuidInput>(
                    () => new OpenApiSchema
                    {
                        Type = "strting",
                        Format = "uuid",
                        Nullable = true,
                        Example = new OpenApiString(Guid.NewGuid().ToString())
                    });

                options.MapType<BoolInput>(
                    () => new OpenApiSchema
                    {
                        Type = "boolean",
                        Nullable = true
                    });

                options.MapTypeEnum<Status>();
                
                options.MapTypeEnum<ContextoCmd>();

                options.MapTypeEnum<TipoAvisos>();

                string pasta = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(pasta, "TemplateApi.Api.xml"));
                options.IncludeXmlComments(Path.Combine(pasta, "TemplateApi.xml"));
            });
        }

        public static void Config(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
