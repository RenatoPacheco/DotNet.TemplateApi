using TemplateApi.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateApi.Api.ValuesObject
{
    public class AutenticacaoApi
    {
        public AutenticacaoApi() { }

        public AutenticacaoApi(Autenticacao dados)
        {
            Nome = dados.Nome;
            Email = dados.Email;
            CriadoEm = dados.CriadoEm;
            ExpiraEm = dados.ExpiraEm;
            Token = dados.Token;
            EstaAutenticado = dados.EstaAutenticado;
            HaChavePublica = dados.HaChavePublica;
        }

        /// <summary>
        /// Informa o nome de quem se autenticou.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Informa o e-mail de quem se autenicou.
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Informa se a autenticação recebida foi validade ou não.
        /// </summary>
        [Display(Name = "Está autenticado")]
        public bool EstaAutenticado { get; set; }

        /// <summary>
        /// Informa se uma chave de acessó válida foi enviada ou não.
        /// </summary>
        [Display(Name = "Há chave pública")]
        public bool HaChavePublica { get; set; }

        /// <summary>
        /// Data em que a autenticação foi criada.
        /// </summary>
        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        /// <summary>
        /// Data que o token dessa autenticação vai expirar.
        /// </summary>
        [Display(Name = "Expira em")]
        public DateTime? ExpiraEm { get; set; }

        /// <summary>
        /// Token gerado a partir dos dados da autenticação, deve ser usado no header de autorização.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Lista de autorização que essa autenticação tem acesso.
        /// </summary>
        [Display(Name = "Autorizações")]
        public AutorizacaoApi[] Autorizacoes { get; set; } = Array.Empty<AutorizacaoApi>();
    }
}
