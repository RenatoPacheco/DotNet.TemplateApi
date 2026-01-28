using TemplateApi.Api.ApiServices;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Dominio.Comandos.StorageCmds;

namespace TemplateApi.Api.DataModels.StorageDataModel {

    public class InserirStorageDataModel
        : Common.BaseDataModel<InserirStorageDataModel> {

        private IList<IFormFile> _arquivo;
        /// <summary>
        /// O peso do arquivo não pode ser maior que 100 kb, 
        /// e os tipos permitidos são imagens, textos ou planilhas.
        /// </summary>
        public IList<IFormFile> Arquivo {
            get => _arquivo ??= new List<IFormFile>();
            set {
                _arquivo = value ?? new List<IFormFile>();
                RegistrarPropriedade();
            }
        }

        public InserirStorageCmd Montar(RequestApiServ request) {
            var resultado = new InserirStorageCmd();

            if (PropriedadeRegistrada(x => x.Arquivo)) {
                resultado.Arquivo = Arquivo.Select(x => {
                    var item = new StoragePrivado(x, request);
                    return item as Dominio.ObjetosDeValor.Arquivo;
                }).ToList();
            }

            resultado.AddNotifications(this);

            return resultado;
        }

        public override bool IsValid() {
            return _notifications.IsValid();
        }
    }
}
