using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.ConteudoPers;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio
{
    internal class ConteudoRep
        : Comum.SimplesRepositorio, IConteudoRep
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

            _persEditarConteudo.Executar(dados);
            IsValid(_persEditarConteudo);
        }

        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();

            _persExcluirConteudo.Executar(comando);
            IsValid(_persExcluirConteudo);
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

            ResultadoBusca<Conteudo> resultado = _persFiltrarConteudo.Executar(comando, referencia, tipo);
            IsValid(_persFiltrarConteudo);

            return resultado;
        }

        public void Inserir(Conteudo dados)
        {
            Notifications.Clear();

            _persInserirConteudo.Inserir(dados);
            IsValid(_persInserirConteudo);
        }
    }
}
