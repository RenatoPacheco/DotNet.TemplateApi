using System;
using BitHelp.Core.Validation.Notations;

namespace TemplateApi.Compartilhado.Validacoes.Notacoes
{
    [AttributeUsage(AttributeTargets.Property |
           AttributeTargets.Field, AllowMultiple = false)]
    public class EnumForStringIsValidAttribute : ListIsValidAttribute
    {
        public EnumForStringIsValidAttribute(Type type) : base()
        {
            Type = type;
        }

        public Type Type { get; set; }

        protected override bool Check(object value)
        {
            return Enum.IsDefined(Type, value.ToString());
        }
    }
}
