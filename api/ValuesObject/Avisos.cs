using System;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Recurso;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class Avisos
    {
        public Avisos(int codigo)
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

        public Avisos(int codigo, ISelfValidation validacao)
            : this(codigo)
        {
            Notificacoes = validacao.Notifications.Messages
                .Select(x => new NoificacaoAvisos(x)).ToArray();
        }

        public Avisos(int codigo, ValidationNotification notificacao)
            : this(codigo)
        {
            Notificacoes = notificacao.Messages
                .Select(x => new NoificacaoAvisos(x)).ToArray();
        }

        public Avisos(Exception ex)
            : this(500)
        {
            Notificacoes = new NoificacaoAvisos[]
            {
                new NoificacaoAvisos(ex.Message, string.Empty) { Excecao = ex }
            };
        }

        public string Mensagem { get; private set; }

        private int _codigo;
        public int Codigo
        {
            get => _codigo;
            set
            {
                _codigo = value;
                if (_codigo < 300)
                {
                    Mensagem = AvisosResx.Status200;
                }
                else if (_codigo < 500)
                {
                    if (_codigo == 401)
                    {
                        Mensagem = AvisosResx.Status401;
                    }
                    else
                    {
                        Mensagem = AvisosResx.Status400;
                    }
                }
                else
                {
                    Mensagem = AvisosResx.Status500;
                }
            }
        }

        public DateTime Data { get; set; }

        public Guid Rastreio { get; set; }

        [Display(Name = "Notificações")]
        public IList<NoificacaoAvisos> Notificacoes { get; internal set; } = new List<NoificacaoAvisos>();

        public bool Validar(ISelfValidation dados)
        {
            bool resultado = dados.IsValid();
            Notificacoes = Notificacoes.Concat(dados.Notifications.Messages.Select(x => new NoificacaoAvisos(x))).ToList();
            return resultado;
        }

        public bool EhValido()
        {
            return !Notificacoes.Any(x => !x.EhValido());
        }

        public void LimparErro()
        {
            Notificacoes = Notificacoes.Where(x => x.EhValido()).ToList();
        }

        public void Erro(string mensagem)
        {
            Notificacoes.Add(new NoificacaoAvisos(mensagem, string.Empty));
        }
    }
}
