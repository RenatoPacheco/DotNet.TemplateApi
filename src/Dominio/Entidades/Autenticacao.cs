using System;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Seguranca;
using DotNetCore.API.Template.Compartilhado.Json;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Entidades
{
    public class Autenticacao
    {
        public static Autenticacao DecodificarToken(string token)
        {
            Autenticacao resultado = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                string decodificado = Codificacao.Decriptar(token);
                if (!string.IsNullOrWhiteSpace(decodificado))
                {
                    resultado = ContratoJson.Desserializar<Autenticacao>(decodificado);
                    resultado.Token = token;
                }
            }

            return resultado;
        }

        public static Autenticacao GerarInterno(bool haChavePublica)
        {
            Autenticacao resultado = new Autenticacao
            {
                Id = Guid.Empty.ToString(),
                Nome = "Usuário interno",
                Interno = true,
                Autenticado = true,
                ExpiraEm = null,
                HaChavePublica = haChavePublica
                
            };

            resultado.AtualizarToken();

            return resultado;
        }

        public static Autenticacao GerarNaoAutenticado(bool haChavePublica)
        {
            Autenticacao resultado = new Autenticacao
            {
                Id = Guid.Empty.ToString(),
                Nome = "Usuário não autenticado",
                Interno = false,
                Autenticado = false,
                ExpiraEm = null,
                HaChavePublica = haChavePublica
            };

            resultado.AtualizarToken();

            return resultado;
        }

        protected internal Autenticacao()
        {
            CriadoEm = DateTime.Now;
            ExpiraEm = CriadoEm.Value.AddDays(1);
        }

        public Autenticacao(string id, string nome, string email)
            : this()
        {
            Id = id;
            Nome = nome;
            Email = email;
            Autenticado = true;
            AtualizarToken();
        }

        public string Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Token { get; set; }

        public bool Interno { get; set; }

        public bool Autenticado { get; set; }

        [Display(Name = "Há chave pública")]
        public bool HaChavePublica { get; set; }

        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        [Display(Name = "Expira em")]
        public DateTime? ExpiraEm { get; set; }

        private Autorizacao[] _autorizacoes = Array.Empty<Autorizacao>();

        [Display(Name = "Autorizações")]
        public Autorizacao[] Autorizacoes
        {
            get { return _autorizacoes; }
            set { _autorizacoes = value ?? Array.Empty<Autorizacao>(); }
        }

        protected void AtualizarToken()
        {
            Autorizacao[] autorizacoes = Autorizacoes;
            Autorizacoes = Array.Empty<Autorizacao>();
            Token = null;

            if (!Interno && Autenticado)
            {
                Token = ContratoJson.Serializar(this);
                Token = Codificacao.Encriptar(Token);
            }

            Autorizacoes = autorizacoes;
        }
    }
}