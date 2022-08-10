using AutoMapper;
using System.Linq;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Api.DataModel.UsuarioDataModel;

namespace TemplateApi.Api.App_Start.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            #region InserirUsuarioCmd

            CreateMap<InserirUsuarioDataModel, InserirUsuarioCmd>()
                .ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Status, src.Status);
                    });
                });

            #endregion

            #region FiltrarUsuarioCmd

            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Usuario, src.Usuario);
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Status, src.Status);
                    });
                }).ForMember(cmd => cmd.Contexto, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Contexto, src.Contexto);
                    });
                });

            #endregion

            #region EditarUsuarioCmd

            CreateMap<EditarUsuarioDataModel, EditarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Usuario, src.Usuario);
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Status, src.Status);
                    });
                });

            #endregion

            #region ExcluirUsuarioCmd

            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Usuario, src.Usuario);
                    });
                });

            #endregion
        }
    }
}
