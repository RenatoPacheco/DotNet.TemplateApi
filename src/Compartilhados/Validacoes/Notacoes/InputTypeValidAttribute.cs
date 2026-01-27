using BitHelp.Core.Validation.Notations;
using BitHelp.Core.Validation.Resources;
using TemplateApi.Compartilhados.ObjetosDeValor;

namespace TemplateApi.Compartilhados.Validacoes.Notacoes {
    [AttributeUsage(AttributeTargets.Property |
           AttributeTargets.Field, AllowMultiple = false)]
    public class InputTypeValidAttribute : ListIsValidAttribute {

        public InputTypeValidAttribute() : base() {
            ErrorMessageResourceName = nameof(Resource.XNotValid);
        }

        protected override bool Check(object value) {
            return value is IInputType output && output.IsValid();
        }
    }
}
