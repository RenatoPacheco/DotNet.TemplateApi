using System;
using System.IO;
using System.Text;
using BitHelp.Core.Type.pt_BR;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.Settings.Swashbuckle;
using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Api
{
    public static class SwaggerConfig
    {
        public static void Config(IServiceCollection services)
        {
            Sobre sobre = new Sobre();

            services.AddSwaggerGen(options => {
                
                StringBuilder texto = new StringBuilder();

                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = AppSettings.Nome, 
                    Version = "v1",
                    Description = $@"<p>Um projeto para montrar uma estrutura base de reursos e 
                        configuraçãções para montar uma API em .Net Core 3.1.</p> 
                        <p>Para mais informações consulte o projeto no <strong>Github</strong>, 
                        pelo lik do website do swagger.</p>
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
                    Contact = new OpenApiContact() { 
                        Name = AppSettings.Autor.Nome, 
                        Email = AppSettings.Autor.Email,
                        Url = new Uri(AppSettings.Autor.Url)
                    },
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
                    () => options.SchemaBasic("string", "(00) 00000-0000"));
                options.MapType<PhoneType?>(
                    () => options.SchemaBasic("string", "(00) 00000-0000"));

                options.MapType<IntInput>(
                    () => options.SchemaBasic("number", 0));
                options.MapType<IntInput?>(
                    () => options.SchemaBasic("number", 0));

                options.MapType<LongInput>(
                    () => options.SchemaBasic("number", 0));
                options.MapType<LongInput?>(
                    () => options.SchemaBasic("number", 0));

                options.MapType<GuidInput>(
                    () => options.SchemaBasic("string", Guid.NewGuid()));
                options.MapType<GuidInput?>(
                    () => options.SchemaBasic("string", Guid.NewGuid()));

                options.MapType<BoolInput>(
                    () => options.SchemaBasic("boolean", false));
                options.MapType<BoolInput?>(
                    () => options.SchemaBasic("boolean", false));

                options.MapType<Status>(
                    () => options.SchemaEnum<Status>("string"));
                options.MapType<Status?>(
                    () => options.SchemaEnum<Status>("string"));

                options.MapType<EnumInput<Status>>(
                    () => options.SchemaEnum<Status>("string"));
                options.MapType<EnumInput<Status>?>(
                    () => options.SchemaEnum<Status>("string"));

                options.MapType<EnumInput<ContextoCmd>>(
                    () => options.SchemaEnum<ContextoCmd>("string"));
                options.MapType<EnumInput<ContextoCmd>?>(
                    () => options.SchemaEnum<ContextoCmd>("string"));

                options.MapType<TipoNoificacaoAvisos>(
                    () => options.SchemaEnum<TipoNoificacaoAvisos>("string"));
                options.MapType<TipoNoificacaoAvisos?>(
                    () => options.SchemaEnum<TipoNoificacaoAvisos>("string"));

                string pasta = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(pasta, "TemplateApi.Api.xml"));
                options.IncludeXmlComments(Path.Combine(pasta, "TemplateApi.xml"));
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
