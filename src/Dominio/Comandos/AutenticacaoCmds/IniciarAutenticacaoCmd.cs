using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Comandos.AutenticacaoCmds
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

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            return _notifications.IsValid();
        }

        #endregion
    }
}
