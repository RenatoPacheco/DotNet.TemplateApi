using System;
using System.Linq;
using System.Reflection;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using DotNetCore.API.Template.Recurso;
using DotNetCore.API.Template.Aplicacao;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Repositorio.Persistencias.AutorizacaoPers
{
    internal class ListarAutorizacaoPers : Comum.BaseRep
    {
        private static Autorizacao[] _dadosBase;

        private Autorizacao[] _opcoes = null;
        private Autorizacao[] Opcoes
        {
            get
            {
                if (_opcoes is null || !_opcoes.Any())
                {
                    _opcoes = Sincronizar();
                }
                return _opcoes;
            }
        }

        private Autorizacao[] Sincronizar()
        {
            if (_dadosBase is null || !_dadosBase.Any())
            {
                string referencia = typeof(SobreApp).Namespace;
                Autorizacao[] resultado = Array.Empty<Autorizacao>();
                IEnumerable<Autorizacao> autorizacoes = new List<Autorizacao>();
                Type[] classes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(
                    t => t.ReflectedType is null
                    && !(t.Namespace is null)
                    && t.IsPublic
                    && t.IsClass
                    && !t.IsAbstract
                    && !t.IsInterface
                    && t.Namespace == referencia).ToArray();

                foreach (Type classe in classes)
                {

                    autorizacoes = classe.GetMethods().Where(
                        metodo => !(metodo is null)
                        && metodo.IsPublic
                        && metodo.DeclaringType == metodo.ReflectedType
                        && !metodo.IsStatic
                        && !metodo.IsSpecialName
                        ).ToArray().Select(metodo => new Autorizacao(metodo));
                    // Sem aplicar o ToArray() antes de selecionar a lista, ele estava ignorando o filtro where

                    resultado = resultado.Concat(autorizacoes).ToArray();
                }

                _dadosBase = resultado;
            }

            return new List<Autorizacao>().Concat(_dadosBase).ToArray();
        }

        public Autorizacao[] Listar()
        {
            return Listar(string.Empty, ValidationType.Alert);
        }

        public Autorizacao[] Listar(string referencia)
        {
            return Listar(referencia, ValidationType.Alert);
        }

        public Autorizacao[] Listar(ValidationType tipo)
        {
            return Listar(string.Empty, tipo);
        }

        public Autorizacao[] Listar(string referencia, ValidationType tipo)
        {
            Notifications.Clear();
            Autorizacao[] resultado = Opcoes;

            if (!resultado.Any())
            {
                Notifications.Add(new ValidationMessage(
                    string.Format(AvisosResx.XNaoEncontrados, NomesResx.Autorizacoes), referencia, tipo));
            }

            return resultado;
        }
    }
}
