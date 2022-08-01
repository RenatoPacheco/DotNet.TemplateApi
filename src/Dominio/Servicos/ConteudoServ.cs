using System;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Dominio.Servicos
{
    public class ConteudoServ : Comum.BaseServico
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

            if (IsValid(comando))
            {
                resultado = _repConteudo.Filtrar(comando);
                IsValid(_repConteudo);
            }

            return resultado;
        }

        public Conteudo Inserir(InserirConteudoCmd comando)
        {
            Notifications.Clear();
            Conteudo resultado = null;

            if (IsValid(comando))
            {
                comando.Aplicar(ref resultado);
                _repConteudo.Inserir(resultado);
                IsValid(_repConteudo);
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public Conteudo Editar(EditarConteudoCmd comando)
        {
            Notifications.Clear();
            Conteudo resultado = null;

            if (IsValid(comando))
            {
                resultado = _repConteudo.Filtrar(new FiltrarConteudoCmd {
                    Conteudo = new int[] { comando.Conteudo.Value },
                    Contexto = ContextoCmd.Editar,
                    Maximo = 1, 
                    Pagina = 1
                }, nameof(comando.Conteudo), ValidationType.Error).FirstOrDefault();
                IsValid(_repConteudo);

                if (IsValid())
                {
                    comando.Aplicar(ref resultado);
                    _repConteudo.Editar(resultado);
                    IsValid(_repConteudo);
                }
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();

            if (IsValid(comando))
            {
                _repConteudo.Excluir(comando);
                IsValid(_repConteudo);
            }
        }
    }
}
