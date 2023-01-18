using Microsoft.AspNetCore.Http;

namespace TemplateApi.Api.DataModels.UploadDataModel
{
    public class ArquivoCKEditorV4UploadDataModel
        : Common.BaseDataModel<ArquivoCKEditorV4UploadDataModel>
    {
        private IFormFile _arquivo;
        /// <summary>
        /// O peso do arquivo não pode ser maior que 100 kb, 
        /// e os tipos permitidos são imagens, textos ou planilhas.
        /// </summary>
        public IFormFile Arquivo
        {
            get => _arquivo;
            set
            {
                _arquivo = value;
                RegistarPropriedade();
            }
        }
    }
}
