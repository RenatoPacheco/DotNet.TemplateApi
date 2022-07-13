using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using System.Collections.Generic;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class StorageInter : Comum.BaseInter
    {
        public StorageInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        public void Obter(ObterStorageCmd comando)
        {
            if (!_servAutenticacao.EstaAutenticado())
            {
                if (!comando.HasNotification(x => x.Status))
                {
                    comando.Status = new List<EnumInput<Status>>() { (EnumInput<Status>)Status.Ativo };
                }
            }
        }

        public void Filtrar(FiltrarStorageCmd comando)
        {
            if (!_servAutenticacao.EstaAutenticado())
            {
                if (!comando.HasNotification(x => x.Status))
                {
                    comando.Status = new List<EnumInput<Status>>() { (EnumInput<Status>)Status.Ativo };
                }
            }
        }
    }
}
