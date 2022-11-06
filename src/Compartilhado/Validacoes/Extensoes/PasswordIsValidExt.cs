using System;
using System.Linq.Expressions;
using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Helpers;
using BitHelp.Core.Validation.Resources;
using TemplateApi.Compartilhado.Validacoes.Notacoes;

namespace TemplateApi.Compartilhado.Validacoes.Extensoes
{
    public static  class PasswordIsValidExt
    {
        #region To ISelfValidation

        public static ValidationNotification PasswordIsValid<T, P>(
            this T source, Expression<Func<T, P>> expression)
            where T : ISelfValidation
        {
            return source.PasswordIsValid(
                source.GetStructureToValidate(expression));
        }

        public static ValidationNotification PasswordIsValid<T>(
            this T source, object value)
            where T : ISelfValidation
        {
            return source.PasswordIsValid(new StructureToValidate
            {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            });
        }

        public static ValidationNotification PasswordIsValid<T>(
            this T source, IStructureToValidate data)
            where T : ISelfValidation
        {
            return source.Notifications.PasswordIsValid(data);
        }

        #endregion

        public static ValidationNotification PasswordIsValid<T, P>(
            this ValidationNotification source, T data, Expression<Func<T, P>> expression)
        {
            return source.PasswordIsValid(
                data.GetStructureToValidate(expression));
        }

        public static ValidationNotification PasswordIsValid(
            this ValidationNotification source, object value)
        {
            return source.PasswordIsValid(new StructureToValidate
            {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            });
        }

        public static ValidationNotification PasswordIsValid(
            this ValidationNotification source, IStructureToValidate data)
        {
            source.CleanLastMessage();
            PasswordIsValidAttribute validation = new PasswordIsValidAttribute();
            if (!validation.IsValid(data.Value))
            {
                string text = validation.FormatErrorMessage(data.Display);
                var message = new ValidationMessage(text, data.Reference);
                source.SetLastMessage(message, data.Display);
                source.Add(message);
            }
            return source;
        }
    }
}
