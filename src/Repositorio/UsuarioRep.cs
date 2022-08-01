using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Interfaces;
using TemplateApi.Repositorio.Persistencias.Banco.TemplateApi.Servicos.UsuarioServ;

namespace TemplateApi.Repositorio
{
    internal class UsuarioRep
        : Comum.SimplesRepositorio, IUsuarioRep
    {
        public UsuarioRep(
            Conexao conexao,
            IUnidadeTrabalho udt,
            InserirUsuarioServ persInserirUsuario,
            EditarUsuarioServ persEditarUsuario,
            ExcluirUsuarioServ persExcluirUsuario,
            FiltrarUsuarioServ persFiltrarUsuario)
            : base(conexao, udt)
        {
            _persInserirUsuario = persInserirUsuario;
            _persEditarUsuario = persEditarUsuario;
            _persExcluirUsuario = persExcluirUsuario;
            _persFiltrarUsuario = persFiltrarUsuario;
        }

        private readonly InserirUsuarioServ _persInserirUsuario;
        private readonly EditarUsuarioServ _persEditarUsuario;
        private readonly ExcluirUsuarioServ _persExcluirUsuario;
        private readonly FiltrarUsuarioServ _persFiltrarUsuario;

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
