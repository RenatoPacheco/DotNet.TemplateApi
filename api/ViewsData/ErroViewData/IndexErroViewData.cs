using TemplateApi.Api.ValuesObject;
using BitHelp.Core.Validation;

namespace TemplateApi.Api.ViewsData.ErroViewData
{
    public class IndexErroViewData
        : ISelfValidation
    {
        public IndexErroViewData(int codigo)
        {
            _avisos = new Avisos(codigo);
        }

        private readonly Avisos _avisos;

        public string Titulo => $"{_avisos.Codigo} - {_avisos.Mensagem}";

        #region ISelfValidation

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        bool ISelfValidation.IsValid()
        {
            return _notifications.IsValid();
        }

        #endregion
    }
}
