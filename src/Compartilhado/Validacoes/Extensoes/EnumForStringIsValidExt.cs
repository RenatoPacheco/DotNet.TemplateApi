using System;
using System.Linq.Expressions;
using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Helpers;
using BitHelp.Core.Validation.Resources;
using TemplateApi.Compartilhado.Validacoes.Notacoes;

namespace TemplateApi.Compartilhado.Validacoes.Extensoes
{
    public static class EnumForStringIsValidExt
    {
        #region To ISelfValidation

        public static ValidationNotification EnumForStringIsValid<T, P>(
            this T source, Expression<Func<T, P>> expression, Type type)
            where T : ISelfValidation
        {
            return source.EnumForStringIsValid(
                source.GetStructureToValidate(expression),
                type);
        }

        public static ValidationNotification EnumForStringIsValid<T>(
            this T source, object value, Type type)
            where T : ISelfValidation
        {
            return source.EnumForStringIsValid(new StructureToValidate
            {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            }, type);
        }

        public static ValidationNotification EnumForStringIsValid<T>(
            this T source, IStructureToValidate data, Type type)
            where T : ISelfValidation
        {
            return source.Notifications.EnumForStringIsValid(data, type);
        }

        #endregion

        public static ValidationNotification EnumForStringIsValid<T, P>(
            this ValidationNotification source, T data, Expression<Func<T, P>> expression, Type type)
        {
            return source.EnumForStringIsValid(
                data.GetStructureToValidate(expression),
                type);
        }

        public static ValidationNotification EnumForStringIsValid(
            this ValidationNotification source, object value, Type type)
        {
            return source.EnumForStringIsValid(new StructureToValidate
            {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            }, type);
        }

        [Obsolete("Use EnumForStringIsValid(IStructureToValidate data, Type type)")]
        private static ValidationNotification EnumForStringIsValid(
            this ValidationNotification source, object value, string display, string reference, Type type)
        {
            return source.EnumForStringIsValid(new StructureToValidate
            {
                Value = value,
                Display = display,
                Reference = reference
            }, type);
        }

        public static ValidationNotification EnumForStringIsValid(
            this ValidationNotification source, IStructureToValidate data, Type type)
        {
            source.CleanLastMessage();
            EnumForStringIsValidAttribute validation = new EnumForStringIsValidAttribute(type);
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