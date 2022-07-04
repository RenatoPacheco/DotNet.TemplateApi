using System;
using System.Linq;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Recurso;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class Notificacao
    {
        public Notificacao(int codigo)
        {
            if (codigo < 300)
            {
                Mensagem = AvisosResx.SolicitacaoSucesso;
            }
            else if (codigo < 500)
            {
                if (codigo == 401)
                {
                    Mensagem = AvisosResx.SolicitacaoNaoAutorizada;
                }
                else
                {
                    Mensagem = AvisosResx.SolicitacaoErro;
                }
            }
            else
            {
                Mensagem = AvisosResx.OcorreuUmErroInterno;
            }
            Codigo = codigo;
        }

        public Notificacao(int codigo, ISelfValidation validacao)
            : this(codigo)
        {
            Notificacoes = validacao.Notifications.Messages
                .Select(x => new MensagemNotificacao(x)).ToArray();
        }

        public Notificacao(int codigo, ValidationNotification notificacao)
            : this(codigo)
        {
            Notificacoes = notificacao.Messages
                .Select(x => new MensagemNotificacao(x)).ToArray();
        }

        public string Mensagem { get; set; }

        [Display(Name = "Código")]
        public int Codigo { get; set; }

        public Guid Rastreio { get; set; } = Guid.Empty;

        public DateTime Data { get; set; } = DateTime.Now;

        [Display(Name = "Notificações")]
        public MensagemNotificacao[] Notificacoes { get; set; } = Array.Empty<MensagemNotificacao>();
    }
}
