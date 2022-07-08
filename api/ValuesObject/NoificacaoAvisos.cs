using System;
using BitHelp.Core.Validation;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Extensoes;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class NoificacaoAvisos
    {
        public NoificacaoAvisos(string mensagem, string referencia)
        {
            Id = Guid.NewGuid();
            Data = DateTime.Now;
            Mensagem = mensagem;
            Referencia = referencia ?? string.Empty;
            Tipo = TipoNoificacaoAvisos.Erro;
        }

        public NoificacaoAvisos(ValidationMessage dados)
        {
            Id = dados.Id;
            Data = dados.Date;
            Mensagem = dados.Message;
            Referencia = dados.Reference ?? string.Empty;
            Tipo = InterpretarTipo(dados.Type);
            Excecao = dados.Exception;
        }

        public Guid Id { get; set; }

        public DateTime Data { get; set; }

        public string Mensagem { get; set; }

        private string _referencia;
        [Display(Name = "Referência")]
        public string Referencia
        {
            get { return _referencia ??= string.Empty; }
            set { _referencia = (value ?? string.Empty).StartToLower(); }
        }

        public TipoNoificacaoAvisos Tipo { get; set; }

        [Display(Name = "Exceção")]
        public ExcecaoAvisos Excecao { get; set; }

        public bool EhValido()
        {
            return !(Tipo == TipoNoificacaoAvisos.Erro
                || Tipo == TipoNoificacaoAvisos.NaoAutorizado
                || Tipo == TipoNoificacaoAvisos.NaoEncontrado);
        }

        private TipoNoificacaoAvisos InterpretarTipo(ValidationType dados)
        {
            TipoNoificacaoAvisos resultado;
            switch (dados)
            {
                case ValidationType.Success:
                    resultado = TipoNoificacaoAvisos.Sucesso;
                    break;
                case ValidationType.Info:
                    resultado = TipoNoificacaoAvisos.Sucesso;
                    break;
                case ValidationType.Alert:
                    resultado = TipoNoificacaoAvisos.Atencao;
                    break;
                case ValidationType.Unauthorized:
                    resultado = TipoNoificacaoAvisos.NaoAutorizado;
                    break;
                case ValidationType.NotFound:
                    resultado = TipoNoificacaoAvisos.NaoEncontrado;
                    break;
                default:
                    resultado = TipoNoificacaoAvisos.Erro;
                    break;
            }

            return resultado;
        }
    }
}