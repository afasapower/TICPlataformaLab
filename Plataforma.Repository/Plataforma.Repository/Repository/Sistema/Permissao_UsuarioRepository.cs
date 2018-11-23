using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Permissao_UsuarioRepository : RepositoryBase<Permissao_Usuario>, IPermissao_UsuarioRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Permissao_UsuarioRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa, Guid id_usuario)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Permissao_Usuario in db.Permissao_Usuario
                              where _Permissao_Usuario.id_usuario == id_usuario
                              select _Permissao_Usuario.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todas as permissoes de usuarios que não estão excluidos
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Permissao_Usuario> GetList(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Permissao_Usuario in db.Permissao_Usuario
                              join _Usuario in db.Usuario on _Permissao_Usuario.id_usuario equals _Usuario.id
                              join _Pagina in db.Pagina on _Permissao_Usuario.id_pagina equals _Pagina.id
                              where _Permissao_Usuario.id_usuario == id_usuario
                              && _Permissao_Usuario.id_pagina == id_pagina
                              && ((id_pagina == Guid.Empty) || (_Permissao_Usuario.id_pagina == id_pagina))
                              select new
                              {
                                  // Dados da Pagina
                                  id = _Permissao_Usuario.id,
                                  id_pagina = _Permissao_Usuario.id_pagina,
                                  nome_pagina = _Pagina.nome,
                                  ler = _Permissao_Usuario.ler,
                                  incluir = _Permissao_Usuario.incluir,
                                  atualizar = _Permissao_Usuario.atualizar,
                                  deletar = _Permissao_Usuario.deletar,
                                  upload = _Permissao_Usuario.upload,
                                  download = _Permissao_Usuario.download,
                                  outros = _Permissao_Usuario.outros,
                                  usuario = _Permissao_Usuario.usuario,
                                  data_inclusao = _Permissao_Usuario.data_inclusao
                              }).OrderBy(b => b.id).ToList(); //.Skip(page * length).Take(length).ToList().OrderBy(b => b.id);

                IList<Permissao_Usuario> permissao_usuario = new List<Permissao_Usuario>();

                foreach (var item in lQuery)
                {
                    Permissao_Usuario _Permissao_Usuario = new Permissao_Usuario()
                    {
                        id = item.id,

                        // Dados Pagina
                        id_pagina = item.id_pagina,
                        Pagina = new Pagina()
                        {
                            id = item.id_pagina,
                            nome = item.nome_pagina
                        },

                        ler = item.ler,
                        incluir = item.incluir,
                        atualizar = item.atualizar,
                        deletar = item.deletar,
                        upload = item.upload,
                        download = item.download,
                        outros = item.outros,
                        usuario = item.usuario,
                        data_inclusao = item.data_inclusao
                    };

                    permissao_usuario.Add(_Permissao_Usuario);
                }

                return permissao_usuario;
            }
        }
    }
}
