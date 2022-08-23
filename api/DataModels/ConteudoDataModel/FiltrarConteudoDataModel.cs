using System.Collections.Generic;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.ConteudoDataModel
{
    public class FiltrarConteudoDataModel 
        : Common.FiltrarBaseDataModel<FiltrarConteudoDataModel>
    {
        public override string Texto
        {
            get => base.Texto;
            set
            {
                base.Texto = value;
                RegistarPropriedade(x => x.Texto);
            }
        }

        public override int Maximo
        {
            get => base.Maximo;
            set
            {
                base.Maximo = value;
                RegistarPropriedade(x => x.Maximo);
            }
        }

        public override int Pagina
        {
            get => base.Pagina;
            set
            {
                base.Pagina = value;
                RegistarPropriedade(x => x.Pagina);
            }
        }

        public override bool CalcularPaginacao
        {
            get => base.CalcularPaginacao;
            set
            {
                base.CalcularPaginacao = value;
                RegistarPropriedade(x => x.CalcularPaginacao);
            }
        }

        private EnumInput<ContextoCmd> _contexto;
        /// <summary>
        /// Informe o contexto da busca, sendo que o valor padrão é Embutir
        /// </summary>
        public EnumInput<ContextoCmd> Contexto
        {
            get => _contexto;
            set
            {
                _contexto = value;
                RegistarPropriedade(x => x.Contexto);
            }
        }

        private IList<IntInput> _conteudo;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<IntInput> Conteudo
        {
            get => _conteudo;
            set
            {
                _conteudo = value;
                RegistarPropriedade(x => x.Conteudo);
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistarPropriedade(x => x.Status);
            }
        }
    }
}
