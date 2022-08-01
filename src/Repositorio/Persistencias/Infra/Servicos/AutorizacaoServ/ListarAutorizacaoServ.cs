using System;
using System.Linq;
using System.Reflection;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.RecursoResx;
using TemplateApi.Aplicacao;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Persistencias.Infra.Servicos.AutorizacaoServ
{
    internal class ListarAutorizacaoServ 
        : Comum.BaseRepositorio
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
                        ).ToArray().Select(metodo => new Autorizacao(metodo, classe));
                    // Sem aplicar o ToArray() antes de selecionar a lista, ele estava ignorando o filtro where

                    resultado = resultado.Concat(autorizacoes).ToArray();
                }

                _dadosBase = resultado;
            }

            return new List<Autorizacao>().Concat(_dadosBase).ToArray();
        }

        public Autorizacao[] Executar()
        {
            return Executar(string.Empty, ValidationType.Alert);
        }

        public Autorizacao[] Executar(string referencia)
        {
            return Executar(referencia, ValidationType.Alert);
        }

        public Autorizacao[] Executar(ValidationType tipo)
        {
            return Executar(string.Empty, tipo);
        }

        public Autorizacao[] Executar(string referencia, ValidationType tipo)
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
