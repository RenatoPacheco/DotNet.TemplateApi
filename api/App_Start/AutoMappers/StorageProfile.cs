using AutoMapper;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Api.DataModels.StorageDataModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TemplateApi.Api.ValuesObject;
using System.Linq;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            #region InserirStorageCmd

            CreateMap<InserirStorageDataModel, InserirStorageCmd>()
                .ForMember(cmd => cmd.Arquivo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Arquivo));
                    opts.MapFrom(src => src.Arquivo.Select(x => new StoragePrivado(x)));
                });

            #endregion

            #region FiltrarStorageCmd

            CreateMap<FiltrarStorageDataModel, FiltrarStorageCmd>()
                .ForMember(cmd => cmd.Texto, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Texto));
                })
                .ForMember(cmd => cmd.Pagina, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Pagina)
                            && dest.InputTypeEhValido(x => x.Pagina, src.Pagina));
                })
                .ForMember(cmd => cmd.Maximo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Maximo)
                            && dest.InputTypeEhValido(x => x.Maximo, src.Maximo));
                })
                .ForMember(cmd => cmd.CalcularPaginacao, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.CalcularPaginacao)
                            && dest.InputTypeEhValido(x => x.CalcularPaginacao, src.CalcularPaginacao));
                })
                .ForMember(cmd => cmd.Storage, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Storage)
                            && dest.InputTypeEhValido(x => x.Storage, src.Storage));
                })
                .ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Status)
                            && dest.InputTypeEhValido(x => x.Status, src.Status));
                })
                .ForMember(cmd => cmd.Contexto, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Contexto)
                            && dest.InputTypeEhValido(x => x.Contexto, src.Contexto));
                });

            #endregion

            #region EditarStorageCmd

            CreateMap<EditarStorageDataModel, EditarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Storage)
                            && dest.InputTypeEhValido(x => x.Storage, src.Storage));
                })
                .ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Status)
                            && dest.InputTypeEhValido(x => x.Status, src.Status));
                });

            #endregion

            #region ExcluirStorageCmd

            CreateMap<ExcluirStorageDataModel, ExcluirStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Storage)
                            && dest.InputTypeEhValido(x => x.Storage, src.Storage));
                })
                .ForMember(cmd => cmd.Alias, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Alias));
                });

            #endregion

            #region ObterStorageCmd

            CreateMap<ObterStorageDataModel, ObterStorageCmd>()
                .ForMember(cmd => cmd.Texto, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Texto));
                })
                .ForMember(cmd => cmd.Pagina, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Pagina)
                            && dest.InputTypeEhValido(x => x.Pagina, src.Pagina));
                })
                .ForMember(cmd => cmd.Maximo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Maximo)
                            && dest.InputTypeEhValido(x => x.Maximo, src.Maximo));
                })
                .ForMember(cmd => cmd.CalcularPaginacao, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.CalcularPaginacao)
                            && dest.InputTypeEhValido(x => x.CalcularPaginacao, src.CalcularPaginacao));
                })
                .ForMember(cmd => cmd.Alias, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Alias));
                })
                .ForMember(cmd => cmd.Download, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Download)
                            && dest.InputTypeEhValido(x => x.Download, src.Download));
                })
                .ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Status)
                            && dest.InputTypeEhValido(x => x.Status, src.Status));
                });

            #endregion
        }
    }
}
