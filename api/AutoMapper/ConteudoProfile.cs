using AutoMapper;
using System.Linq;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds;
using DotNetCore.API.Template.Site.DataModel.ConteudoDataModel;

namespace DotNetCore.API.Template.Site.AutoMapper
{
    public class ConteudoProfile : Profile
    {
        public ConteudoProfile()
        {
            #region InserirConteudoCmd

            CreateMap<InserirConteudoDataModel, InserirConteudoCmd>()
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

            #region FiltrarConteudoCmd

            CreateMap<FiltrarConteudoDataModel, FiltrarConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts => {
                    opts.MapFrom(src => (src.Conteudo == null) ? null : src.Conteudo.Select(x => (int)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Conteudo?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Conteudo != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Conteudo);

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
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region EditarConteudoCmd

            CreateMap<EditarConteudoDataModel, EditarConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts => {
                    opts.MapFrom(src => (src.Conteudo == null) ? null : (int?)src.Conteudo);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Conteudo?.IsValid() ?? false;

                        if ((src.Conteudo != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Conteudo);

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

            #region ExcluirConteudoCmd

            CreateMap<ExcluirConteudoDataModel, ExcluirConteudoCmd>()
                .ForMember(cmd => cmd.Conteudo, opts => {
                    opts.MapFrom(src => (src.Conteudo == null) ? null : src.Conteudo.Select(x => (int)x).ToList());
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Conteudo?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Conteudo != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Conteudo);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion
        }
    }
}
