using AutoMapper;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Site.DataModel.UsuarioDataModel;

namespace DotNetCore.API.Template.Site.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<InserirUsuarioDataModel, InserirUsuarioCmd>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<FiltrarUsuarioDataModel, FiltrarUsuarioCmd>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EditarUsuarioDataModel, EditarUsuarioCmd>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ExcluirUsuarioDataModel, ExcluirUsuarioCmd>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
