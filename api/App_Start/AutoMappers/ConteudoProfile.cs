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
                .ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Status, src.Status);
                    });
                });

            #endregion

            #region FiltrarConteudoCmd

            CreateMap<FiltrarConteudoDataModel, FiltrarConteudoCmd>()
                .ForMember(cmd => cmd.Texto, opts => {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                }).ForMember(cmd => cmd.Contexto, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Contexto, src.Contexto);
                    });
                }).ForMember(cmd => cmd.Conteudo, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Conteudo, src.Conteudo);
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Status, src.Status);
                    });
                });

            #endregion

            #region EditarConteudoCmd

            CreateMap<EditarConteudoDataModel, EditarConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return dest.InputTypeEhValido(x => x.Conteudo, src.Conteudo);
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Status, src.Status);
                    });
                });

            #endregion

            #region ExcluirConteudoCmd

            CreateMap<ExcluirConteudoDataModel, ExcluirConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        return srcMember != null
                            && dest.InputTypeEhValido(x => x.Conteudo, src.Conteudo);
                    });
                });

            #endregion
        }
    }
}
