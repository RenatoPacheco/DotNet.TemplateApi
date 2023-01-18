using System;
using System.IO;
using BitHelp.Core.ManageFile;
using Microsoft.AspNetCore.Http;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.ValuesObject
{
    public class StoragePublico : Arquivo
    {
        public StoragePublico(
            IFormFile formFile)
        {
            string folder = $"storage/public/{DateTime.Now:yyyy/MM/dd}";

            _formFile = formFile;

            Nome = formFile.FileName;
            Extensao = formFile.FileName[formFile.FileName.LastIndexOf(".")..]?.ToLower();
            Tipo = formFile.ContentType; 
            Alias = $"{Guid.NewGuid():N}{Extensao}";
            Diretorio = folder;
            Peso = formFile.Length;
            Referencia = $"{Diretorio}/{Alias}";
            Url = $"/{Referencia}";
        }

        private readonly IFormFile _formFile;

        public override void Excluir()
        {
            if (Directory.Exists(Diretorio))
            {
                if (File.Exists(Referencia))
                {
                    File.Delete(Referencia);
                }
            }
        }

        public override void Salvar()
        {
            if (!Directory.Exists(Diretorio))
            {
                Directory.CreateDirectory(Diretorio);
            }

            using (Stream filestream = _formFile.OpenReadStream())
            {
                Checksum = CheckSUM.GetMD5Hash(filestream);
            }

            using (FileStream filestream = File.Create(Referencia))
            {
                _formFile.CopyTo(filestream);
                filestream.Flush();
            }
        }
    }
}
