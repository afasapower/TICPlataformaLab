using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Usuario_EmpresaRepository : RepositoryBase<Usuario_Empresa>, IUsuario_EmpresaRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Usuario_EmpresaRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registros da tabela
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_usuario, Guid id_empresa)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario_Empresa in db.Usuario_Empresa
                              where (_Usuario_Empresa.id_usuario == id_usuario)
                                && ((id_empresa == Guid.Empty) || (_Usuario_Empresa.id_pessoa_empresa == id_empresa))
                                && _Usuario_Empresa.excluido == false
                              select _Usuario_Empresa.id).ToList().Count();

                return lQuery;
            }
        }

        /// <summary>
        /// Lista os dados com as empresas que o usuário pode acessar
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Usuario_Empresa> GetList(Guid id_usuario, Guid id_empresa, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario_Empresa in db.Usuario_Empresa
                              join _Usuario in db.Usuario on _Usuario_Empresa.id_usuario equals _Usuario.id
                              join _Pessoa in db.Pessoa on _Usuario_Empresa.id_pessoa_empresa equals _Pessoa.id
                              where _Usuario_Empresa.id_usuario == id_usuario
                                && ((id_empresa == Guid.Empty) || (_Usuario_Empresa.id_pessoa_empresa == id_empresa))
                                && _Usuario_Empresa.excluido == false
                              select new
                              {
                                  // Dados Usuário Empresa
                                  id = _Usuario_Empresa.id,
                                  id_usuario = _Usuario_Empresa.id_usuario,
                                  id_pessoa_empresa = _Usuario_Empresa.id_pessoa_empresa,
                                  data_inclusao = _Usuario_Empresa.data_inclusao,
                                  usuario = _Usuario_Empresa.usuario,
                                  // Dados Usuário
                                  login = _Usuario.login,
                                  // Dados Empresa
                                  id_pessoa = _Pessoa.id,
                                  razao_social_nome = _Pessoa.razao_social_nome,
                                  nome_fantasia_apelido = _Pessoa.nome_fantasia_apelido,
                                  cnpj_cpf = _Pessoa.cnpj_cpf
                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                List<Usuario_Empresa> usuario_empresa = new List<Usuario_Empresa>();

                foreach (var item in lQuery)
                {
                    Usuario_Empresa _Usuario_Empresa = new Usuario_Empresa();

                    _Usuario_Empresa.id = item.id;
                    _Usuario_Empresa.id_usuario = item.id_usuario;
                    _Usuario_Empresa.id_pessoa_empresa = item.id_pessoa_empresa;
                    _Usuario_Empresa.data_inclusao = item.data_inclusao;
                    _Usuario_Empresa.usuario = item.usuario;

                    // Dados de Pessoa
                    /*
                    _Usuario_Empresa.Usuario = new Usuario()
                    {
                        id = item.id_pessoa,
                        login = item.login
                    };
                    */

                    // Dados de Pessoa
                    _Usuario_Empresa.Pessoa_Empresa = new Pessoa()
                    {
                        id = item.id_pessoa,
                        razao_social_nome = item.razao_social_nome,
                        nome_fantasia_apelido = item.nome_fantasia_apelido,
                        cnpj_cpf = item.cnpj_cpf,
                    };

                    usuario_empresa.Add(_Usuario_Empresa);
                }

                return usuario_empresa;
            }
        }

        /// <summary>
        /// Remove todas as permissões da empresa antiga
        /// </summary>
        /// <param name="id_usuario"></param>
        public void RemoveAll(Guid id_usuario)
        {
            using (var db = new PlataformaContext())
            {
                //db.Database.ExecuteSqlCommand("update usuario_empresa set excluido = true where id_usuario = '" + id_usuario + "'");
                Db.SaveChanges();
            }
        }
    }
}