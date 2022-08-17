using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class CustonTypesProfile : Profile
    {
        public CustonTypesProfile()
        {
            CreateMap<IFormFile, Arquivo>().ConvertUsing(v => v == null ? null : new ArquivoUpload(v));

            CreateMap<EnumInput<Status>, Status>().ConvertUsing(v => v == null ? 0 : (Status)v);
            CreateMap<EnumInput<Status>, Status?>().ConvertUsing(v => v == null ? null : (Status?)v);

            CreateMap<EnumInput<ContextoCmd>, ContextoCmd>().ConvertUsing(v => v == null ? 0 : (ContextoCmd)v);
            CreateMap<EnumInput<ContextoCmd>, ContextoCmd?>().ConvertUsing(v => v == null ? null : (ContextoCmd?)v);

            CreateMap<IntInput, int>().ConvertUsing(v => v == null ? 0 : (int)v);
            CreateMap<IntInput, int?>().ConvertUsing(v => v == null ? null : (int?)v);

            CreateMap<LongInput, long>().ConvertUsing(v => v == null ? 0 : (long)v);
            CreateMap<LongInput, long?>().ConvertUsing(v => v == null ? null : (long?)v);

            CreateMap<DecimalInput, decimal>().ConvertUsing(v => v == null ? 0 : (decimal)v);
            CreateMap<DecimalInput, decimal?>().ConvertUsing(v => v == null ? null : (decimal?)v);

            CreateMap<DoubleInput, double>().ConvertUsing(v => v == null ? 0 : (double)v);
            CreateMap<DoubleInput, double?>().ConvertUsing(v => v == null ? null : (double?)v);

            CreateMap<BoolInput, bool>().ConvertUsing(v => v == null ? false : (bool)v);
            CreateMap<BoolInput, bool?>().ConvertUsing(v => v == null ? null : (bool?)v);

            CreateMap<GuidInput, Guid>().ConvertUsing(v => v == null ? Guid.Empty : (Guid)v);
            CreateMap<GuidInput, Guid?>().ConvertUsing(v => v == null ? null : (Guid?)v);

            CreateMap<DateTimeInput, DateTime>().ConvertUsing(v => v == null ? new DateTime() : (DateTime)v);
            CreateMap<DateTimeInput, DateTime?>().ConvertUsing(v => v == null ? null : (DateTime?)v);

            CreateMap<TimeSpanInput, TimeSpan>().ConvertUsing(v => v == null ? new TimeSpan() : (TimeSpan)v);
            CreateMap<TimeSpanInput, TimeSpan?>().ConvertUsing(v => v == null ? null : (TimeSpan?)v);
        }
    }
}
