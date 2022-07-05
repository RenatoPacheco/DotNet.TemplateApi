﻿using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class EditarUsuarioCmd 
        : Comum.EditarBaseCmd, ISelfValidation
    {
        private int? _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public int? Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                RegistrarCampo(nameof(Usuario));
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Nome
        {
            get => _nome;
            set 
            { 
                _nome = value;
                RegistrarCampo(nameof(Nome));
            }
        }

        private string _email;
        /// <summary>
        /// E-mail de usuário
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RegistrarCampo(nameof(Email));
            }
        }

        private Status? _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status? Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistrarCampo(nameof(Status));
            }
        }

        public void Aplicar(ref Usuario dados)
        {
            if (CampoFoiRegistrado(nameof(Nome)))
            {
                dados.Nome = Nome;
            }

            if (CampoFoiRegistrado(nameof(Email)))
            {
                dados.Email = Email;
            }

            if (CampoFoiRegistrado(nameof(Status)))
            {
                dados.Status = Status;
            }
        }

        public void Desfazer(ref Usuario dados) => dados = null;

        #region Auto validação

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Usuario);
            return Notifications.IsValid();
        }

        #endregion
    }
}
