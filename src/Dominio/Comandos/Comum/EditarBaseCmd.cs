using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TemplateApi.Dominio.Comandos.Comum
{
    public abstract class EditarBaseCmd
    {
        protected IList<string> _propriedadesRegistradas = new List<string>();

        protected string[] PropriedadesRegistradas()
        {
            return _propriedadesRegistradas.ToArray();
        }

        protected void RegistrarPropriedade([CallerMemberName] string nome = null)
        {
            if (!_propriedadesRegistradas.Contains(nome))
            {
                _propriedadesRegistradas.Add(nome);
            }
        }

        protected bool PropriedadeRegistrada(string nome)
        {
            return _propriedadesRegistradas.Contains(nome);
        }


        protected void LimparPropriedadeRegistrada(string nome = null)
        {
            if (nome is null)
            {
                _propriedadesRegistradas.Clear();
            }
            else
            {
                _propriedadesRegistradas = _propriedadesRegistradas.Where(x => x != nome).ToList();
            }
        }
    }
}
