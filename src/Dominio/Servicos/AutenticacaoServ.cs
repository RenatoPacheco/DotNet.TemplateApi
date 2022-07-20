using System.Linq;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Dominio.Comandos.AutenticacaoCmds;
using System.Reflection;
using TemplateApi.RecursoResx;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Servicos
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
                    x => _autenticacao.EstaAutenticado || !x.RequerAutorizacao).ToArray();
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

        public bool EstaAutorizado(MethodBase metodo, ValidationType erro = ValidationType.Error)
        {
            Notifications.Clear();
            Autorizacao requisito = _repAutorizacao.Listar().Where(x => x.Metodo == metodo).FirstOrDefault();
            
            bool chavePublica = !requisito.RequerChavePublica || _autenticacao.HaChavePublica;
            bool autorizacao = !requisito.RequerAutorizacao || _autenticacao.Autorizacoes.Any(x => x.Metodo == metodo);

            if (!chavePublica || !autorizacao)
            {
                Notifications.Add(
                    new ValidationMessage(
                        AvisosResx.AcessoNaoAutorizado, null, erro));
            }

            return Notifications.IsValid();
        }

        public bool EstaAutenticado()
        {
            Notifications.Clear();
            bool resultado = _autenticacao?.EstaAutenticado ?? false;

            if (!resultado)
            {
                Notifications.AddAlert(
                    AvisosResx.AcessoNaoAutenticado);
            }

            return resultado;
        }
    }
}
