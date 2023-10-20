using AutoMapper;
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

            CreateMap<BoolInput, bool>().ConvertUsing(v => v == null || !v.IsValid() ? false : (bool)v);
            CreateMap<BoolInput, bool?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (bool?)v);

            CreateMap<DateTimeInput, DateTime>().ConvertUsing(v => v == null || !v.IsValid() ? new DateTime() : (DateTime)v);
            CreateMap<DateTimeInput, DateTime?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (DateTime?)v);

            CreateMap<DecimalInput, decimal>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (decimal)v);
            CreateMap<DecimalInput, decimal?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (decimal?)v);

            CreateMap<DoubleInput, double>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (double)v);
            CreateMap<DoubleInput, double?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (double?)v);

            CreateMap<EnumInput<Status>, Status>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (Status)v);
            CreateMap<EnumInput<Status>, Status?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (Status?)v);

            CreateMap<EnumInput<ContextoCmd>, ContextoCmd>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (ContextoCmd)v);
            CreateMap<EnumInput<ContextoCmd>, ContextoCmd?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (ContextoCmd?)v);

            CreateMap<FloatInput, float>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (float)v);
            CreateMap<FloatInput, float?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (float?)v);

            CreateMap<GuidInput, Guid>().ConvertUsing(v => v == null || !v.IsValid() ? Guid.Empty : (Guid)v);
            CreateMap<GuidInput, Guid?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (Guid?)v);

            CreateMap<IntInput, int>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (int)v);
            CreateMap<IntInput, int?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (int?)v);

            CreateMap<LongInput, long>().ConvertUsing(v => v == null || !v.IsValid() ? 0 : (long)v);
            CreateMap<LongInput, long?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (long?)v);

            CreateMap<TimeSpanInput, TimeSpan>().ConvertUsing(v => v == null || !v.IsValid() ? new TimeSpan() : (TimeSpan)v);
            CreateMap<TimeSpanInput, TimeSpan?>().ConvertUsing(v => v == null || !v.IsValid() ? null : (TimeSpan?)v);
        }
    }
}
