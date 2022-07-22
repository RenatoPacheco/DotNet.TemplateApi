﻿using System;
using AutoMapper;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.TesteCmds;
using TemplateApi.Api.DataModel.TesteDataModel;

namespace TemplateApi.Api.Settings.AutoMapper
{
    public class TesteProfile : Profile
    {        public TesteProfile()
        {
            #region FormatosTesteCmd

            CreateMap<FormatosTesteDataModel, FormatosTesteCmd>()
                .ForMember(cmd => cmd.Int, opts => {
                    opts.MapFrom(src => (src.Int == null) ? null : (int?)src.Int);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Int?.IsValid() ?? false;

                        if ((src.Int != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Int);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Long, opts => {
                    opts.MapFrom(src => (src.Long == null) ? null : (long?)src.Long);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Long?.IsValid() ?? false;

                        if ((src.Long != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Long);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Decimal, opts => {
                    opts.MapFrom(src => (src.Decimal == null) ? null : (decimal?)src.Decimal);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Decimal?.IsValid() ?? false;

                        if ((src.Decimal != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Decimal);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Double, opts => {
                    opts.MapFrom(src => (src.Double == null) ? null : (double?)src.Double);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Double?.IsValid() ?? false;

                        if ((src.Double != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Double);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Enum, opts => {
                    opts.MapFrom(src => (src.Enum == null) ? null : (Status?)src.Enum);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Enum?.IsValid() ?? false;

                        if ((src.Enum != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Enum);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.DateTime, opts => {
                    opts.MapFrom(src => (src.DateTime == null) ? null : (DateTime?)src.DateTime);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.DateTime?.IsValid() ?? false;

                        if ((src.DateTime != null) && !ehValido)
                            dest.AddErrorNotification(x => x.DateTime);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.TimeSpan, opts => {
                    opts.MapFrom(src => (src.TimeSpan == null) ? null : (TimeSpan?)src.TimeSpan);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.TimeSpan?.IsValid() ?? false;

                        if ((src.TimeSpan != null) && !ehValido)
                            dest.AddErrorNotification(x => x.TimeSpan);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Guid, opts => {
                    opts.MapFrom(src => (src.Guid == null) ? null : (Guid?)src.Guid);
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Guid?.IsValid() ?? false;

                        if ((src.Bool != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Guid);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Bool, opts => {
                    opts.MapFrom(src => (src.Bool == null) ? null : (bool?)src.Bool);
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
