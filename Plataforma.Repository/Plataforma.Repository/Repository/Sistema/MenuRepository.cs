using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public PlataformaContext Context; //Refazer metodo para parametro

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public MenuRepository(PlataformaContext context) : base(context)
        {
            Context = context;//Refazer metodo para parametro
        }

        /// <summary>
        /// Retorna os menus ativos do módulo
        /// </summary>
        /// <param name="id_modulo"></param>
        /// <param name="id_menu"></param>
        /// <param name="id_usuario"></param>
        /// <param name="_Parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetList(Guid id_modulo, Guid id_menu, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            Menu_SubRepository _Menu_SubRepository = new Menu_SubRepository(Context);//Refazer metodo para parametro

            var lQuery_SubMenu1 = (from _Menu in Db.Menu select new { _Menu.id }).Distinct();

            IList<Menu> menu = new List<Menu>();

            using (var db = new PlataformaContext())
            {

                if (lQuery_SubMenu1.Count() > 0)
                {
                    var lQuery = (from _Menu in db.Menu
                                  // Modulo
                                  join _Modulo in db.Modulo on _Menu.id_modulo equals _Modulo.id
                                  // Situacao Cadastral
                                  //join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu.id_situacao_cadastral equals _Situacao_Cadastral.id

                                  //join _lQuery_SubMenu in lQuery_SubMenu1 on _Menu.id_modulo equals _lQuery_SubMenu.id

                                  // Condições
                                  where _Modulo.id == id_modulo
                                  && _Menu.excluido == false
                                  && ((id_menu == Guid.Empty) || (_Menu.id == id_menu))
                                  orderby _Menu.ordem, _Menu.id_modulo, _Menu.id, _Menu.nome, _Menu.descricao, _Menu.nome_imagem
                                  select new
                                  {
                                      id = _Menu.id,
                                      id_modulo = _Menu.id_modulo,
                                      nome_modulo = _Modulo.nome,
                                      nome = _Menu.nome,
                                      descricao = _Menu.descricao,
                                      nome_imagem = _Menu.nome_imagem,
                                      ordem = _Menu.ordem,
                                      id_situacao_cadastral = _Menu.id_situacao_cadastral,
                                      //nome_situacao = _Situacao_Cadastral.descricao
                                  }).ToList(); //.Skip(page * length).Take(length).ToList();

                    foreach (var item in lQuery)
                    {
                        Menu _Menu = new Menu();

                        _Menu.id = item.id;
                        _Menu.id_modulo = item.id_modulo;
                        _Menu.nome = item.nome;
                        _Menu.descricao = item.descricao;
                        _Menu.nome_imagem = item.nome_imagem;
                        _Menu.ordem = item.ordem;
                        _Menu.Modulo = new Modulo()
                        {
                            id = item.id_modulo,
                            nome = item.nome_modulo
                        };
                        _Menu.Situacao_Cadastral = new Situacao_Cadastral()
                        {
                            id = item.id_situacao_cadastral,
                            //descricao = item.nome_situacao
                        };

                        menu.Add(_Menu);
                    }

                }
                else
                {
                    var lQuery = (from _Menu in db.Menu
                                  // Modulo
                                  join _Modulo in db.Modulo on _Menu.id_modulo equals _Modulo.id
                                  // Situacao Cadastral
                                  //join _Situacao_Cadastral in db.Situacao_Cadastral on _Menu.id_situacao_cadastral equals _Situacao_Cadastral.id

                                  // Condições
                                  where _Modulo.id == id_modulo
                                  && _Menu.excluido == false
                                  && ((id_menu == Guid.Empty) || (_Menu.id == id_menu))
                                  orderby _Menu.ordem, _Menu.id_modulo, _Menu.id, _Menu.nome, _Menu.descricao, _Menu.nome_imagem
                                  select new
                                  {
                                      id = _Menu.id,
                                      id_modulo = _Menu.id_modulo,
                                      nome_modulo = _Modulo.nome,
                                      nome = _Menu.nome,
                                      descricao = _Menu.descricao,
                                      nome_imagem = _Menu.nome_imagem,
                                      ordem = _Menu.ordem,
                                      id_situacao_cadastral = _Menu.id_situacao_cadastral,
                                      //nome_situacao = _Situacao_Cadastral.descricao
                                  }).ToList(); //.Skip(page * length).Take(length).ToList();

                    foreach (var item in lQuery)
                    {
                        Menu _Menu = new Menu();

                        _Menu.id = item.id;
                        _Menu.id_modulo = item.id_modulo;
                        _Menu.nome = item.nome;
                        _Menu.descricao = item.descricao;
                        _Menu.nome_imagem = item.nome_imagem;
                        _Menu.ordem = item.ordem;
                        _Menu.Modulo = new Modulo()
                        {
                            id = item.id_modulo,
                            nome = item.nome_modulo
                        };
                        _Menu.Situacao_Cadastral = new Situacao_Cadastral()
                        {
                            id = item.id_situacao_cadastral,
                            //descricao = item.nome_situacao
                        };

                        menu.Add(_Menu);
                    }
                }

                return menu.OrderBy(m => m.ordem);
            }
        }
    }
}
