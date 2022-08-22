using System;
using BitHelp.Core.Validation;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.Interfaces;
using Newtonsoft.Json;

namespace TemplateApi.Dominio.ObjetosDeValor
{
    public class Storage
        : IArquivo, ISelfValidation, IEquatable<Storage>
    {
        [JsonConstructor]
        protected Storage()
        {
            _escopo = new StorageEscp<Storage>(this);
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
            Status = ObjetosDeValor.Status.Inativo;
        }

        public Storage(IArquivo dados)
            : this()
        {
            Alias = dados.Alias;
            Nome = dados.Nome;
            Diretorio = dados.Diretorio;
            Extensao = dados.Extensao;
            Tipo = dados.Tipo;
            Checksum = dados.Checksum;
            Referencia = dados.Referencia;
            Peso = dados.Peso;
            Status = ObjetosDeValor.Status.Ativo;
        }

        public long? Id { get; set; }

        public string Nome { get; set; }

        public string Alias { get; set; }

        [Display(Name = "Diretório")]
        public string Diretorio { get; set; }

        [Display(Name = "Extensão")]
        public string Extensao { get; set; }

        public string Checksum { get; set; }

        public string Tipo { get; set; }

        public long Peso { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        [Display(Name = "AlteradoEm")]
        public DateTime? AlteradoEm { get; set; }

        public Status? Status { get; set; }

        public override string ToString() => Nome;

        #region Compare

        public bool Equals([AllowNull] Storage other)
        {
            return !(other is null)
                && other.GetHashCode() == GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Storage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}:{Id}".GetHashCode();
        }

        #endregion

        #region Operadores

        public static bool operator ==(Storage a, Storage b)
        {
            return (a is null) && (b is null)
                || (!(a is null) && !(b is null) && a.Equals(b));
        }

        public static bool operator !=(Storage a, Storage b)
        {
            return !((a is null) && (b is null)
                || (!(a is null) && !(b is null) && a.Equals(b)));
        }

        #endregion

        #region ISelfValidation

        protected readonly StorageEscp<Storage> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public bool IsValid()
        {
            _escopo.IdEhValido(x => x.Id);
            _escopo.NomeEhValido(x => x.Nome);
            _escopo.AliasEhValido(x => x.Alias);
            _escopo.DiretorioEhValido(x => x.Diretorio);
            _escopo.ExtensaoEhValido(x => x.Extensao);
            _escopo.TipoEhValido(x => x.Tipo);
            _escopo.PesoEhValido(x => x.Peso);
            _escopo.ReferenciaEhValido(x => x.Referencia);
            _escopo.StatusEhValido(x => x.Status);

            return _notifications.IsValid();
        }

        #endregion
    }
}
