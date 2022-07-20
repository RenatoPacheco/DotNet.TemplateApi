namespace TemplateApi
{
    public static class ConnectionStrings
    {
        public static void Inicializar(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private static IAppSettings _appSettings;

        public static string TemplateApi => $"Application Name={AppSettings.Nome};{_appSettings.GetConnectionString("app:template-api:{ambiente}")}";
    }
}
