using System.Net;
using System.Text;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Dominio.Auxiliares;

namespace DotNetCore.API.Template.Site.Helpers
{
    public static class MontarResultado
    {
        public static string Json(HttpStatusCode codigo, ValidationNotification notificacoes)
        {
            Notificacao avisos = new Notificacao((int)codigo, notificacoes);
            StringBuilder resultado = new StringBuilder();

            resultado.Append("{");
            resultado.Append($"\"avisos\": {ContratoJson.Serializar(avisos)},");
            resultado.Append($"\"dados\": null");
            resultado.Append("}");

            return resultado.ToString();
        }
        public static string Json(HttpStatusCode codigo, ValidationNotification notificacoes, string dados)
        {
            Notificacao avisos = new Notificacao((int)codigo, notificacoes);
            StringBuilder resultado = new StringBuilder();

            resultado.Append("{");
            resultado.Append($"\"avisos\": {ContratoJson.Serializar(avisos)},");
            resultado.Append($"\"dados\": {dados}");
            resultado.Append("}");

            return resultado.ToString();
        }

        public static string Json(HttpStatusCode codigo, ValidationNotification notificacoes, object dados)
        {
            Notificacao avisos = new Notificacao((int)codigo, notificacoes);
            StringBuilder resultado = new StringBuilder();

            resultado.Append("{");
            resultado.Append($"\"avisos\": {ContratoJson.Serializar(avisos)},");
            resultado.Append($"\"dados\": {ContratoJson.Serializar(dados)}");
            resultado.Append("}");

            return resultado.ToString();
        }
    }
}
