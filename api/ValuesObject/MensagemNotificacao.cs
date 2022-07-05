using System;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.Extensoes;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class MensagemNotificacao
    {
        protected MensagemNotificacao() { }

        public MensagemNotificacao(ValidationMessage dados)
        {
            Id = dados.Id;
            Referencia = dados.Reference ?? string.Empty;
            Mensagem = dados.Message ?? string.Empty;
            Tipo = InterpretarTipo(dados.Type);
            Data = dados.Date;
            Excecao = dados.Exception;
        }

        public MensagemNotificacao(Exception dados)
        {
            Id = Guid.NewGuid();
            Referencia = string.Empty;
            Mensagem = dados.Message;
            Data = DateTime.Now;
            Excecao = dados;
        }

        public Guid Id { get; set; }

        private string _referencia;

        public string Referencia
        {
            get { return _referencia ??= string.Empty; }
            set { _referencia = (value ?? string.Empty).StartToLower(); }
        }


        public string Mensagem { get; set; }

        public string Tipo { get; set; }

        public DateTime Data { get; set; }

        public ExcecaoNotificacao Excecao { get; set; }

        private string InterpretarTipo(ValidationType dados)
        {
            string resultado;
            switch (dados)
            {
                case ValidationType.Success:
                    resultado = "Sucesso";
                    break;
                case ValidationType.Info:
                    resultado = "Informacao";
                    break;
                case ValidationType.Alert:
                    resultado = "Atencao";
                    break;
                case ValidationType.Unauthorized:
                    resultado = "NaoAutorizado";
                    break;
                default:
                    resultado = "Erro";
                    break;
            }

            return resultado;
        }
    }
}
