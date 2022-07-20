using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Aplicacao.Intreceptadores;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Aplicacao
{
    public class ConteudoApp : Comum.BaseAplicacao
    {
        public ConteudoApp(
            AutenticacaoServ servAutenticacao,
            ConteudoServ servConteudo,
            ConteudoInter interConteudo)
            : base(servAutenticacao)
        {
            _servConteudo = servConteudo;
            _interConteudo = interConteudo;
        }

        protected readonly ConteudoServ _servConteudo;
        protected readonly ConteudoInter _interConteudo;

        /// <summary>
        /// Permite filtrar os conteúdos.
        /// </summary>
        [Display(Name = "Filtrar conteúdo")]
        [Description("Permite filtrar os conteúdos.")]
        public ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Conteudo> resultado = new ResultadoBusca<Conteudo>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interConteudo.Filtrar(comando);
                resultado = _servConteudo.Filtrar(comando);
                IsValid(_servConteudo);
            }

            return resultado;
        }

        /// <summary>
        /// Permite inserir um novo conteúdo.
        /// </summary>
        [Display(Name = "Inserir conteúdo")]
        [Description("Permite inserir um novo conteúdo.")]
        public Conteudo Inserir(InserirConteudoCmd comando)
        {
            Notifications.Clear();
            Conteudo resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interConteudo.Inserir(comando);
                resultado = _servConteudo.Inserir(comando);
                IsValid(_servConteudo);
            }

            return resultado;
        }

        /// <summary>
        /// Permite editar um conteúdo específico.
        /// </summary>
        [Display(Name = "Editar conteúdo")]
        [Description("Permite editar um conteúdo específico.")]
        public Conteudo Editar(EditarConteudoCmd comando)
        {
            Notifications.Clear();
            Conteudo resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interConteudo.Editar(comando);
                resultado = _servConteudo.Editar(comando);
                IsValid(_servConteudo);
            }

            return resultado;
        }

        /// <summary>
        /// Permite excluir um ou mais conteúdos.
        /// </summary>
        [Display(Name = "Excluir conteúdo")]
        [Description("Permite excluir um ou mais conteúdos.")]
        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interConteudo.Excluir(comando);
                _servConteudo.Excluir(comando);
                IsValid(_servConteudo);
            }
        }
    }
}
