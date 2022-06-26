namespace DotNetCore.API.Template
{
    public static class ConnectionStrings
    {
        public static void Inicializar(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public static IAppSettings _appSettings;

        public static string Site => _appSettings.GetConnectionString("app:site:{ambiente}");
    }
}
