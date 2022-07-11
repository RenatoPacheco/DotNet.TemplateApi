using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;

namespace DotNetCore.API.Template.Dominio.Comandos.AutenticacaoCmds
{
    public class IniciarAutenticacaoCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        private string _token;
        /// <summary>
        /// Token de acesso
        /// </summary>
        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                this.RemoveAtReference(x => Token);
            }
        }

        private string _chavePublica;
        /// <summary>
        /// Chave pública
        /// </summary>
        public string ChavePublica
        {
            get => _chavePublica;
            set
            {
                _chavePublica = value;
                this.RemoveAtReference(x => ChavePublica);
            }
        }

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
