using System.Reflection;
using System.Diagnostics;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Comum {
    public abstract class BaseAplicacao
        : Dominio.Servicos.Comum.BaseServico {
        public BaseAplicacao(
            AutenticacaoServ servAutenticacao) {
            _servAutenticacao = servAutenticacao;
        }

        protected readonly AutenticacaoServ _servAutenticacao;

        protected bool EhAutorizado() {
            var metodo = new StackTrace().GetFrame(1).GetMethod();
            return EhAutorizado(metodo);
        }

        protected bool EhAutorizado(MethodBase metodo) {
            Notifications.Clear();
            _servAutenticacao.EstaAutorizado(metodo, ValidationType.Unauthorized);
            return IsValid(_servAutenticacao);
        }
    }
}
