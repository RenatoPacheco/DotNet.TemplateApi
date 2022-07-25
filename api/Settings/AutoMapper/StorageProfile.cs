﻿using AutoMapper;
using System.Linq;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Api.DataModel.StorageDataModel;

namespace TemplateApi.Api.Settings.AutoMapper
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            #region InserirStorageCmd

            CreateMap<InserirStorageDataModel, InserirStorageCmd>()
                .ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region FiltrarStorageCmd

            CreateMap<FiltrarStorageDataModel, FiltrarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Storage?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Storage != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Storage);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Status?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Status != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Contexto, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Contexto.IsValid();

                        if ((src.Contexto != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Contexto);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region EditarStorageCmd

            CreateMap<EditarStorageDataModel, EditarStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Storage?.IsValid() ?? false;

                        if ((src.Storage != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Storage);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Status?.IsValid() ?? false;

                        if ((src.Status != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region ExcluirStorageCmd

            CreateMap<ExcluirStorageDataModel, ExcluirStorageCmd>()
                .ForMember(cmd => cmd.Storage, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Storage?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Storage != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Storage);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion

            #region ObterStorageCmd

            CreateMap<ObterStorageDataModel, ObterStorageCmd>()
                .ForMember(cmd => cmd.Download, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = src.Download?.IsValid() ?? false;

                        if ((src.Download != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Download);

                        return ehValido;
                    });
                }).ForMember(cmd => cmd.Status, opts => {
                    opts.Condition((src, dest, srcMember) => {
                        bool ehValido = !src.Status?.Any(x => !x.IsValid()) ?? false;

                        if ((src.Status != null) && !ehValido)
                            dest.AddErrorNotification(x => x.Status);

                        return ehValido;
                    });
                }).ForAllMembers(opts => {
                    opts.PreCondition((src, dest, srcMember) => srcMember != null);
                });

            #endregion
        }
    }
}
