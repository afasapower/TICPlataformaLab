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
    public class Grupo_UsuarioRepository : RepositoryBase<Grupo_Usuario>, IGrupo_UsuarioRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Grupo_UsuarioRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos
        /// </summary>
        /// <param name="paramentros_Pessoa"></param>
        /// <returns></returns>
        public int Count(Parametros_Pessoa paramentros_Pessoa)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Grupo_Usuario in db.Grupo_Usuario
                              where _Grupo_Usuario.id_usuario == paramentros_Pessoa.id
                              && _Grupo_Usuario.excluido == false
                              select _Grupo_Usuario.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todos os Grupos Usuarios que não estão excluidas de acordo com os parametros
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Grupo_Usuario> GetList(Guid id_usuario, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Grupo_Usuario in db.Grupo_Usuario
                              // Inner Join
                              join _Usuario in db.Usuario on _Grupo_Usuario.id_usuario equals _Usuario.id
                              join _Grupo in db.Grupo on _Grupo_Usuario.id_grupo equals _Grupo.id
                              // Condições
                              //where ((id_usuario == Guid.Empty) || (gu.id_usuario == id_usuario))
                              where _Grupo_Usuario.id_usuario == id_usuario
                              && ((id_grupo == Guid.Empty) || (_Grupo_Usuario.id_grupo == id_grupo))
                              && _Grupo_Usuario.excluido == false

                              // Ordenação
                              orderby _Grupo.nome

                              // Define novos atributos
                              select new
                              {
                                  id = _Grupo_Usuario.id,
                                  id_grupo = _Grupo_Usuario.id_grupo,
                                  id_usuario = _Grupo_Usuario.id_usuario,
                                  //Grupos
                                  id_pessoa_empresa_grupo = _Grupo.id_pessoa_empresa,
                                  nome_grupo = _Grupo.nome,

                                  //Usuarios
                                  login_usuario = _Usuario.login,
                                  id_pessoa_usuario = _Usuario.id_pessoa,
                                  id_pessoa_empresa_usuario = _Usuario.id_pessoa_empresa,

                                  data_inclusao = _Grupo_Usuario.data_inclusao,
                                  usuario = _Grupo_Usuario.usuario,

                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Grupo_Usuario> grupo_usuario = new List<Grupo_Usuario>();

                foreach (var item in lQuery)
                {
                    Grupo_Usuario _Grupo_Usuario = new Grupo_Usuario();

                    _Grupo_Usuario.id = item.id;
                    _Grupo_Usuario.id_usuario = item.id_usuario;
                    _Grupo_Usuario.id_grupo = item.id_grupo;

                    // Informações do grupo
                    _Grupo_Usuario.Grupo = new Grupo()
                    {
                        id = item.id_grupo,
                        id_pessoa_empresa = item.id_pessoa_empresa_grupo,
                        nome = item.nome_grupo
                    };

                    _Grupo_Usuario.data_inclusao = item.data_inclusao;
                    _Grupo_Usuario.usuario = item.usuario;

                    grupo_usuario.Add(_Grupo_Usuario);
                }

                return grupo_usuario;
            }
        }

        /// <summary>
        /// Condição para remover usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        public void RemoveAll(Guid id_usuario)
        {
            using (var db = new PlataformaContext())
            {
                //db.Database.ExecuteSqlCommand("update grupo_usuario set excluido = true where id_usuario = '" + id_usuario + "'");
                Db.SaveChanges();
            }
        }
    }
}
