using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using TemplateApi.Dominio.ObjetosDeValor;
using BitHelp.Core.ManageFile;

namespace TemplateApi.Api.ValuesObject
{
    public class ArquivoUpload : Arquivo
    {
        public ArquivoUpload(IFormFile formFile)
        {
            string folder = $"storage/{DateTime.Now:yyyy/MM/dd}";
            
            _formFile = formFile;

            Nome = formFile.FileName;
            Extensao = formFile.FileName[formFile.FileName.LastIndexOf(".")..];
            Tipo = formFile.ContentType; 
            Alias = $"{Guid.NewGuid():N}{Extensao}";
            Diretorio = folder;
            Peso = formFile.Length;
            Referencia = $"{Diretorio}/{Alias}";            
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

            using (FileStream filestream = File.Create(Referencia))
            {
                Checksum = CheckSUM.GetMD5Hash(filestream);
                _formFile.CopyTo(filestream);
                filestream.Flush();
            }
        }
    }
}
