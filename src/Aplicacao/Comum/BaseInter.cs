﻿using System.Reflection;
using BitHelp.Core.Validation;

namespace DotNetCore.API.Template.Aplicacao.Comum
{
    public abstract class BaseInter : ISelfValidation
    {
        public ValidationNotification Notifications { get; protected set; } = new ValidationNotification();

        public bool Validate(ISelfValidation valor)
        {
            bool resultado = valor.IsValid();
            Notifications.Add(valor);
            return resultado;
        }

        public bool IsValid()
        {
            return Notifications.IsValid();
        }

        public bool EhAutorizado(MethodBase metodo)
        {
            return true;
        }
    }
}