using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.UsuarioPers;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio
{
    internal class UsuarioRep
        : Comum.SimplesRepositorio, IUsuarioRep
    {
        public UsuarioRep(
            Conexao conexao,
            IUnidadeTrabalho udt,
            InserirUsuarioPers persInserirUsuario,
            EditarUsuarioPers persEditarUsuario,
            ExcluirUsuarioPers persExcluirUsuario,
            FiltrarUsuarioPers persFiltrarUsuario)
            : base(conexao, udt)
        {
            _persInserirUsuario = persInserirUsuario;
            _persEditarUsuario = persEditarUsuario;
            _persExcluirUsuario = persExcluirUsuario;
            _persFiltrarUsuario = persFiltrarUsuario;
        }

        private readonly InserirUsuarioPers _persInserirUsuario;
        private readonly EditarUsuarioPers _persEditarUsuario;
        private readonly ExcluirUsuarioPers _persExcluirUsuario;
        private readonly FiltrarUsuarioPers _persFiltrarUsuario;

        public void Editar(Usuario dados)
        {
            Notifications.Clear();

            _persEditarUsuario.Executar(dados);
            IsValid(_persEditarUsuario);
        }

        public void Excluir(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();

            _persExcluirUsuario.Executar(comando);
            IsValid(_persExcluirUsuario);
        }

        public ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, string referencia)
        {
            return Filtrar(comando, referencia, ValidationType.Alert);
        }

        public ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Usuario> resultado = _persFiltrarUsuario.Executar(comando, referencia, tipo);
            IsValid(_persFiltrarUsuario);

            return resultado;
        }

        public void Inserir(Usuario dados)
        {
            Notifications.Clear();

            _persInserirUsuario.Executar(dados);
            IsValid(_persInserirUsuario);
        }
    }
}
