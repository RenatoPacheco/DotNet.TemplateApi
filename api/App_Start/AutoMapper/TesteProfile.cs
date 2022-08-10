using AutoMapper;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Comandos.TesteCmds;
using TemplateApi.Api.DataModel.TesteDataModel;

namespace TemplateApi.Api.App_Start.AutoMapper
{
    public class TesteProfile : Profile
    {        public TesteProfile()
        {
            #region FormatosTesteCmd

            CreateMap<FormatosTesteDataModel, FormatosTesteCmd>()
                .ForMember(cmd => cmd.Int, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Int?.IsValid() ?? false;

                        if ((src.Int != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Int);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Long, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Long?.IsValid() ?? false;

                        if ((src.Long != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Long);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Decimal, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Decimal?.IsValid() ?? false;

                        if ((src.Decimal != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Decimal);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Double, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Double?.IsValid() ?? false;

                        if ((src.Double != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Double);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Enum, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Enum?.IsValid() ?? false;

                        if ((src.Enum != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Enum);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.DateTime, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.DateTime?.IsValid() ?? false;

                        if ((src.DateTime != null) && !ehValido)
                            dest.AddErrorNotification(x => x.DateTime);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.TimeSpan, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.TimeSpan?.IsValid() ?? false;

                        if ((src.TimeSpan != null) && !ehValido)
                            dest.AddErrorNotification(x => x.TimeSpan);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Guid, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Guid?.IsValid() ?? false;

                        if ((src.Bool != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Guid);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Bool, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Bool?.IsValid() ?? false;

                        if ((src.Bool != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Bool);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });
            #endregion
        }
    }
}
