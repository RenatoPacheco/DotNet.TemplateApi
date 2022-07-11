using System.Linq;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;
using DotNetCore.API.Template.Dominio.Comandos.AutenticacaoCmds;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class AutenticacaoServ : Comum.BaseServ
    {
        public AutenticacaoServ(
            IAutorizacaoRep repAutorizacao)
        {
            _repAutorizacao = repAutorizacao;
        }

        protected readonly IAutorizacaoRep _repAutorizacao;
        protected Autenticacao _autenticacao;

        public Autenticacao Obter()
        {
            Notifications.Clear();

            if(_autenticacao is null)
            {
                _autenticacao = Autenticacao.GerarInterno(false);
                _autenticacao.Autorizacoes = _repAutorizacao.Listar().Where(
                    x => _autenticacao.EstaAutenticado || !x.RequerAutenticacao).ToArray();
            }

            return _autenticacao;
        }

        public Autenticacao Iniciar(IniciarAutenticacaoCmd comando)
        {
            Notifications.Clear();

            _autenticacao = Autenticacao.GerarNaoAutenticado(false);

            if (Validate(comando))
            {
                bool haChavePublica = AppSettings.ChavePublica == comando.ChavePublica;

                if (AppSettings.Autorizacao == comando.Token)
                {
                    _autenticacao = Autenticacao.GerarInterno(haChavePublica);
                }
                else
                {
                    _autenticacao = Autenticacao.GerarNaoAutenticado(haChavePublica);
                }
            }

            _autenticacao.Autorizacoes = _repAutorizacao.Listar().Where(
                x => x.EstaAutorizado(_autenticacao)).ToArray();

            return _autenticacao;
        }
    }
}
