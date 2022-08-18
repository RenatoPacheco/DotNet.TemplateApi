using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class InserirStorageCmd : ISelfValidation
    {
        private IList<Arquivo> _arquivo;
        /// <summary>
        /// O peso do arquivo não pode ser maior que 100 kb, 
        /// e os tipos permitidos são imagens, textos e planilhas.
        /// </summary>
        public IList<Arquivo> Arquivo
        {
            get => _arquivo ??= new List<Arquivo>();
            set 
            { 
                _arquivo = value ?? new List<Arquivo>();
                this.RemoveAtReference(x => x.Arquivo);
                
                this.MaxNumberIsValid(Arquivo.Select(x => x.Peso), 1024 * 100)
                    .SetReference(nameof(Arquivo)).SetMessage("Peso do arquivo não pode ser maior que 100 kb.");

                this.RegexIsValid(Arquivo.Select(x => x.Extensao), @"^(\.)(jpg|jpeg|png|doc|docx|xls|xlsx|txt|xml|pdf)$", RegexOptions.IgnoreCase)
                    .SetReference(nameof(Arquivo)).SetMessage("Tipo de arquivo não é válido.");
            }
        }

        public void Aplicar(ref Storage dados, Arquivo arquivo)
        {
            arquivo.Salvar();
            dados = new Storage(arquivo);
        }

        public void Desfazer(ref Storage dados, Arquivo arquivo)
        {
            dados = null;
            arquivo.Excluir();
        }

        #region Auto validação

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Arquivo);
            return Notifications.IsValid();
        }

        #endregion

    }
}
