using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Comandos.UploadCmds
{
    public class ArquivoUploadCmd
        : ISelfValidation
    {
        private IList<Arquivo> _arquivo = new List<Arquivo>();

        public IList<Arquivo> Arquivo
        {
            get => _arquivo;
            set
            {
                _arquivo = value ?? new List<Arquivo>();
                this.RemoveAtReference(x => x.Arquivo);
                this.MaxItemsIsValid(x => x.Arquivo, 20);
                this.SingletonItemsIsValid(x => x.Arquivo);

                this.RegexIsValid(x => x.Extensao, @"\.(jpg|jpeg|png|doc|docx|xls|xlsx|txt|pdf)", RegexOptions.IgnoreCase)
                    .SetReference(nameof(Arquivo));

                this.MaxNumberIsValid(x => x.Peso, 2 * 1024 * 1024)
                    .SetMessage("{0} não pode ter um valor maior que 2 MB.")
                    .SetReference(nameof(Arquivo));
            }
        }

        private IEnumerable<long> Peso => Arquivo.Select(x => x.Peso);

        [Display(Name = "Extensão")]
        private IEnumerable<string> Extensao => Arquivo.Select(x => x.Extensao);

        #region Auto validação

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        bool ISelfValidation.IsValid()
        {
            this.RequiredIsValid(x => x.Arquivo);

            return _notifications.IsValid();
        }

        #endregion

    }
}
