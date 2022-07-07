using AutoMapper;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Site.DataModel.UsuarioDataModel;

namespace DotNetCore.API.Template.Site.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>();
            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>();
        }
    }
}
