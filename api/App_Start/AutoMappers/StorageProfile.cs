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
                .ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region FiltrarStorageCmd

            CreateMap<FiltrarStorageDataModel, FiltrarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Storage, src.Storage)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Contexto, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Contexto, src.Contexto)
                            && srcMember != null;
                    });
                });

            #endregion

            #region EditarStorageCmd

            CreateMap<EditarStorageDataModel, EditarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Storage, src.Storage)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                });

            #endregion

            #region ExcluirStorageCmd

            CreateMap<ExcluirStorageDataModel, ExcluirStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Storage, src.Storage)
                            && srcMember != null;
                    });
                });

            #endregion

            #region ObterStorageCmd

            CreateMap<ObterStorageDataModel, ObterStorageCmd>()
                .ForMember(cmd => cmd.Download, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Download, src.Download)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                });

            #endregion
        }
    }
}
