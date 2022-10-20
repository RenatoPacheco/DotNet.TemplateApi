using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class EditarStorageCmd 
        : Comum.EditarBaseCmd, ISelfValidation
    {
        public EditarStorageCmd()
        {
            _escopo = new StorageEscp<EditarStorageCmd>(this);
        }

        private long? _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public long? Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                RegistrarCampo();
                _escopo.IdEhValido(x => x.Storage);
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de storage
        /// </summary>
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                RegistrarCampo();
                _escopo.NomeEhValido(x => x.Nome);
            }
        }

        private Status? _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status? Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistrarCampo();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        public void Aplicar(ref Storage dados)
        {
            if (CampoFoiRegistrado(nameof(Nome)))
            {
                dados.Nome = Nome;
            }

            if (CampoFoiRegistrado(nameof(Status)))
            {
                dados.Status = (Status)Status;
            }
        }

        public void Desfazer(ref Storage dados) => dados = null;

        #region Auto validação

        protected readonly StorageEscp<EditarStorageCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Storage);

            return _notifications.IsValid();
        }

        #endregion
    }
}
