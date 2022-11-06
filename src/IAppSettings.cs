namespace TemplateApi
{
    public interface IAppSettings
    {
        T GetValue<T>(string keys);

        string GetConnectionString(string keys);
    }
}
