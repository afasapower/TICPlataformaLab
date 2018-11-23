using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System.Linq;
using System;
using System.Collections.Generic;
using Plataforma.Domain.Entities.NotMapped;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Permissao_GrupoRepository : RepositoryBase<Permissao_Grupo>, IPermissao_GrupoRepository
    {
        public PlataformaContext Context; //Refazer metodo para parametro

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Permissao_GrupoRepository(PlataformaContext context) : base(context)
        {
            context = Context;
        }

        /// <summary>
        /// Retorna o total de registro não excluidos 
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa, Guid id_grupo)
        {
            using (var db = new PlataformaContext())
            {
                //var lQuery = (from _Permissao_Grupo in db.Permissao_Grupo
                //              where _Permissao_Grupo.id_grupo == id_grupo
                //              && _Permissao_Grupo.excluido == false
                //              select _Permissao_Grupo.id);
                var lQuery = (from _Menu_Sub in db.Menu_Sub
                              // Inner join Menu
                              join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                              // Inner Join Modulo
                              join _Modulo in db.Modulo on _Menu.id_modulo equals _Modulo.id
                              // left join Permissão
                              join _Permissao_Grupo in db.Permissao_Grupo on _Menu_Sub.id_pagina equals _Permissao_Grupo.id_pagina into _PG
                              from _Permissao_Grupo in _PG.DefaultIfEmpty(new Permissao_Grupo())
                              // Inner join Grupo
                              join _Grupo in db.Grupo on _Permissao_Grupo.id_grupo equals _Grupo.id
                              // Condições
                              where _Permissao_Grupo.id_grupo == id_grupo
                              && _Menu_Sub.excluido == false
                              orderby _Modulo.nome
                              select new
                              {
                                  // Grupo
                                  //id_grupo = _Grupo.id,
                                  //nome_grupo = _Grupo.nome,
                                  // Modulo
                                  id_modulo = _Modulo.id,
                                  nome_modulo = _Modulo.nome,
                                  // Menu
                                  id_menu = _Menu.id,
                                  nome_menu = _Menu.nome
                              });

                // Agrupa os resultados
                var result = from i in
                             (from _lQuery in lQuery
                              select new
                              {
                                  id_modulo = _lQuery.id_modulo,
                                  nome_modulo = _lQuery.nome_modulo,
                                  id_menu = _lQuery.id_menu,
                                  nome_menu = _lQuery.nome_menu
                              })
                             group i by new { i.id_modulo, i.nome_modulo, i.id_menu, i.nome_menu } into g
                             select new { g.Key.id_modulo, g.Key.nome_modulo, g.Key.id_menu, g.Key.nome_menu };

                return result.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todos as permissoes de grupo que não estão excluidas
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Permissao_Grupo> GetList(Guid id_empresa, Guid id_grupo, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Permissao_Grupo in db.Permissao_Grupo
                              join _Grupo in db.Grupo on _Permissao_Grupo.id_grupo equals _Grupo.id
                              join _Pagina in db.Pagina on _Permissao_Grupo.id_pagina equals _Pagina.id
                              where _Grupo.id_pessoa_empresa == id_empresa
                              && _Permissao_Grupo.id_grupo == id_grupo
                              && ((id_pagina == Guid.Empty) || (_Permissao_Grupo.id_pagina == id_pagina))
                              && _Permissao_Grupo.excluido == false
                              && _Grupo.excluido == false
                              && _Pagina.excluido == false
                              select new
                              {
                                  id = _Permissao_Grupo.id,
                                  id_grupo = _Permissao_Grupo.id_grupo,
                                  // Dados do Grupo
                                  nome_grupo = _Grupo.nome,
                                  id_pagina = _Permissao_Grupo.id_pagina,
                                  // Dados da Pagina
                                  nome_pagina = _Pagina.nome,
                                  ler = _Permissao_Grupo.ler,
                                  incluir = _Permissao_Grupo.incluir,
                                  atualizar = _Permissao_Grupo.atualizar,
                                  deletar = _Permissao_Grupo.deletar,
                                  upload = _Permissao_Grupo.upload,
                                  download = _Permissao_Grupo.download,
                                  outros = _Permissao_Grupo.outros,
                                  usuario = _Permissao_Grupo.usuario,
                                  data_inclusao = _Permissao_Grupo.data_inclusao
                              }).OrderBy(b => b.id).ToList(); //.Skip(page * length).Take(length).ToList().OrderBy(b => b.id);

                IList<Permissao_Grupo> permissao_grupo = new List<Permissao_Grupo>();

                foreach (var item in lQuery)
                {
                    Permissao_Grupo _Permissao_Grupo = new Permissao_Grupo()
                    {
                        id = item.id,
                        // Dados Grupo
                        id_grupo = item.id_grupo,
                        Grupo = new Grupo()
                        {
                            id = item.id_grupo,
                            nome = item.nome_grupo
                        },

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

                    permissao_grupo.Add(_Permissao_Grupo);
                }
                return permissao_grupo;
            }
        }

        /// <summary>
        /// Retorna uma lista de todas as permissoes das paginas que não estão excluidas
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="id_menu"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Retorno_Permissao_Grupo_Pagina> GetListPermissaoPagina(Guid id_empresa, Guid id_grupo, Guid id_menu, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Menu_Sub in db.Menu_Sub
                              // Inner join Menu
                              join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                              // Inner Join Modulo
                              join _Modulo in db.Modulo on _Menu.id_modulo equals _Modulo.id
                              // Inner join Permissão
                              join _Permissao_Grupo in db.Permissao_Grupo on _Menu_Sub.id_pagina equals _Permissao_Grupo.id_pagina
                              // Inner join Grupo
                              join _Grupo in db.Grupo on _Permissao_Grupo.id_grupo equals _Grupo.id
                              // Inner join Pagina
                              join _Pagina in db.Pagina on _Permissao_Grupo.id_pagina equals _Pagina.id
                              // Condições
                              where _Permissao_Grupo.id_grupo == id_grupo
                              && _Menu.id == id_menu
                              && _Menu_Sub.excluido == false
                              && _Menu.excluido == false
                              && _Modulo.excluido == false
                              && _Permissao_Grupo.excluido == false
                              && _Grupo.excluido == false
                              && _Pagina.excluido == false
                              select new
                              {
                                  id = _Permissao_Grupo.id,

                                  // Modulo
                                  id_modulo = _Modulo.id,
                                  nome_modulo = _Modulo.nome,
                                  // Menu
                                  id_menu = _Menu.id,
                                  nome_menu = _Menu.nome,
                                  // Sub-Menu
                                  id_menu_sub = _Menu_Sub.id,
                                  nome_menu_sub = _Menu_Sub.nome,
                                  //Grupo
                                  id_grupo = _Grupo.id,
                                  nome_grupo = _Grupo.nome,
                                  //Página
                                  id_pagina = _Pagina.id,
                                  nome_pagina = _Pagina.nome
                              }).OrderByDescending(b => b.nome_pagina).ToList().Distinct(); //.Skip(page * length).Take(length).ToList().OrderByDescending(b => b.nome_pagina);

                IList<Retorno_Permissao_Grupo_Pagina> retorno_permissao_grupo_pagina = new List<Retorno_Permissao_Grupo_Pagina>();

                foreach (var item in lQuery)
                {
                    Retorno_Permissao_Grupo_Pagina _Retorno_Permissao_Grupo_Pagina = new Retorno_Permissao_Grupo_Pagina()
                    {
                        id = item.id,
                        id_modulo = item.id_modulo,
                        nome_modulo = item.nome_modulo,
                        id_menu = item.id_menu,
                        nome_menu = item.nome_menu,
                        id_menu_sub = item.id_menu_sub,
                        nome_menu_sub = item.nome_menu_sub,
                        id_grupo = item.id_grupo,
                        nome_grupo = item.nome_grupo,
                        id_pagina = item.id_pagina,
                        nome_pagina = item.nome_menu_sub
                    };

                    retorno_permissao_grupo_pagina.Add(_Retorno_Permissao_Grupo_Pagina);
                }

                return retorno_permissao_grupo_pagina.OrderByDescending(p => p.nome_pagina);
            }
        }

        /// <summary>
        /// Retorna uma lista de todas as permissoes por menu que não estão excluidas
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Retorno_Permissao_Grupo_Menu> GetListPermissaoMenu(Guid id_empresa, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Menu_Sub in db.Menu_Sub
                              // Inner join Menu
                              join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                              // Inner Join Modulo
                              join _Modulo in db.Modulo on _Menu.id_modulo equals _Modulo.id
                              // left join Permissão
                              join _Permissao_Grupo in db.Permissao_Grupo on _Menu_Sub.id_pagina equals _Permissao_Grupo.id_pagina into _PG
                              from _Permissao_Grupo in _PG.DefaultIfEmpty(new Permissao_Grupo())
                              // Inner join Grupo
                              join _Grupo in db.Grupo on _Permissao_Grupo.id_grupo equals _Grupo.id
                              // Condições
                              where _Permissao_Grupo.id_grupo == id_grupo
                              && _Menu_Sub.excluido == false
                              orderby _Modulo.nome
                              select new
                              {
                                  // Grupo
                                  //id_grupo = _Grupo.id,
                                  //nome_grupo = _Grupo.nome,
                                  // Modulo
                                  id_modulo = _Modulo.id,
                                  nome_modulo = _Modulo.nome,
                                  // Menu
                                  id_menu = _Menu.id,
                                  nome_menu = _Menu.nome
                              });

                             // Agrupa os resultados
                             var result = from i in
                             (from _lQuery in lQuery
                              select new
                              {
                                  id_modulo = _lQuery.id_modulo,
                                  nome_modulo = _lQuery.nome_modulo,
                                  id_menu = _lQuery.id_menu,
                                  nome_menu = _lQuery.nome_menu
                              })
                             group i by new { i.id_modulo, i.nome_modulo, i.id_menu, i.nome_menu } into g
                             select new { g.Key.id_modulo, g.Key.nome_modulo, g.Key.id_menu, g.Key.nome_menu };

                IList<Retorno_Permissao_Grupo_Menu> retorno_permissao_grupo_menu = new List<Retorno_Permissao_Grupo_Menu>();

                foreach (var item in result)
                {
                    Retorno_Permissao_Grupo_Menu _Retorno_Permissao_Grupo_Menu = new Retorno_Permissao_Grupo_Menu()
                    {
                        //id_grupo = item.id_grupo,
                        //nome_grupo = item.nome_grupo,
                        id_modulo = item.id_modulo,
                        nome_modulo = item.nome_modulo,
                        id_menu = item.id_menu,
                        nome_menu = item.nome_menu
                    };

                    retorno_permissao_grupo_menu.Add(_Retorno_Permissao_Grupo_Menu);
                }

                return retorno_permissao_grupo_menu.ToList(); //.Skip(page * length).Take(length).ToList();
            }
        }
    }
}