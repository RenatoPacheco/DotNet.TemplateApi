using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TemplateApi.Dominio.Comandos.Comum
{
    public abstract class EditarBaseCmd
    {
        protected IList<string> _propriedadesRegistrados = new List<string>();

        protected string[] PropriedadesRegistrados()
        {
            return _propriedadesRegistrados.ToArray();
        }

        protected void RegistrarPropriedade([CallerMemberName] string nome = null)
        {
            if (!_propriedadesRegistrados.Contains(nome))
            {
                _propriedadesRegistrados.Add(nome);
            }
        }

        protected bool PropriedadeRegistrada(string nome)
        {
            return _propriedadesRegistrados.Contains(nome);
        }


        protected void LimparPropriedadeRegistrada(string nome = null)
        {
            if (nome is null)
            {
                _propriedadesRegistrados.Clear();
            }
            else
            {
                _propriedadesRegistrados = _propriedadesRegistrados.Where(x => x != nome).ToList();
            }
        }
    }
}
