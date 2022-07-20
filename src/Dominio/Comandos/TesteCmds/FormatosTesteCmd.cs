using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.TesteCmds
{
    public class FormatosTesteCmd : ISelfValidation
    {
        public string String { get; set; }

        public int? Int { get; set; }

        public long? Long { get; set; }

        public decimal? Decimal { get; set; }

        public double? Double { get; set; }

        public Status? Enum { get; set; }

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
