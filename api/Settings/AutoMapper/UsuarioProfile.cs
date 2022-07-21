using AutoMapper;
using System.Linq;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Api.DataModel.UsuarioDataModel;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Api.Settings.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            #region InserirUsuarioCmd

            CreateMap<InserirUsuarioDataModel, InserirUsuarioCmd>()
                .ForMember(cmd => cmd.Status, opts => {
                    opts.MapFrom(src => (src.Status == null) ? null : (Status?)src.Status);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status?.IsValid() ?? false;

                        if ((src.Status != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region FiltrarUsuarioCmd

            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.MapFrom(src => (src.Usuario == null) ? null : src.Usuario.Select(x => (int)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Usuario?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Usuario != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Usuario);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.MapFrom(src => (src.Status == null) ? null : src.Status.Select(x => (Status)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Status?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Status != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Contexto, opts => {
                    opts.MapFrom(src => (ContextoCmd)src.Contexto);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Contexto.IsValid();

                        if ((src.Contexto != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Contexto);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region EditarUsuarioCmd

            CreateMap<EditarUsuarioDataModel, EditarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.MapFrom(src => (src.Usuario == null) ? null : (int?)src.Usuario);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Usuario?.IsValid() ?? false;

                        if ((src.Usuario != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Usuario);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.MapFrom(src => (src.Status == null) ? null : (Status?)src.Status);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status?.IsValid() ?? false;

                        if (!(src.Status is null) && !ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region ExcluirUsuarioCmd

            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.MapFrom(src => (src.Usuario == null) ? null : src.Usuario.Select(x => (int)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Usuario?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Usuario != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Usuario);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion
        }
    }
}
