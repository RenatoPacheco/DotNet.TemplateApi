using System;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Repositorio
{
    internal class UsuarioRep
        : Comum.SimplesRep, IUsuarioRep
    {
        public UsuarioRep(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Editar(Usuario dados)
        {
            throw new NotImplementedException();
        }

        public void Excluir(ExcluirUsuarioCmd comando)
        {
            throw new NotImplementedException();
        }

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Usuario dados)
        {
            throw new NotImplementedException();
        }
    }
}
