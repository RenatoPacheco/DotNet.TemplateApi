using AutoMapper;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Api.DataModels.UsuarioDataModel;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            #region InserirUsuarioCmd

            CreateMap<InserirUsuarioDataModel, InserirUsuarioCmd>()
                .ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                });

            #endregion

            #region FiltrarUsuarioCmd

            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Usuario, src.Usuario)
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

            #region EditarUsuarioCmd

            CreateMap<EditarUsuarioDataModel, EditarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Usuario, src.Usuario)
                            && srcMember != null;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && srcMember != null;
                    });
                });

            #endregion

            #region ExcluirUsuarioCmd

            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Usuario, src.Usuario)
                            && srcMember != null;
                    });
                });

            #endregion
        }
    }
}
