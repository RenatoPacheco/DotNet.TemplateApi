using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Comandos.UploadCmds;

namespace TemplateApi.Aplicacao.Interceptadores
{
    public class UploadInter : Comum.BaseInterceptador
    {
        public UploadInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        public void Arquivo(ArquivoUploadCmd comando)
        {

        }

        public void Imagem(ImagemUploadCmd comando)
        {

        }
    }
}
