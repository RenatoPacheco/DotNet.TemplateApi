using System;
using BitHelp.Core.Validation;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio.Auxiliares
{
    public class IniciarTransicao 
        : IDisposable
    {
        public IniciarTransicao(
            IUnidadeTrabalho udt, 
            ISelfValidation referencia)
        {
            _referencia = referencia;
            if (udt.PossoIniciar())
            {
                _transicao = udt.Requisitar();
                _transicao.Iniciar();
            }
        }

        private readonly ITransicao _transicao;
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
