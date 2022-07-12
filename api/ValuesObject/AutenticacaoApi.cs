using DotNetCore.API.Template.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.API.Template.Site.ValuesObject
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

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Está autenticado")]
        public bool EstaAutenticado { get; set; }

        [Display(Name = "Há chave pública")]
        public bool HaChavePublica { get; set; }

        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        [Display(Name = "Expira em")]
        public DateTime? ExpiraEm { get; set; }

        public string Token { get; set; }

        [Display(Name = "Autorizações")]
        public AutorizacaoApi[] Autorizacoes { get; set; } = Array.Empty<AutorizacaoApi>();
    }
}
