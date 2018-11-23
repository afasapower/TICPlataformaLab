using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Menu_SubRepository : RepositoryBase<Menu_Sub>, IMenu_SubRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Menu_SubRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna uma lista de Sub-Menu para o Front do sistema
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetListFront(Guid id_usuario)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery_Usuario = (from _Grupo_Usuario in db.Grupo_Usuario
                                      where _Grupo_Usuario.id_usuario == id_usuario
                                      && _Grupo_Usuario.excluido == false
                                      select new { _Grupo_Usuario.id_grupo, _Grupo_Usuario.id_usuario }).ToList();

                var lQuery = (from _Menu_Sub1 in db.Menu_Sub
                              // Left join
                              join _Menu_Sub2 in db.Menu_Sub on _Menu_Sub1.parent equals _Menu_Sub2.id into _Menu_Sub2_
                              from _Menu_Sub2 in _Menu_Sub2_.DefaultIfEmpty(new Menu_Sub())
                              // Left join
                              join _Pagina in db.Pagina on _Menu_Sub1.id_pagina equals _Pagina.id into _Pagina2
                              from _Pagina in _Pagina2.DefaultIfEmpty(new Pagina())
                              where _Pagina.id_situacao_cadastral == 0
                              // Left join
                              join _Permissao_Usuario in db.Permissao_Usuario on _Pagina.id equals _Permissao_Usuario.id_pagina
                              where _Permissao_Usuario.ler == true && _Permissao_Usuario.id_usuario == id_usuario
                              && _Menu_Sub1.tipo_pagina == "M"
                              && _Menu_Sub1.excluido == false
                              orderby _Menu_Sub1.parent descending
                              select new
                              {
                                  id = _Menu_Sub1.id,
                                  id_menu = _Menu_Sub1.id_menu,
                                  id_pagina = _Menu_Sub1.id_pagina,
                                  id_situacao_cadastral = _Menu_Sub1.id_situacao_cadastral,
                                  parent = _Menu_Sub1.parent,
                                  nome = _Menu_Sub1.nome,
                                  descricao = _Menu_Sub1.descricao,
                                  nome_imagem = _Menu_Sub1.nome_imagem,
                                  url = _Menu_Sub1.url,
                                  ordem = _Menu_Sub1.ordem,
                                  numero_grid = _Menu_Sub1.numero_grid
                              }).Union(from _Menu_Sub in db.Menu_Sub
                                       // Left join
                                       join Menu_Sub2 in db.Menu_Sub on _Menu_Sub.parent equals Menu_Sub2.id into Menu_Sub2_
                                       from Menu_Sub2 in Menu_Sub2_.DefaultIfEmpty()
                                       // Left join
                                       join _Pagina in db.Pagina on _Menu_Sub.id_pagina equals _Pagina.id into _Pagina2
                                       from _Pagina in _Pagina2.DefaultIfEmpty()
                                       where _Pagina.id_situacao_cadastral == 0
                                       // Left join
                                       join _Permissao_Grupo in db.Permissao_Grupo on _Pagina.id equals _Permissao_Grupo.id_pagina
                                       join _Grupo_Usuario in db.Grupo_Usuario on _Permissao_Grupo.id_grupo equals _Grupo_Usuario.id_grupo
                                       join _lQuery_Usuario in lQuery_Usuario on _Grupo_Usuario.id_grupo equals _lQuery_Usuario.id_grupo
                                       where _Permissao_Grupo.ler == true && _Grupo_Usuario.id_usuario == id_usuario
                                       && _Menu_Sub.tipo_pagina == "M"
                                       && _Menu_Sub.excluido == false
                                       orderby _Menu_Sub.parent descending
                                       select new
                                       {
                                           id = _Menu_Sub.id,
                                           id_menu = _Menu_Sub.id_menu,
                                           id_pagina = _Menu_Sub.id_pagina,
                                           id_situacao_cadastral = _Menu_Sub.id_situacao_cadastral, 
                                           parent = _Menu_Sub.parent,
                                           nome = _Menu_Sub.nome,
                                           descricao = _Menu_Sub.descricao,
                                           nome_imagem = _Menu_Sub.nome_imagem,
                                           url = _Menu_Sub.url,
                                           ordem = _Menu_Sub.ordem,
                                           numero_grid = _Menu_Sub.numero_grid
                                       }).ToList();

                IList<Menu_Sub> menu_sub = new List<Menu_Sub>();

                foreach (var item in lQuery)
                {
                    Menu_Sub _Menu_Sub = new Menu_Sub
                    {
                        id = item.id,
                        id_menu = item.id_menu,
                        id_pagina = item.id_pagina,
                        id_situacao_cadastral = item.id_situacao_cadastral,
                        parent = (item.parent == null ? Guid.Empty : item.parent),
                        nome = item.nome,
                        descricao = item.descricao,
                        nome_imagem = item.nome_imagem,
                        url = item.url,
                        ordem = item.ordem,
                        numero_grid = item.numero_grid
                    };

                    menu_sub.Add(_Menu_Sub);
                }

                // return menu_sub.OrderBy(a => new { a.parent, a.ordem, a.id_menu, a.id, a.id_pagina});
                return menu_sub.OrderBy(x => x.ordem);
            }

        }

        /// <summary>
        /// Retorna uma lista de Menus para a Administração de Sub-Menus
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="tipo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetList(Guid id_menu, string tipo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Menu_Sub in db.Menu_Sub
                              // Left Join
                              join _Pagina in db.Pagina on _Menu_Sub.id_pagina equals _Pagina.id //into p2
                                                                              //from P in p2.DefaultIfEmpty()
                              where _Pagina.id_situacao_cadastral == 0
                              join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                              join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu_Sub.id_situacao_cadastral equals _Situacao_Cadastral.id

                              // tem que passar id menu
                              where ((id_menu == Guid.Empty) || (_Menu_Sub.id_menu == id_menu))
                              && ((tipo == "") || (_Menu_Sub.tipo_pagina == tipo))
                              && _Menu_Sub.excluido == false

                              orderby _Menu_Sub.id_pagina descending, _Menu_Sub.ordem, _Menu_Sub.nome

                              select new
                              {
                                  id = _Menu_Sub.id,
                                  id_menu = _Menu_Sub.id_menu,
                                  nome_menu = _Menu.nome,
                                  id_situacao_cadastral = _Menu_Sub.id_situacao_cadastral,
                                  nome_situacao = _Situacao_Cadastral.descricao,
                                  id_pagina = _Pagina.id,
                                  nome_pagina = _Pagina.nome,
                                  nome = _Menu_Sub.nome,
                                  descricao = _Menu_Sub.descricao,
                                  url = _Menu_Sub.url,
                                  parent = _Menu_Sub.parent,
                                  ordem = _Menu_Sub.ordem,
                                  data_inclusao = _Menu_Sub.data_inclusao,
                                  usuario = _Menu_Sub.usuario,
                                  tipo_pagina = _Menu_Sub.tipo_pagina,
                                  numero_grid = _Menu_Sub.numero_grid
                              }).Union(from _Menu_Sub in db.Menu_Sub
                                       join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                                       join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu_Sub.id_situacao_cadastral equals _Situacao_Cadastral.id
                                       where ((id_menu == Guid.Empty) || (_Menu_Sub.id_menu == id_menu))
                                       //where sm.id_menu == id_menu

                                       && _Menu_Sub.id_pagina == null
                                       && _Menu_Sub.excluido == false
                                       orderby _Menu_Sub.id_pagina descending, _Menu_Sub.ordem, _Menu_Sub.nome
                                       select new
                                       {
                                           id = _Menu_Sub.id,
                                           id_menu = _Menu_Sub.id_menu,
                                           nome_menu = _Menu.nome,
                                           id_situacao_cadastral = _Menu_Sub.id_situacao_cadastral,
                                           nome_situacao = _Situacao_Cadastral.descricao,
                                           id_pagina = Guid.Empty,
                                           nome_pagina = "",
                                           nome = _Menu_Sub.nome,
                                           descricao = _Menu_Sub.descricao,
                                           url = _Menu_Sub.url,
                                           parent = _Menu_Sub.parent,
                                           ordem = _Menu_Sub.ordem,
                                           data_inclusao = _Menu_Sub.data_inclusao,
                                           usuario = _Menu_Sub.usuario,
                                           tipo_pagina = _Menu_Sub.tipo_pagina,
                                           numero_grid = _Menu_Sub.numero_grid
                                       }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Menu_Sub> menu_sub = new List<Menu_Sub>();

                foreach (var item in lQuery.ToList())
                {
                    Menu_Sub _Menu_Sub = new Menu_Sub
                    {
                        id = item.id,
                        nome = item.nome,
                        descricao = item.descricao,
                        url = item.url,
                        parent = (item.parent == null ? Guid.Empty : item.parent),
                        ordem = item.ordem,
                        data_inclusao = item.data_inclusao,
                        usuario = item.usuario,
                        id_pagina = item.id_pagina,
                        numero_grid = item.numero_grid,

                        // Monta informação do menu
                        Menu = new Menu()
                        {
                            id = item.id_menu,
                            descricao = item.nome_menu
                        },
                        // Monta a situação cadastrado
                        Situacao_Cadastral = new Situacao_Cadastral()
                        {
                            id = item.id_situacao_cadastral,
                            descricao = item.nome_situacao
                        },
                        // Monta informação da pagina
                        Pagina = new Pagina()
                        {
                            id = item.id_pagina,
                            nome = item.nome_pagina
                        },

                        tipo_pagina = item.tipo_pagina
                    };

                    menu_sub.Add(_Menu_Sub);
                }

                return menu_sub.OrderBy(m => m.id_pagina);
            }
        }


        /// <summary>
        /// Retorna as abas do menu pagina
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetListAba(Guid id_menu, Guid parent)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Menu_Sub in db.Menu_Sub
                              // Inner Join em Paginas
                              join _Pagina in db.Pagina on _Menu_Sub.id_pagina equals _Pagina.id
                              where _Pagina.id_situacao_cadastral == 0
                              // Inner Join em Permissão de Grupo
                              join _Permissao_Grupo in db.Permissao_Grupo on _Menu_Sub.id_pagina equals _Permissao_Grupo.id_pagina
                              where _Permissao_Grupo.ler == true
                              // Inner Join em Menu
                              join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                              join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu_Sub.id_situacao_cadastral equals _Situacao_Cadastral.id
                              // Condiçoes
                              where _Menu_Sub.id_menu == id_menu && _Menu_Sub.parent == parent
                              && _Menu_Sub.tipo_pagina == "A"
                              && _Menu_Sub.excluido == false
                              orderby _Menu_Sub.id_pagina descending, _Menu_Sub.ordem, _Menu_Sub.nome

                              select new
                              {
                                  id = _Menu_Sub.id,
                                  id_menu = _Menu_Sub.id_menu,
                                  nome_menu = _Menu.nome,
                                  id_situacao_cadastral = _Menu_Sub.id_situacao_cadastral,
                                  nome_situacao = _Situacao_Cadastral.descricao,
                                  id_pagina = _Pagina.id,
                                  nome_pagina = _Pagina.nome,
                                  nome = _Menu_Sub.nome,
                                  descricao = _Menu_Sub.descricao,
                                  url = _Menu_Sub.url,
                                  parent = _Menu_Sub.parent,
                                  ordem = _Menu_Sub.ordem,
                                  data_inclusao = _Menu_Sub.data_inclusao,
                                  usuario = _Menu_Sub.usuario,
                                  tipo_pagina = _Menu_Sub.tipo_pagina,
                                  numero_grid = _Menu_Sub.numero_grid
                              }).Union(from _Menu_Sub in db.Menu_Sub
                                       // Inner Join Menu
                                       join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                                       // Inner Join Permissão de Grupo
                                       join _Permissao_Grupo in db.Permissao_Grupo on _Menu_Sub.id_pagina equals _Permissao_Grupo.id_pagina
                                       where _Permissao_Grupo.ler == true
                                       // Inner Join Situação Cadastral
                                       join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu_Sub.id_situacao_cadastral equals _Situacao_Cadastral.id
                                       // Condições
                                       where _Menu_Sub.id_menu == id_menu
                                       && _Menu_Sub.id_pagina == null
                                       && _Menu_Sub.parent == parent
                                       && _Menu_Sub.tipo_pagina == "A"
                                       && _Menu_Sub.excluido == false
                                       orderby _Menu_Sub.id_pagina descending, _Menu_Sub.ordem, _Menu_Sub.nome
                                       select new
                                       {
                                           id = _Menu_Sub.id,
                                           id_menu = _Menu_Sub.id_menu,
                                           nome_menu = _Menu.nome,
                                           id_situacao_cadastral = _Menu_Sub.id_situacao_cadastral,
                                           nome_situacao = _Situacao_Cadastral.descricao,
                                           id_pagina = Guid.Empty,
                                           nome_pagina = "",
                                           nome = _Menu_Sub.nome,
                                           descricao = _Menu_Sub.descricao,
                                           url = _Menu_Sub.url,
                                           parent = _Menu_Sub.parent,
                                           ordem = _Menu_Sub.ordem,
                                           data_inclusao = _Menu_Sub.data_inclusao,
                                           usuario = _Menu_Sub.usuario,
                                           tipo_pagina = _Menu_Sub.tipo_pagina,
                                           numero_grid = _Menu_Sub.numero_grid
                                       }).ToList();

                IList<Menu_Sub> menu_sub = new List<Menu_Sub>();

                foreach (var item in lQuery.ToList())
                {
                    Menu_Sub _Menu_Sub = new Menu_Sub
                    {
                        id = item.id,
                        nome = item.nome,
                        descricao = item.descricao,
                        url = item.url,
                        parent = item.parent,
                        ordem = item.ordem,
                        data_inclusao = item.data_inclusao,
                        usuario = item.usuario,

                        // Monta informação do menu
                        Menu = new Menu()
                        {
                            id = item.id_menu,
                            descricao = item.nome_menu
                        },
                        // Monta a situação cadastrado
                        Situacao_Cadastral = new Situacao_Cadastral()
                        {
                            id = item.id_situacao_cadastral,
                            descricao = item.nome_situacao
                        },
                        // Monta informação da pagina
                        Pagina = new Pagina()
                        {
                            id = item.id_pagina,
                            nome = item.nome_pagina
                        },

                        tipo_pagina = item.tipo_pagina,
                        numero_grid = item.numero_grid
                    };

                    menu_sub.Add(_Menu_Sub);
                }

                return menu_sub.OrderBy(m => m.ordem);
            }
        }

        /// <summary>
        /// Lista as paginas disponiveis para o menu
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="tipo"></param>
        /// <param name="_Parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetListPaginaDisponivel(Guid id_menu, string tipo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Menu_Sub in db.Menu_Sub
                              // Left Join
                              join _Pagina in db.Pagina on _Menu_Sub.id_pagina equals _Pagina.id //into p2
                              //from P in p2.DefaultIfEmpty()
                              where _Pagina.id_situacao_cadastral == 0

                              join _Menu in db.Menu on _Menu_Sub.id_menu equals _Menu.id
                              join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu_Sub.id_situacao_cadastral equals _Situacao_Cadastral.id

                              // tem que passar id menu
                              where (_Menu_Sub.id_menu == id_menu)
                              && ((tipo == "") || (_Menu_Sub.tipo_pagina == tipo))
                              && _Menu_Sub.excluido == false

                              // orderby sm.id_pagina descending, sm.ordem, sm.nome

                              select new
                              {
                                  id = _Menu_Sub.id,
                                  id_menu = _Menu_Sub.id_menu,
                                  nome_menu = _Menu.nome,
                                  id_situacao_cadastral = _Menu_Sub.id_situacao_cadastral,
                                  nome_situacao = _Situacao_Cadastral.descricao,
                                  id_pagina = _Pagina.id,
                                  nome_pagina = _Pagina.nome,
                                  nome = _Menu_Sub.nome,
                                  descricao = _Menu_Sub.descricao,
                                  url = _Menu_Sub.url,
                                  parent = _Menu_Sub.parent,
                                  ordem = _Menu_Sub.ordem,
                                  data_inclusao = _Menu_Sub.data_inclusao,
                                  usuario = _Menu_Sub.usuario,
                                  tipo_pagina = _Menu_Sub.tipo_pagina,
                                  numero_grid = _Menu_Sub.numero_grid
                              }).OrderByDescending(p => p.nome_pagina).ToList(); //.Skip(page * length).Take(length).OrderByDescending(p => p.nome_pagina);

                IList<Menu_Sub> menu_sub = new List<Menu_Sub>();

                foreach (var item in lQuery.ToList())
                {
                    Menu_Sub _Menu_Sub = new Menu_Sub
                    {
                        id = item.id,
                        nome = item.nome,
                        descricao = item.descricao,
                        url = item.url,
                        parent = item.parent,
                        ordem = item.ordem,
                        data_inclusao = item.data_inclusao,
                        usuario = item.usuario,
                        id_pagina = item.id_pagina,

                        // Monta informação do menu
                        Menu = new Menu()
                        {
                            id = item.id_menu,
                            descricao = item.nome_menu
                        },
                        // Monta a situação cadastrado
                        Situacao_Cadastral = new Situacao_Cadastral()
                        {
                            id = item.id_situacao_cadastral,
                            descricao = item.nome_situacao
                        },
                        // Monta informação da pagina
                        Pagina = new Pagina()
                        {
                            id = item.id_pagina,
                            nome = item.nome_pagina
                        },

                        numero_grid = item.numero_grid,
                        tipo_pagina = item.tipo_pagina
                    };

                    menu_sub.Add(_Menu_Sub);
                }

                return menu_sub.OrderByDescending(m => m.Menu.descricao).OrderBy(m => m.Menu.nome);
            }
        }
    }
}
