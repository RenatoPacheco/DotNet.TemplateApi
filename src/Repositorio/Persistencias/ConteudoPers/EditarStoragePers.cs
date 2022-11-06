using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.ConteudoServ;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class EditarConteudoPers
        : Comum.BaseRepositorio
    {
        public EditarConteudoPers(
            EditarConteudoServ servEditarConteudo)
        {
            _servEditarConteudo = servEditarConteudo;
        }

        private readonly EditarConteudoServ _servEditarConteudo;

        public void Executar(Conteudo dados)
        {
            Notifications.Clear();

            _servEditarConteudo.Executar(dados);
            IsValid(_servEditarConteudo);
        }
    }
}
