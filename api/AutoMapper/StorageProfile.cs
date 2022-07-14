using AutoMapper;
using System.Linq;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Site.DataModel.StorageDataModel;

namespace DotNetCore.API.Template.Site.AutoMapper
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            CreateMap<InserirStorageDataModel, InserirStorageCmd>()
                .ForMember(m => m.Arquivo, opts => {
                    opts.MapFrom(src => (src.Arquivo == null) ? null : src.Arquivo.Select(x => new ArquivoUpload(x)).ToList());
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<FiltrarStorageDataModel, FiltrarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.MapFrom(src => (src.Storage == null) ? null : src.Storage.Select(x => (long)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Storage.Any() && !src.Storage.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Storage);

                        return ehValido;
                    });
                })
                .ForMember(cmd => cmd.Status, opts => {
                    opts.MapFrom(src => (src.Status == null) ? null : src.Status.Select(x => (Status)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status.Any() && !src.Status.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<EditarStorageDataModel, EditarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.MapFrom(src => (src.Storage == null) ? null : (long?)src.Storage);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Storage?.IsValid() ?? false;

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Storage);

                        return ehValido;
                    });
                })
                .ForMember(cmd => cmd.Status, opts => {
                    opts.MapFrom(src => (src.Status == null) ? null : (Status?)src.Status);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status?.IsValid() ?? false;

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<ExcluirStorageDataModel, ExcluirStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.MapFrom(src => (src.Storage == null) ? null : src.Storage.Select(x => (long)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Storage.Any() && !src.Storage.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Storage);

                        return ehValido;
                    });
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<ObterStorageDataModel, ObterStorageCmd>()
                .ForMember(cmd => cmd.Download, opts => {
                    opts.MapFrom(src => (src.Download == null) ? null : (bool?)src.Download);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Download?.IsValid() ?? false;

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Download);

                        return ehValido;
                    });
                })
                .ForMember(cmd => cmd.Status, opts => {
                    opts.MapFrom(src => (src.Status == null) ? null : src.Status.Select(x => (Status)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status.Any() && !src.Status.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
        }
    }
}
