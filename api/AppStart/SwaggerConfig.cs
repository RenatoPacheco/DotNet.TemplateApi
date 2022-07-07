using System;
using System.IO;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using DotNetCore.API.Template.Site.Swashbuckle;
using Microsoft.Extensions.DependencyInjection;
using BitHelp.Core.Type.pt_BR;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using System.Linq;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using DotNetCore.API.Template.Site.Extensions;

namespace DotNetCore.API.Template.Site
{
    public static class SwaggerConfig
    {
        public static void Config(IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                
                StringBuilder texto = new StringBuilder();

                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = AppSettings.Nome, 
                    Version = "v1" 
                });

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

                options.MapType<PhoneType>(
                    () => new OpenApiSchema { Type = "string" });
                options.MapType<PhoneType?>(
                    () => new OpenApiSchema { Type = "string" });

                options.MapType<Status>(
                    () => new OpenApiSchema {
                        Type = "string",
                        Enum = options.IOpenApiAnyByEnum<Status>()
                    });
                options.MapType<Status?>(
                    () => new OpenApiSchema {
                        Type = "string",
                        Enum = options.IOpenApiAnyByEnum<Status>()
                    });

                string pasta = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(pasta, "DotNetCore.API.Template.Site.xml"));
                options.IncludeXmlComments(Path.Combine(pasta, "DotNetCore.API.Template.xml"));
            });
        }

        public static void Config(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppSettings.Nome} v1");
                options.RoutePrefix = "swagger";
            });
        }
    }
}
