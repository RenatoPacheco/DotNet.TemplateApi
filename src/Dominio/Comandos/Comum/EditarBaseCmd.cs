using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TemplateApi.Dominio.Comandos.Comum
{
    public abstract class EditarBaseCmd
    {
        protected IList<string> _camposRegistrados = new List<string>();

        protected string[] CamposRegistrados()
        {
            return _camposRegistrados.ToArray();
        }

        protected void RegistrarCampo([CallerMemberName] string campo = null)
        {
            if (!_camposRegistrados.Contains(campo))
            {
                _camposRegistrados.Add(campo);
            }
        }

        protected bool CampoFoiRegistrado(string campo)
        {
            return _camposRegistrados.Contains(campo);
        }


        protected void LimparCampoRegistrado(string campo = null)
        {
            if (campo is null)
            {
                _camposRegistrados.Clear();
            }
            else
            {
                _camposRegistrados = _camposRegistrados.Where(x => x != campo).ToList();
            }
        }
    }
}
