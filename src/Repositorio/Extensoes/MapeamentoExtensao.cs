using System;
using System.Linq.Expressions;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Auxiliares;

namespace TemplateApi.Repositorio.Extensoes
{
    internal static class MapeamentoExtensao
    {
        public static string CharParaStatus<T, P>(
            this MapeamentoBase<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            string campo = source.Col(expression);

            return $@" CASE
                WHEN {campo} = '1' THEN '{Status.Ativo}'
                WHEN {campo} = '2' THEN '{Status.Excluido}'
                ELSE '{Status.Inativo}'
            END AS {source.Prop(expression)}";
        }

        public static string CharParaBoolean<T, P>(
            this MapeamentoBase<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            string campo = source.Col(expression);

            return $@" CASE
                WHEN {campo} = '1' THEN 'true'
                ELSE 'false'
            END AS {source.Prop(expression)}";
        }

        public static string BitParaBoolean<T, P>(
            this MapeamentoBase<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            string campo = source.Col(expression);

            return $@" CASE
                WHEN {campo} = '1' THEN 'true'
                ELSE 'false'
            END AS {source.Prop(expression)}";
        }
    }
}
