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
                .ForMember(cmd => cmd.Nome, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Nome);
                    });
                }).ForMember(cmd => cmd.Email, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Email);
                    });
                }).ForMember(cmd => cmd.Senha, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Senha);
                    });
                }).ForMember(cmd => cmd.Telefone, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Telefone);
                    });
                }).ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && src.PropriedadeRegistrada(x => x.Status);
                    });
                });

            #endregion

            #region FiltrarUsuarioCmd

            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>()
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
                }).ForMember(cmd => cmd.Usuario, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Usuario, src.Usuario)
                            && src.PropriedadeRegistrada(x => x.Usuario);
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

            #region EditarUsuarioCmd

            CreateMap<EditarUsuarioDataModel, EditarUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Usuario, src.Usuario)
                            && src.PropriedadeRegistrada(x => x.Usuario);
                    });
                }).ForMember(cmd => cmd.Nome, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Nome);
                    });
                }).ForMember(cmd => cmd.Email, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Email);
                    });
                }).ForMember(cmd => cmd.Senha, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Senha);
                    });
                }).ForMember(cmd => cmd.Telefone, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Telefone);
                    });
                }).ForMember(cmd => cmd.Status, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Status, src.Status)
                            && src.PropriedadeRegistrada(x => x.Status);
                    });
                });

            #endregion

            #region ExcluirUsuarioCmd

            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>()
                .ForMember(cmd => cmd.Usuario, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Usuario, src.Usuario)
                            && src.PropriedadeRegistrada(x => x.Usuario);
                    });
                });

            #endregion
        }
    }
}
