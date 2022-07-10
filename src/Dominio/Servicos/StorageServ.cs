using System.Linq;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class StorageServ : Comum.BaseServ
    {
        public Arquivo[] Inserir(InserirStorageCmd comando)
        {
            Notifications.Clear();
            IList<Arquivo> resultado = new List<Arquivo>();

            if (Validate(comando))
            {
                foreach (Arquivo item in comando.Arquivo)
                {
                    item.Salvar();
                    resultado.Add(item);
                }
            }

            return resultado.ToArray();
        }
    }
}
