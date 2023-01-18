using AutoMapper;
using System.Linq;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Dominio.Comandos.UploadCmds;
using TemplateApi.Api.DataModels.UploadDataModel;
using System.Collections.Generic;
using TemplateApi.Api.ApiServices;

namespace TemplateApi.Api.App_Start.AutoMappers
{
    public class UploadProfile : Profile
    {
        private readonly RequestApiServ _apiServRequest;

        public UploadProfile(RequestApiServ apiServRequest)
        {
            _apiServRequest = apiServRequest;

            #region ArquivoUploadCmd

            CreateMap<ArquivoUploadDataModel, ArquivoUploadCmd>()
                .ForMember(cmd => cmd.Arquivo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Arquivo));
                    opts.MapFrom(src => src.Arquivo == null ? null : src.Arquivo.Select(x => new StoragePublico(x, _apiServRequest)));
                });

            #endregion

            #region ImagemUploadCmd

            CreateMap<ImagemUploadDataModel, ImagemUploadCmd>()
                .ForMember(cmd => cmd.Arquivo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Arquivo));
                    opts.MapFrom(src => src.Arquivo == null ? null : src.Arquivo.Select(x => new StoragePublico(x, _apiServRequest)));
                });

            #endregion


            #region ArquivoUploadCmd

            CreateMap<ArquivoCKEditorV4UploadDataModel, ArquivoUploadCmd>()
                .ForMember(cmd => cmd.Arquivo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Arquivo));
                    opts.MapFrom(src => src.Arquivo == null ? null : new List<StoragePublico> { new StoragePublico(src.Arquivo, _apiServRequest) });
                });

            #endregion

            #region ImagemUploadCmd

            CreateMap<ImagemCKEditorV4UploadDataModel, ImagemUploadCmd>()
                .ForMember(cmd => cmd.Arquivo, opts =>
                {
                    opts.Condition((src, dest, srcMember)
                        => src.PropriedadeRegistrada(x => x.Arquivo));
                    opts.MapFrom(src => src.Arquivo == null ? null : new List<StoragePublico> { new StoragePublico(src.Arquivo, _apiServRequest) });
                });

            #endregion
        }
    }
}
