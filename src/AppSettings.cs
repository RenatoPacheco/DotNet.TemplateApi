namespace TemplateApi
{
    public static class AppSettings
    {
        public static void Inicializar(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private static IAppSettings _appSettings;

        public static bool EhDesenvolvimento { get; set; }

        public static string Ambiente => EhDesenvolvimento ? "desenvolvimento" : "producao";

        public static string Nome => _appSettings.GetValue<string>("app:nome");

        public static string Versao => _appSettings.GetValue<string>("app:versao");

        public static string Autorizacao => _appSettings.GetValue<string>("app:autorizacao");

        public static string ChavePublica => _appSettings.GetValue<string>("app:chave-publica");

        public static string CultureInfo => _appSettings.GetValue<string>("app:culture-info");

        public static class Autor
        {
            public static string Nome => _appSettings.GetValue<string>("app:autor:nome");

            public static string Url => _appSettings.GetValue<string>("app:autor:url");

            public static string Email => _appSettings.GetValue<string>("app:autor:email");
        }
    }
}
