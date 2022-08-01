using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Infra.Banco.TemplateApi.Servicos.ConteudoServ;
using TemplateApi.Dominio.Entidades;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class FiltrarConteudoPers
        : Comum.BaseRepositorio
    {
        public FiltrarConteudoPers(
            FiltrarConteudoServ servFiltrarConteudo)
        {
            _servFiltrarConteudo = servFiltrarConteudo;
        }

        private readonly FiltrarConteudoServ _servFiltrarConteudo;

        public ResultadoBusca<Conteudo> Executar(
            FiltrarConteudoCmd comando, string referencia)
        {
            return Executar(comando, referencia, ValidationType.Alert);
        }

        public ResultadoBusca<Conteudo> Executar(
            FiltrarConteudoCmd comando, ValidationType tipo)
        {
            return Executar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Conteudo> Executar(
            FiltrarConteudoCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Conteudo> resultado = _servFiltrarConteudo.Executar(comando, referencia, tipo);
            IsValid(_servFiltrarConteudo);

            return resultado;
        }
    }
}
