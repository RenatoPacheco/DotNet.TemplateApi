using AutoMapper;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Comandos.TesteCmds;
using TemplateApi.Api.DataModels.TesteDataModel;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class TesteProfile : Profile
    {
        public TesteProfile()
        {
            #region FormatosTesteCmd

            CreateMap<FormatosTesteDataModel, FormatosTesteCmd>()
                .ForMember(cmd => cmd.String, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return src.PropriedadeRegistrada(x => x.String);
                    });
                }).ForMember(cmd => cmd.Int, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Int, src.Int)
                            && src.PropriedadeRegistrada(x => x.Int);
                    });
                }).ForMember(cmd => cmd.Long, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Long, src.Long)
                            && src.PropriedadeRegistrada(x => x.Long);
                    });
                }).ForMember(cmd => cmd.Decimal, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Decimal, src.Decimal)
                            && src.PropriedadeRegistrada(x => x.Decimal);
                    });
                }).ForMember(cmd => cmd.Double, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Double, src.Double)
                            && src.PropriedadeRegistrada(x => x.Double);
                    });
                }).ForMember(cmd => cmd.Enum, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Enum, src.Enum)
                            && src.PropriedadeRegistrada(x => x.Enum);
                    });
                }).ForMember(cmd => cmd.DateTime, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.DateTime, src.DateTime)
                            && src.PropriedadeRegistrada(x => x.DateTime);
                    });
                }).ForMember(cmd => cmd.TimeSpan, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.TimeSpan, src.TimeSpan)
                            && src.PropriedadeRegistrada(x => x.TimeSpan);
                    });
                }).ForMember(cmd => cmd.Guid, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Guid, src.Guid)
                            && src.PropriedadeRegistrada(x => x.Guid);
                    });
                }).ForMember(cmd => cmd.Bool, opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        return dest.InputTypeEhValido(x => x.Bool, src.Bool)
                            && src.PropriedadeRegistrada(x => x.Bool);
                    });
                });
            #endregion
        }
    }
}
