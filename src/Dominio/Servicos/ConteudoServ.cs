using System;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class ConteudoServ : Comum.BaseServ
    {
        public ConteudoServ(
            IConteudoRep repConteudo)
        {
            _repConteudo = repConteudo;
        }

        protected readonly IConteudoRep _repConteudo;

        public ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Conteudo> resultado = new ResultadoBusca<Conteudo>();

            if (Validate(comando))
            {
                resultado = _repConteudo.Filtrar(comando);
                Validate(_repConteudo);
            }

            return resultado;
        }

        public Conteudo Inserir(InserirConteudoCmd comando)
        {
            Notifications.Clear();
            Conteudo resultado = null;

            if (Validate(comando))
            {
                comando.Aplicar(ref resultado);
                _repConteudo.Inserir(resultado);
                Validate(_repConteudo);
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public Conteudo Editar(EditarConteudoCmd comando)
        {
            Notifications.Clear();
            Conteudo resultado = null;

            if (Validate(comando))
            {
                resultado = (Conteudo)_repConteudo.Filtrar(new FiltrarConteudoCmd {
                    Conteudo = new int[] { comando.Conteudo.Value },
                    Maximo = 1, Pagina = 1
                }, nameof(comando.Conteudo), ValidationType.Error);
                Validate(_repConteudo);

                if (IsValid())
                {
                    comando.Aplicar(ref resultado);
                    _repConteudo.Editar(resultado);
                    Validate(_repConteudo);
                }
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();

            if (Validate(comando))
            {
                _repConteudo.Excluir(comando);
                Validate(_repConteudo);
            }
        }
    }
}
