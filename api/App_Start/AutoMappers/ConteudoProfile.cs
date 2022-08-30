using AutoMapper;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Api.DataModels.ConteudoDataModel;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class ConteudoProfile : Profile
    {
        public ConteudoProfile()
        {
            #region InserirConteudoCmd

            CreateMap<InserirConteudoDataModel, InserirConteudoCmd>()
                .ForMember(cmd => cmd.Titulo, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Titulo);
                    });
                }).ForMember(cmd => cmd.Alias, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Alias);
                    });
                }).ForMember(cmd => cmd.Texto, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Texto);
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

            #region FiltrarConteudoCmd

            CreateMap<FiltrarConteudoDataModel, FiltrarConteudoCmd>()
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
                }).ForMember(cmd => cmd.Contexto, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Contexto, src.Contexto)
                            && src.PropriedadeRegistrada(x => x.Contexto);
                    });
                }).ForMember(cmd => cmd.Conteudo, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Conteudo, src.Conteudo)
                            && srcMember != null;
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

            #region EditarConteudoCmd

            CreateMap<EditarConteudoDataModel, EditarConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Conteudo, src.Conteudo)
                            && src.PropriedadeRegistrada(x => x.Conteudo);
                    });
                }).ForMember(cmd => cmd.Titulo, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Titulo);
                    });
                }).ForMember(cmd => cmd.Alias, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Alias);
                    });
                }).ForMember(cmd => cmd.Texto, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.Texto);
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

            #region ExcluirConteudoCmd

            CreateMap<ExcluirConteudoDataModel, ExcluirConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Conteudo, src.Conteudo)
                            && src.PropriedadeRegistrada(x => x.Conteudo);
                    });
                });

            #endregion
        }
    }
}
