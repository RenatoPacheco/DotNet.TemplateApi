using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
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
