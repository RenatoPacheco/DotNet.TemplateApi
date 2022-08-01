using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Repositorio.Interfaces;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.Banco.TemplateApi.Servicos.ConteudoServ;

namespace TemplateApi.Repositorio
{
    internal class ConteudoRep
        : Comum.SimplesRepositorio, IConteudoRep
    {
        public ConteudoRep(
            Conexao conexao,
            IUnidadeTrabalho udt,
            InserirConteudoServ persInserirConteudo,
            EditarConteudoServ persEditarConteudo,
            ExcluirConteudoServ persExcluirConteudo,
            FiltrarConteudoServ persFiltrarConteudo)
            : base(conexao, udt)
        {
            _persInserirConteudo = persInserirConteudo;
            _persEditarConteudo = persEditarConteudo;
            _persExcluirConteudo = persExcluirConteudo;
            _persFiltrarConteudo = persFiltrarConteudo;
        }

        private readonly InserirConteudoServ _persInserirConteudo;
        private readonly EditarConteudoServ _persEditarConteudo;
        private readonly ExcluirConteudoServ _persExcluirConteudo;
        private readonly FiltrarConteudoServ _persFiltrarConteudo;

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
