using System;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;
using BitHelp.Core.Type.pt_BR;

namespace TemplateApi.Dominio.Comandos.TesteCmds
{
    public class FormatosTesteCmd : ISelfValidation
    {
        public string String { get; set; }

        public int? Int { get; set; }

        public long? Long { get; set; }

        public decimal? Decimal { get; set; }

        public double? Double { get; set; }

        public float? Float { get; set; }

        public bool? Bool { get; set; }

        public DateTime? DateTime { get; set; }

        public TimeSpan? TimeSpan { get; set; }

        public Guid? Guid { get; set; }

        public Status? Enum { get; set; }

        public PhoneType? Phone { get; set; }

        #region Auto validação

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            return _notifications.IsValid();
        }

        #endregion
    }
}
