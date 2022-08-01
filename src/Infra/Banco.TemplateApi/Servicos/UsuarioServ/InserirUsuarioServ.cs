using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Banco.TemplateApi.Servicos.UsuarioServ
{
    internal class InserirUsuarioServ
        : BaseSimplesServico
    {
        public InserirUsuarioServ(
            Conexao conexao,
            EhUnicoUsuarioServ persEhUnicoUsuario)
            : base(conexao)
        {
            _persEhUnicoUsuario = persEhUnicoUsuario;
        }

        private readonly EhUnicoUsuarioServ _persEhUnicoUsuario;

        public void Executar(Usuario dados)
        {
            Notifications.Clear();
            Mapeamentos.UsuarioMap map = new Mapeamentos.UsuarioMap();

            IsValid(dados);

            _persEhUnicoUsuario.Executar(dados);
            IsValid(_persEhUnicoUsuario);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO {map.Tabela}
                           ({map.Col(x => x.Nome)}
                           ,{map.Col(x => x.Email)}
                           ,{map.Col(x => x.Senha)}
                           ,{map.Col(x => x.Telefone)}
                           ,{map.Col(x => x.CriadoEm)}
                           ,{map.Col(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)})
                    VALUES
                           (@Nome
                           ,@Email
                           ,@Senha
                           ,@Telefone
                           ,@CriadoEm
                           ,@AlteradoEm
                           ,@Status); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                object sqlObject = new
                {
                    Nome = dados.Nome,
                    Email = dados.Email,
                    Senha = dados.Senha,
                    Telefone = dados.Telefone,
                    Status = StatusAdapt.EnumParaSql(dados.Status),
                    CriadoEm = dados.CriadoEm,
                    AlteradoEm = dados.AlteradoEm
                };

                dados.Id = Conexao.Sessao.QuerySingle<int>(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
