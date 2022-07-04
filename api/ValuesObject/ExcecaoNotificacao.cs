using System;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class ExcecaoNotificacao
    {
        protected ExcecaoNotificacao() { }
        
        public ExcecaoNotificacao(Exception dados)
        {
            ClassName = dados.GetType().ToString();
            Message = dados.Message;
            HelpLink = dados.HelpLink;
            StackTrace = dados.StackTrace;
            Source = dados.Source;
            HResult = dados.HResult.ToString();
            Source = dados.Source;
        }

        public string ClassName { get; set; }

        public string Message { get; set; }

        public string HelpLink { get; set; }

        public string StackTrace { get; set; }

        public string HResult { get; set; }

        public string Source { get; set; }

        public static implicit operator ExcecaoNotificacao(Exception ex) => ex is null ? null : new ExcecaoNotificacao(ex);
    }
}
