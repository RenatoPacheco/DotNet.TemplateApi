using System;

namespace DotNetCore.API.Template
{
    public class DetectarServidor
    {
        private static string _servidor;
        /// <summary>
        /// Servidor atual, sendo produção por padrão
        /// </summary>
        public static string Servidor => _servidor ??= Detectar();

        /// <summary>
        /// Retorna true se o servidor atual for local
        /// </summary>
        public static bool EhLocal()
        {
            return Servidor == "local";
        }

        /// <summary>
        /// Retorna true se o servidor atual for de homologação
        /// </summary>
        public static bool EhHomologacao()
        {
            return Servidor == "homologacao";
        }

        /// <summary>
        /// Retorna true se o servidor atual for de produção
        /// </summary>
        public static bool EhProducao()
        {
            return Servidor == "producao";
        }

        private static string Detectar()
        {
            string host = AppDomain.CurrentDomain.BaseDirectory.ToLower();

            if (host.IndexOf(AppSettings.Ambiente.Local.ToLower()) >= 0)
                return "local";
            else if (host.IndexOf(AppSettings.Ambiente.Homologacao.ToLower()) >= 0)
                return "homologacao";
            else
                return "producao";
        }
    }
}