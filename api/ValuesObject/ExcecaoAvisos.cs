using System;

namespace TemplateApi.Api.ValuesObject
{
    public class ExcecaoAvisos
    {
        public ExcecaoAvisos(Exception ex)
        {
            ClassName = ex.GetType().ToString();
            Message = ex.Message;
            HelpLink = ex.HelpLink;
            StackTrace = ex.StackTrace;
            Source = ex.Source;
            HResult = ex.HResult.ToString();
        }

        public string ClassName { get; set; }

        public string Message { get; set; }

        public string HelpLink { get; set; }

        public string StackTrace { get; set; }

        public string HResult { get; set; }

        public string Source { get; set; }

        public static implicit operator ExcecaoAvisos(Exception ex) => ex is null ? null : new ExcecaoAvisos(ex);
    }
}