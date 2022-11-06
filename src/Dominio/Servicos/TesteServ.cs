using TemplateApi.Dominio.Comandos.TesteCmds;

namespace TemplateApi.Dominio.Servicos
{
    public class TesteServ : Comum.BaseServico
    {
        internal FormatosTesteCmd Formatos(FormatosTesteCmd comando)
        {
            if (IsValid(comando))
            {

            }

            return comando;
        }
    }
}
