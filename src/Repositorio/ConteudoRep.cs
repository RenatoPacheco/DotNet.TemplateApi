using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.ConteudoPers;

namespace TemplateApi.Repositorio
{
    internal class ConteudoRep
        : Comum.SimplesRep, IConteudoRep
    {
        public ConteudoRep(
            Conexao conexao,
            IUnidadeTrabalho udt,
            InserirConteudoPers persInserirConteudo,
            EditarConteudoPers persEditarConteudo,
            ExcluirConteudoPers persExcluirConteudo,
            FiltrarConteudoPers persFiltrarConteudo)
            : base(conexao, udt) 
        {
            _persInserirConteudo = persInserirConteudo;
            _persEditarConteudo = persEditarConteudo;
            _persExcluirConteudo = persExcluirConteudo;
            _persFiltrarConteudo = persFiltrarConteudo;
        }

        private readonly InserirConteudoPers _persInserirConteudo;
        private readonly EditarConteudoPers _persEditarConteudo;
        private readonly ExcluirConteudoPers _persExcluirConteudo;
        private readonly FiltrarConteudoPers _persFiltrarConteudo;

        public void Editar(Conteudo dados)
        {
            Notifications.Clear();

            _persEditarConteudo.Editar(dados);
            Validate(_persEditarConteudo);
        }

        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();

            _persExcluirConteudo.Excluir(comando);
            Validate(_persExcluirConteudo);
        }

        public ResultadoBusca<Conteudo> Filtrar(
            FiltrarConteudoCmd comando, string referencia)
        {
            return Filtrar(comando, referencia, ValidationType.Alert);
        }

        public ResultadoBusca<Conteudo> Filtrar(
            FiltrarConteudoCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Conteudo> Filtrar(
            FiltrarConteudoCmd comando, string referencia = "", 
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Conteudo> resultado = _persFiltrarConteudo.Filtrar(comando, referencia, tipo);
            Validate(_persFiltrarConteudo);

            return resultado;
        }

        public void Inserir(Conteudo dados)
        {
            Notifications.Clear();

            _persInserirConteudo.Inserir(dados);
            Validate(_persInserirConteudo);
        }
    }
}
