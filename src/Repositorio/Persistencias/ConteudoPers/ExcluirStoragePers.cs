using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.ConteudoServ;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class ExcluirConteudoPers
        : Comum.BaseRepositorio
    {
        public ExcluirConteudoPers(
            ExcluirConteudoServ servExcluirConteudo)
        {
            _servExcluirConteudo = servExcluirConteudo;
        }

        private readonly ExcluirConteudoServ _servExcluirConteudo;

        public void Executar(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();

            _servExcluirConteudo.Executar(comando);
            IsValid(_servExcluirConteudo);
        }
    }
}
