using System;
using System.Reflection;
using System.ComponentModel;
using TemplateApi.Dominio.Servicos;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Aplicacao.Interceptadores;
using TemplateApi.Dominio.Comandos.UploadCmds;
using TemplateApi.Dominio.Notacoes;

namespace TemplateApi.Aplicacao
{
    [AcessoLivre]
    public class UploadApp
        : Comum.BaseAplicacao
    {
        public UploadApp(
            UploadServ servUpload,
            UploadInter interUpload,
            AutenticacaoServ servAutenticacao)
        : base(servAutenticacao)
        {
            _servUpload = servUpload;
            _interUpload = interUpload;
        }

        private readonly UploadServ _servUpload;
        private readonly UploadInter _interUpload;

        [Display(Name = "Upload de arquivos")]
        [Description("Permite upload de arquivos diversos entre imagens, documentos e planilhas.")]
        public IArquivo[] Arquivo(ArquivoUploadCmd comando)
        {
            Notifications.Clear();
            IArquivo[] resultado = Array.Empty<IArquivo>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interUpload.Arquivo(comando);
                resultado = _servUpload.Arquivo(comando);
                IsValid(_servUpload);
            }

            return resultado;
        }

        [Display(Name = "Upload de imagens")]
        [Description("Permite upload de arquivos diversos entre imagens.")]
        public IArquivo[] Imagem(ImagemUploadCmd comando)
        {
            Notifications.Clear();
            IArquivo[] resultado = Array.Empty<IArquivo>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interUpload.Imagem(comando);
                resultado = _servUpload.Imagem(comando);
                IsValid(_servUpload);
            }

            return resultado;
        }
    }
}
