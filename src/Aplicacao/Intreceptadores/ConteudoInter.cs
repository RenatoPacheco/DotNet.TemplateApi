using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Aplicacao.Intreceptadores
{
    public class ConteudoInter : Comum.BaseInter
    {
        public ConteudoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        internal void Excluir(ExcluirConteudoCmd comando)
        {

        }

        internal void Editar(EditarConteudoCmd comando)
        {

        }

        internal void Inserir(InserirConteudoCmd comando)
        {

        }

        internal void Filtrar(FiltrarConteudoCmd comando)
        {

        }
    }
}
