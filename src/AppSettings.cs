﻿namespace DotNetCore.API.Template
{
    public static class AppSettings
    {
        public static void Inicializar(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public static IAppSettings _appSettings;

        public static class Ambiente
        {
            public static string Local => _appSettings.GetValue<string>("app:ambiente:local");

            public static string Homologacao => _appSettings.GetValue<string>("app:ambiente:homologacao");
        };

        public static string Nome => _appSettings.GetValue<string>("app:nome");

        public static string Versao => _appSettings.GetValue<string>("app:versao");

        public static string Autorizacao => _appSettings.GetValue<string>("app:autorizacao");

        public static string ChavePublica => _appSettings.GetValue<string>("app:chave-publica");

        public static string CultureInfo => _appSettings.GetValue<string>("app:culture-info");
    }
}
