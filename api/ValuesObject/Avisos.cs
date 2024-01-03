using System;
using System.Linq;
using Newtonsoft.Json;
using TemplateApi.Recurso;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Api.ValuesObject
{
    public class Avisos
    {
        [JsonConstructor]
        protected Avisos() 
        {
            Data = DateTime.Now;
            Rastreio = Guid.NewGuid().ToString("N");
        }

        public Avisos(int codigo)
            : this()
        {
            Codigo = codigo;
            if (codigo >= 300)
            {
                TipoAvisos tipo = TipoAvisos.Erro;
                if (codigo < 500)
                {
                    if (codigo == 401)
                    {
                        tipo = TipoAvisos.NaoAutorizado;
                    }
                    else if (codigo == 404)
                    {
                        tipo = TipoAvisos.NaoEncontrado;
                    }
                }
                Notificacoes = new NotificacaoAvisos[] { 
                    new NotificacaoAvisos(Mensagem) { Tipo = tipo }
                };
            }
        }

        public Avisos(int codigo, ISelfValidation validacao)
            : this()
        {
            Codigo = codigo;
            Notificacoes = validacao.Notifications.Messages
                .Select(x => new NotificacaoAvisos(x)).ToArray();
        }

        public Avisos(int codigo, ValidationNotification notificacao)
            : this()
        {
            Codigo = codigo;
            Notificacoes = notificacao.Messages
                .Select(x => new NotificacaoAvisos(x)).ToArray();
        }

        public Avisos(Exception ex)
            : this(500)
        {
            Codigo = 500;
            Notificacoes = new NotificacaoAvisos[]
            {
                new NotificacaoAvisos(ex.Message, string.Empty) { Excecao = ex }
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
                    else if (_codigo == 404)
                    {
                        Mensagem = AvisosResx.Status404;
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

        public string Rastreio { get; set; }

        [Display(Name = "Notificações")]
        public NotificacaoAvisos[] Notificacoes { get; internal set; } = Array.Empty<NotificacaoAvisos>();

        public bool Validar(ISelfValidation dados)
        {
            bool resultado = dados.IsValid();
            Notificacoes = Notificacoes.Concat(dados.Notifications.Messages.Select(x => new NotificacaoAvisos(x))).ToArray();
            return resultado;
        }

        public bool EhValido()
        {
            return !Notificacoes.Any(x => !x.EhValido());
        }

        public void LimparErro()
        {
            Notificacoes = Notificacoes.Where(x => x.EhValido()).ToArray();
        }
    }
}
