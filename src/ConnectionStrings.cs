namespace DotNetCore.API.Template
{
    public static class ConnectionStrings
    {
        public static void Inicializar(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private static IAppSettings _appSettings;

        public static string Teste => $"Application Name={AppSettings.Nome};{_appSettings.GetConnectionString("app:teste:{ambiente}")}";
    }
}
