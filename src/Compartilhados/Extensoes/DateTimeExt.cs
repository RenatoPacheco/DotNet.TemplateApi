namespace TemplateApi.Compartilhados.Extensoes {

    public static class DateTimeExt {

        public static string ToSql(DateTime? data) {
            return data?.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

    }
}
