using TemplateApi.Api.ApiServices;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Dominio.Comandos.UploadCmds;

namespace TemplateApi.Api.DataModels.UploadDataModel {
    public class ImagemUploadDataModel
        : Common.BaseDataModel<ImagemUploadDataModel> {

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

        public ImagemUploadCmd Montar(RequestApiServ request) {
            var resultado = new ImagemUploadCmd();

            if (PropriedadeRegistrada(x => x.Arquivo)) {
                resultado.Arquivo = Arquivo.Select(x => {
                    var item = new StoragePublico(x, request);
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
