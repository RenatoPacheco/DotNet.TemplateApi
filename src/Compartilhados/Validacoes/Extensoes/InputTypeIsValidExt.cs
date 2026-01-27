using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Helpers;
using BitHelp.Core.Validation.Resources;
using System.Linq.Expressions;
using TemplateApi.Compartilhados.Validacoes.Notacoes;

namespace TemplateApi.Compartilhados.Validacoes.Extensoes {
    public static class InputTypeIsValidExt {

        #region To ISelfValidation

        public static ValidationNotification InputTypeIsValid<T, P>(
            this T source, Expression<Func<T, P>> expression)
            where T : ISelfValidation {
            return source.InputTypeIsValid(
                source.GetStructureToValidate(expression));
        }

        public static ValidationNotification InputTypeIsValid<T>(
            this T source, object value)
            where T : ISelfValidation {
            return source.InputTypeIsValid(new StructureToValidate {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            });
        }

        public static ValidationNotification InputTypeIsValid<T>(
            this T source, IStructureToValidate data)
            where T : ISelfValidation {
            return source.Notifications.InputTypeIsValid(data);
        }

        #endregion

        public static ValidationNotification InputTypeIsValid<T, P>(
            this ValidationNotification source, T data, Expression<Func<T, P>> expression) {
            return source.InputTypeIsValid(
                data.GetStructureToValidate(expression));
        }

        public static ValidationNotification InputTypeIsValid(
            this ValidationNotification source, object value) {
            return source.InputTypeIsValid(new StructureToValidate {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            });
        }

        public static ValidationNotification InputTypeIsValid(
            this ValidationNotification source, IStructureToValidate data) {
            source.CleanLastMessage();
            InputTypeValidAttribute validation = new();
            if (!validation.IsValid(data.Value)) {
                string text = validation.FormatErrorMessage(data.Display);
                var message = new ValidationMessage(text, data.Reference);
                source.SetLastMessage(message, data.Display);
                source.Add(message);
            }
            return source;
        }
    }
}
