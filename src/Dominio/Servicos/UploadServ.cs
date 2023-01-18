using System;
using System.Linq;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UploadCmds;

namespace TemplateApi.Dominio.Servicos
{
    public class UploadServ : Comum.BaseServico
    {
        public IArquivo[] Arquivo(ArquivoUploadCmd comando)
        {
            Notifications.Clear();

            Arquivo[] resultado = Array.Empty<Arquivo>();

            if (IsValid(comando))
            {
                resultado = comando.Arquivo.ToArray();
                foreach (Arquivo item in resultado)
                {
                    item.Salvar();
                }
            }

            return resultado;
        }

        public IArquivo[] Imagem(ImagemUploadCmd comando)
        {
            Notifications.Clear();

            Arquivo[] resultado = Array.Empty<Arquivo>();

            if (IsValid(comando))
            {
                resultado = comando.Arquivo.ToArray();
                foreach (Arquivo item in resultado)
                {
                    item.Salvar();
                }
            }

            return resultado;
        }
    }
}
