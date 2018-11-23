using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.Controllers.Sistema
{
    public class Sistema_MenuController : BaseController
    {
        public IModuloService ModuloService { get; private set; }
        public IMenuService MenuService { get; private set; }
        public IMenu_SubService MenuSubService { get; private set; }
        public IPaginaService PaginaService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }
        private IHostingEnvironment hostingEnv;

        public Guid IdUsuarioLogado { get; private set; }

        public Sistema_MenuController(IModuloService moduloService,
                                      IMenuService menuService,
                                      IMenu_SubService menuSubService,
                                      IPaginaService paginaService,
                                      ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                      IUsuarioService usuarioService,
                                      IHostingEnvironment env,
                                      IMapper mapper) : base(mapper)
        {
            ModuloService = moduloService;
            MenuService = menuService;
            MenuSubService = menuSubService;            
            PaginaService = paginaService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            UsuarioService = usuarioService;
            this.hostingEnv = env;
        }

        /// <summary>
        /// Lista inicial
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                   Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            IdUsuarioLogado = new Guid(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value);
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);
            return View(listaPermissoesMenus);
        }

        /// <summary>
        /// Página principal de abas
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }

        /// <summary>
        /// Controller get para listagem inicial
        /// </summary>
        /// <param name="_Parametros"></param>
        /// <returns></returns>
        [Authorize, HttpPost]
        public IActionResult SelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                             Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            var modulo = ModuloService.GetList(_Parametros_Busca_Grid);
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = modulo.Count(), recordsFiltered = 0, data = modulo });
        }

        #region Modulos

        /// <summary>
        /// Controller Get ModuloAba
        /// </summary>
        /// <param name="_Parametros"></param>
        /// <returns></returns>
        [Authorize, Route("Sistema_Menu/modulo-aba")]
        public IActionResult ModuloAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                       Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);

            var viewMenu = new MenuViewModel()
            {
                modulos = ModuloService.GetById(_Parametros_Busca_Navegacao.id),
                listaPermissoesMenus = listaPermissoesMenus
            };
            return View("modulos-aba", viewMenu);
        }

        /// <summary>
        /// Controller Post ModuloAba
        /// </summary>
        /// <param name="modulos"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("Sistema_Menu/modulo-aba")]
        public IActionResult ModuloAba(Modulo modulos, string tipo)
        {
            ModuloService.Add(modulos);

            return Json(new { data = "ok", iditem = modulos.id });
        }

        #endregion

        #region Menu

        /// <summary>
        /// Controller Get menu-acoes-aba
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, Route("Sistema_Menu/menu-acoes-aba")]
        public IActionResult MenuAcoesAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                          Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            IdUsuarioLogado = new Guid(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value);
            List<Menu> lista = MenuService.GetList(_Parametros_Busca_Navegacao.id, Guid.Empty, IdUsuarioLogado, _Parametros_Busca_Grid).ToList();
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);

            var viewMenu = new MenuViewModel()
            {
                listaModulos = ModuloService.GetList(_Parametros_Busca_Grid).ToList(),
                listaPermissoesMenus = listaPermissoesMenus,
                menu = (lista.Count > 0 ? lista[0] : new Menu())
            };
            return View("menu-acoes-aba", viewMenu);
        }

        [Authorize, Route("Sistema_Menu/menu-acoes-edicao-aba")]
        public IActionResult MenuAcoesEdicaoAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            var viewMenu = new MenuViewModel()
            {
                listaModulos = ModuloService.GetList(_Parametros_Busca_Grid).ToList(),            
                menu = MenuService.GetById(_Parametros_Busca_Navegacao.id)
            };
            return View("menu-acoes-aba", viewMenu);
        }

        /// <summary>
        /// Controller Post menu-acoes-aba
        /// </summary>
        /// <param name="menuAcoes"></param>
        /// <param name="_Parametros"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("Sistema_Menu/menu-acoes-aba"), ValidateAntiForgeryToken]
        public IActionResult MenuAcoesAbaOperacoes(Menu menuAcoes,
                                                   Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            bool status = true;
            object data = null;

            try
            {
                Arquivos upload = new Arquivos(hostingEnv);

                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    var excluir = MenuService.GetById(menuAcoes.id);
                    excluir.usuario = _Parametros_Busca_Navegacao.usuario;
                    excluir.excluido = true;
                    MenuService.Update(excluir);
                }
                else
                        if (ModelState.IsValid)
                {

                    menuAcoes.id_modulo = _Parametros_Busca_Navegacao.id_modulo;
                    object message = null;
                    switch (_Parametros_Busca_Navegacao.acao)
                    {
                        case "I":
                            MenuService.Add(menuAcoes);
                            message = upload.UploadArquivos(Request, "sistema/menu");
                            break;
                        case "U":
                            menuAcoes.id = _Parametros_Busca_Navegacao.id;
                            menuAcoes.usuario = _Parametros_Busca_Navegacao.usuario;
                            MenuService.Update(menuAcoes);
                            message = upload.UploadArquivos(Request, "sistema/menu");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    status = false;
                    data = Formularios.CapturaModelErros(ModelState);
                }

            }
            catch (Exception ex)
            {
                Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao
                {
                    id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa,
                    origem = "Cadastro de Menus",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = menuAcoes.ToString(),
                    metodo = "Sistema_menuController - MenuAcoesAba",
                    usuario = menuAcoes.usuario
                };
                Log_Erro_AplicacaoService.Add(erros);
                status = false;
                ModelState.AddModelError("1001", "Erro não tratado");
                data = Formularios.CapturaModelErros(ModelState, true);
            }

            return Json(new { data = data, status = status, id = menuAcoes.id });
        }

        /// <summary>
        /// Controller get para listagem menu-acoes-aba
        /// </summary>
        /// <param name="_Parametros"></param>
        /// <returns></returns>
        [Authorize, HttpPost]
        public IActionResult MenuAcaoSelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                     Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            IdUsuarioLogado = new Guid(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value);
            var menu = MenuService.GetList(_Parametros_Busca_Navegacao.iditem, Guid.Empty, IdUsuarioLogado, _Parametros_Busca_Grid);
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = 10, recordsFiltered = menu.Count(), data = menu });
        }

        #endregion

        #region Menu-Sub

        /// <summary>
        /// Controller Get menu-areas-aba
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id_modulo"></param>
        /// <returns></returns>
        [Authorize, Route("Sistema_Menu/menu-areas-aba")]
        public IActionResult SubMenuAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                        Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            Modulo modulo = ModuloService.GetById(_Parametros_Busca_Navegacao.id);
            IEnumerable<Menu> menu = MenuService.GetList(_Parametros_Busca_Navegacao.id_modulo, Guid.Empty, _Parametros_Busca_Navegacao.id_usuario_logado, _Parametros_Busca_Grid);
            List<Menu_Sub> menuSub = MenuSubService.GetList((menu.ToList().Count > 0 ? menu.ToList()[0].id : Guid.Empty), "", _Parametros_Busca_Grid).ToList();
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            var viewMenu = new MenuViewModel()
            {
                modulos = modulo,
                menuLista = menu,
                menuSub = (menuSub.Count > 0 ? menuSub[0] : new Menu_Sub()),
                listaPermissoesMenus = listaPermissoesMenus
            };

            return View("menu-areas-aba", viewMenu);
        }

        [Authorize, HttpPost, Route("Sistema_Menu/menu-areas-aba")]
        public IActionResult SubMenuAbOperacoes(Menu_Sub menusub,
                                                Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            bool status = true;
            object data = null;
            try
            {
                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    var excluir = MenuSubService.GetById(menusub.id);
                    excluir.usuario = _Parametros_Busca_Navegacao.usuario;
                    excluir.excluido = true;
                    MenuSubService.Update(excluir);
                }
                else
                if (ModelState.IsValid)
                {
                    switch (_Parametros_Busca_Navegacao.acao)
                    {
                        case "I":
                            MenuSubService.Add(menusub);
                            break;
                        case "U":
                            menusub.id = _Parametros_Busca_Navegacao.id;
                            menusub.usuario = _Parametros_Busca_Navegacao.usuario;
                            MenuSubService.Update(menusub);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    data = Formularios.CapturaModelErros(ModelState);
                    status = false;
                }
            }
            catch (Exception ex)
            {
                Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao
                {
                    id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa,
                    origem = "Cadastro de Menus",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = menusub.ToString(),
                    metodo = "Sistema_menuController - MenuAcoesAba",
                    usuario = menusub.usuario
                };
                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro não tratado");
                data = Formularios.CapturaModelErros(ModelState, true);
                status = false;
            }

            return Json(new { data = data, id = menusub.id, status = status });
        }

        /// <summary>
        /// Controller Get para listagem menu-areas-aba
        /// </summary>
        /// <param name="_Parametros"></param>
        /// <returns></returns>
        [Authorize, HttpPost]
        public IActionResult MenuAreasSelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                      Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Menu_Sub> menu = MenuSubService.GetList(_Parametros_Busca_Navegacao.iditem, "", _Parametros_Busca_Grid).ToList();

            for (int i = 0; i < menu.Count; i++)
            {
                string descricao = menu[i].descricao;
                if (!String.IsNullOrEmpty(descricao))
                {
                    menu[i].descricao =(descricao.Length < 30 ? descricao : descricao.Substring(0, 29) );
                }else
                {
                    menu[i].descricao = string.Empty;
                }              
            }

            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = menu.Count(), recordsFiltered = 30, data = menu });
        }

        [Authorize, Route("Sistema_Menu/menu-areas-select")]
        public IActionResult SubMenuSelect(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                           Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            Menu_Sub menu = MenuSubService.GetById(_Parametros_Busca_Navegacao.id) ?? new Menu_Sub();
            List<Menu_Sub> menuSub = MenuSubService.GetList(menu.id_menu, "M", _Parametros_Busca_Grid).ToList();

            var viewMenu = new MenuViewModel()
            {
                menuSub = MenuSubService.GetById(_Parametros_Busca_Navegacao.id),
                menuSubLista = menuSub,
                paginaLista = PaginaService.GetList(Guid.Empty, _Parametros_Busca_Grid).ToList()
            };
            return View("menu-areas-select", viewMenu);
        }

        #endregion

        #region Paginas

        [Authorize, Route("Sistema_Menu/paginas-menus-aba")]
        public IActionResult PaginasMenuAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                            Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);

            var viewMenu = new MenuViewModel()
            {
                listaPermissoesMenus = listaPermissoesMenus,
                pagina = PaginaService.GetList(_Parametros_Busca_Navegacao.id, _Parametros_Busca_Grid)
            };
            return View("paginas-menus-aba", viewMenu);
        }

        /// <summary>
        /// Controller Get para listagem menu-areas-aba
        /// </summary>
        /// <param name="_Parametros"></param>
        /// <returns></returns>
        [Authorize, HttpPost]
        public IActionResult PaginasMenuSelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                        Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            var pag = PaginaService.GetList(Guid.Empty, _Parametros_Busca_Grid);
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = pag.Count(), recordsFiltered = 0, data = pag });
        }

        [Authorize, HttpPost, Route("Sistema_Menu/paginas-menus-aba")]
        public IActionResult PaginasMenuOperacoes(Pagina paginas,
                                                  Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            bool status = true;
            object data = null;
            try
            {
                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    var excluir = PaginaService.GetById(paginas.id);
                    excluir.usuario = _Parametros_Busca_Navegacao.usuario;
                    excluir.excluido = true;
                    PaginaService.Update(excluir);
                }
                else
                    if (ModelState.IsValid)
                {
                    switch (_Parametros_Busca_Navegacao.acao)
                    {
                        case "I":
                            PaginaService.Add(paginas);
                            break;
                        case "U":
                            paginas.id = _Parametros_Busca_Navegacao.id;
                            paginas.usuario = _Parametros_Busca_Navegacao.usuario;
                            PaginaService.Update(paginas);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    data = Formularios.CapturaModelErros(ModelState);
                    status = false;
                }
            }
            catch (Exception ex)
            {
                Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao
                {
                    id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa,
                    origem = "Cadastro de páginas",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = paginas.ToString(),
                    metodo = "Sistema_menuController - PaginasMenuOperacoes",
                    usuario = paginas.usuario
                };
                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro não tratado");
                data = Formularios.CapturaModelErros(ModelState, true);
                status = false;
            }

            return Json(new { data = data, id = paginas.id, status = status });
        }
        #endregion
    }
}