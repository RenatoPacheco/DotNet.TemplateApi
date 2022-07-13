using AutoMapper;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Site.DataModel.UsuarioDataModel;
using System.Linq;

namespace DotNetCore.API.Template.Site.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<InserirUsuarioDataModel, InserirUsuarioCmd>()
                .ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status?.IsValid() ?? false;

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                    opts.MapFrom(src => (Status)src.Status);
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Usuario.Any() && !src.Usuario.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Usuario);

                        return ehValido;
                    });
                    opts.MapFrom(src => src.Usuario.Select(x => (int)x));
                })
                .ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status.Any() && !src.Status.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                    opts.MapFrom(src => src.Status.Select(x => (Status)x));
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<EditarUsuarioDataModel, EditarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Usuario?.IsValid() ?? false;

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Usuario);

                        return ehValido;
                    });
                    opts.MapFrom(src => (int)src.Usuario);
                })
                .ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status?.IsValid() ?? false;

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                    opts.MapFrom(src => (Status)src.Status);
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Usuario.Any() && !src.Usuario.Any(x => !x.IsValid());

                        if (!ehValido)
                            dest.AddErrorNotification(x => x.Usuario);

                        return ehValido;
                    });
                    opts.MapFrom(src => src.Usuario.Select(x => (int)x));
                })
                .ForAllMembers(opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
        }
    }
}
