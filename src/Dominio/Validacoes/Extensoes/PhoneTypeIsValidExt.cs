using System;
using System.Linq.Expressions;
using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Helpers;
using BitHelp.Core.Validation.Resources;
using DotNetCore.API.Template.Dominio.Validacoes.Notacoes;

namespace DotNetCore.API.Template.Dominio.Validacoes.Extensoes
{
    public static  class PhoneTypeIsValidExt
    {
        #region To ISelfValidation

        public static ValidationNotification PhoneTypeIsValid<T, P>(
            this T source, Expression<Func<T, P>> expression)
            where T : ISelfValidation
        {
            return source.PhoneTypeIsValid(
                source.GetStructureToValidate(expression));
        }

        public static ValidationNotification PhoneTypeIsValid<T>(
            this T source, object value)
            where T : ISelfValidation
        {
            return source.PhoneTypeIsValid(new StructureToValidate
            {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            });
        }

        public static ValidationNotification PhoneTypeIsValid<T>(
            this T source, IStructureToValidate data)
            where T : ISelfValidation
        {
            return source.Notifications.PhoneTypeIsValid(data);
        }

        #endregion

        public static ValidationNotification PhoneTypeIsValid<T, P>(
            this ValidationNotification source, T data, Expression<Func<T, P>> expression)
        {
            return source.PhoneTypeIsValid(
                data.GetStructureToValidate(expression));
        }

        public static ValidationNotification PhoneTypeIsValid(
            this ValidationNotification source, object value)
        {
            return source.PhoneTypeIsValid(new StructureToValidate
            {
                Value = value,
                Display = Resource.DisplayValue,
                Reference = null
            });
        }

        public static ValidationNotification PhoneTypeIsValid(
            this ValidationNotification source, IStructureToValidate data)
        {
            source.CleanLastMessage();
            PhoneTypeIsValidAttribute validation = new PhoneTypeIsValidAttribute();
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
