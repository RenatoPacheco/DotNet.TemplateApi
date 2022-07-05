using System.Linq;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Recurso;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;

namespace DotNetCore.API.Template.Repositorio.Persistencias.UsuarioPers
{
    internal class FiltrarUsuarioPers : Comum.SimplesRep
    {
        public FiltrarUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }


        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando, string referencia)
        {
            return Filtrar(comando, referencia);
        }

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando, string referencia = "", ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Usuario> resultado = new ResultadoBusca<Usuario>();

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                if (comando.Maximo != 1)
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrados, NomesResx.Usuarios), referencia, tipo));
                }
                else
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrado, NomesResx.Usuario), referencia, tipo));
                }
            }

            return resultado;
        }
    }
}
