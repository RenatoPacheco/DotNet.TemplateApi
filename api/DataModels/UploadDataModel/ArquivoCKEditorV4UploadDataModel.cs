using TemplateApi.Api.ApiServices;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Dominio.Comandos.UploadCmds;

namespace TemplateApi.Api.DataModels.UploadDataModel {
    public class ArquivoCKEditorV4UploadDataModel
        : Common.BaseDataModel<ArquivoCKEditorV4UploadDataModel> {

        private IFormFile _arquivo;
        /// <summary>
        /// O peso do arquivo não pode ser maior que 100 kb, 
        /// e os tipos permitidos são imagens, textos ou planilhas.
        /// </summary>
        public IFormFile Arquivo {
            get => _arquivo;
            set {
                _arquivo = value;
                RegistrarPropriedade();
            }
        }

        public ArquivoUploadCmd Montar(RequestApiServ request) {
            var resultado = new ArquivoUploadCmd();

            if (PropriedadeRegistrada(x => x.Arquivo)) {
                resultado.Arquivo = Arquivo != null
                    ? new List<Dominio.ObjetosDeValor.Arquivo> {
                        new StoragePublico(Arquivo, request)
                    } : new List<Dominio.ObjetosDeValor.Arquivo>();
            }

            resultado.AddNotifications(this);

            return resultado;
        }

        public override bool IsValid() {
            return _notifications.IsValid();
        }
    }
}
