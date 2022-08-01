using System;

namespace TemplateApi.Repositorio.Interfaces
{
    public interface ITransicao 
        : IDisposable
    {
        /// <summary>
        /// Iniciar uma transição
        /// </summary>
        void Iniciar();

        /// <summary>
        /// Salvar a trasição atual
        /// </summary>
        void Salvar();

        /// <summary>
        /// Desfazer a trasição atual
        /// </summary>
        void Desfazer();
    }
}