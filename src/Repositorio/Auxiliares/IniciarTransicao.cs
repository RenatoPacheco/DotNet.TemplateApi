using System;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;

namespace DotNetCore.API.Template.Repositorio.Auxiliares
{
    public class IniciarTransicao : IDisposable
    {
        public IniciarTransicao(IUnidadeTrabalho udt, ISelfValidation referencia)
        {
            _referencia = referencia;
            if ((udt as UnidadeTrabalho).DevoInicializar())
            {
                _transicao = udt.Requisitar() as Transicao;
                _transicao.Inicializar();
            }
        }

        private readonly Transicao _transicao;
        private readonly ISelfValidation _referencia;

        public void Dispose()
        {
            if (!(_transicao is null))
            {
                if (_referencia.IsValid())
                {
                    _transicao.Salvar();
                }
                else
                {
                    _transicao.Desfazer();
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
