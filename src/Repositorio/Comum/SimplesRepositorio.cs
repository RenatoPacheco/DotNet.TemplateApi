using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Auxiliares;

namespace TemplateApi.Repositorio.Comum
{
    internal abstract class SimplesRepositorio 
        : BaseRepositorio
    {
        public SimplesRepositorio(
            Conexao conexao,
            IUnidadeTrabalho udt)
        {
            Conexao = conexao;
            _udt = udt;
        }

        private readonly IUnidadeTrabalho _udt;

        protected Conexao Conexao { get; private set; }

        public IniciarTransicao IniciarTransicao()
        {
            return new IniciarTransicao(_udt, this);
        }
    }
}
