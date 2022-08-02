using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.UsuarioServ;
using TemplateApi.Dominio.Entidades;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class FiltrarUsuarioPers
        : Comum.BaseRepositorio
    {
        public FiltrarUsuarioPers(
            FiltrarUsuarioServ servFiltrarUsuario)
        {
            _servFiltrarUsuario = servFiltrarUsuario;
        }

        private readonly FiltrarUsuarioServ _servFiltrarUsuario;

        public ResultadoBusca<Usuario> Executar(
            FiltrarUsuarioCmd comando, string referencia)
        {
            return Executar(comando, referencia, ValidationType.Alert);
        }

        public ResultadoBusca<Usuario> Executar(
            FiltrarUsuarioCmd comando, ValidationType tipo)
        {
            return Executar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Usuario> Executar(
            FiltrarUsuarioCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Usuario> resultado = _servFiltrarUsuario.Executar(comando, referencia, tipo);
            IsValid(_servFiltrarUsuario);

            return resultado;
        }
    }
}
