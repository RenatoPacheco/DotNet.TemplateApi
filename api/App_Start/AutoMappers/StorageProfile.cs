using AutoMapper;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Api.DataModels.StorageDataModel;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            #region InserirStorageCmd

            CreateMap<InserirStorageDataModel, InserirStorageCmd>()
                .ForAllMembers(opts =>
                {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region FiltrarStorageCmd

            CreateMap<FiltrarStorageDataModel, FiltrarStorageCmd>()
                .ForMember(cmd => cmd.Texto, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Texto));
                }).ForMember(cmd => cmd.Pagina, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Pagina, src.Pagina)
                            && src.PropriedadeRegistrada(x => x.Pagina);
                    });
                }).ForMember(cmd => cmd.Maximo, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Maximo, src.Maximo)
                            && src.PropriedadeRegistrada(x => x.Maximo);
                    });
                }).ForMember(cmd => cmd.CalcularPaginacao, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.CalcularPaginacao, src.CalcularPaginacao)
                            && src.PropriedadeRegistrada(x => x.CalcularPaginacao);
                    });
                }).ForMember(cmd => cmd.Storage, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Storage, src.Storage)
                            && src.PropriedadeRegistrada(x => x.Storage);
                    });
                }).ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && src.PropriedadeRegistrada(x => x.Status);
                    });
                }).ForMember(cmd => cmd.Contexto, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Contexto, src.Contexto)
                            && src.PropriedadeRegistrada(x => x.Contexto);
                    });
                });

            #endregion

            #region EditarStorageCmd

            CreateMap<EditarStorageDataModel, EditarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Storage, src.Storage)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                });

            #endregion

            #region ExcluirStorageCmd

            CreateMap<ExcluirStorageDataModel, ExcluirStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Storage, src.Storage)
                            && srcMember != null;
                    });
                });

            #endregion

            #region ObterStorageCmd

            CreateMap<ObterStorageDataModel, ObterStorageCmd>()
                .ForMember(cmd => cmd.Download, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Download, src.Download)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                });

            #endregion
        }
    }
}
