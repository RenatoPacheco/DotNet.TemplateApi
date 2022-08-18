using System;
using BitHelp.Core.Validation;
using Newtonsoft.Json;
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

        public bool? Bool { get; set; }

        public DateTime? DateTime { get; set; }

        public TimeSpan? TimeSpan { get; set; }

        public Guid? Guid { get; set; }

        public Status? Enum { get; set; }

        public PhoneType? Phone { get; set; }

        #region Auto validação

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            return Notifications.IsValid();
        }

        #endregion
    }
}
