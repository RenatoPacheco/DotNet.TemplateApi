using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Banco.TemplateApi.Servicos.ConteudoServ;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class InserirConteudoPers
        : Comum.BaseRepositorio
    {
        public InserirConteudoPers(
            InserirConteudoServ servInserirConteudo)
        {
            _servInserirConteudo = servInserirConteudo;
        }

        private readonly InserirConteudoServ _servInserirConteudo;

        public void Executar(Conteudo dados)
        {
            Notifications.Clear();

            _servInserirConteudo.Executar(dados);
            IsValid(_servInserirConteudo);
        }
    }
}
