using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TemplateApi.Api.DataModels.UploadDataModel
{
    public class ImagemCKEditorV4UploadDataModel
        : Common.BaseDataModel<ImagemCKEditorV4UploadDataModel>
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
                RegistrarPropriedade();
            }
        }
    }
}
