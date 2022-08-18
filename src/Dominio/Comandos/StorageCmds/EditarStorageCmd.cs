using BitHelp.Core.Validation;
using Newtonsoft.Json;
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
                RegistrarCampo(nameof(Storage));
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
                RegistrarCampo(nameof(Nome));
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
                RegistrarCampo(nameof(Status));
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

        protected StorageEscp<EditarStorageCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequerido(x => x.Storage);

            return Notifications.IsValid();
        }

        #endregion
    }
}
