using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.Controllers.Sistema
{
    public class Sistema_GrupoController : BaseController
    {
        public IGrupoService GrupoService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IModuloService ModuloService { get; private set; }
        public IMenuService MenuService { get; private set; }
        public IMenu_SubService Menu_SubService { get; private set; }
        public IPermissao_GrupoService Permissao_GrupoService { get; private set; }
        public IPermissao_Grupo_EtapaService Permissao_Grupo_EtapaService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }

        public Sistema_GrupoController(IGrupoService grupoService,
                                       ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                       IModuloService moduloService,
                                       IMenuService menuService,
                                       IMenu_SubService menu_SubService,
                                       IPermissao_GrupoService permissao_GrupoService,
                                       IPermissao_Grupo_EtapaService permissao_Grupo_EtapaService,
                                       IUsuarioService usuarioService,
                                       IMapper mapper) : base(mapper)

        {
            GrupoService = grupoService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            ModuloService = moduloService;
            MenuService = menuService;
            Menu_SubService = menu_SubService;
            Permissao_GrupoService = permissao_GrupoService;
            Permissao_Grupo_EtapaService = permissao_Grupo_EtapaService;
            UsuarioService = usuarioService;
        }

        #region Cadastro de Grid

        [Authorize]
        public IActionResult Index(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                   Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);
            return View(listaPermissoesMenus);
        }

        [Authorize, HttpPost]
        public IActionResult SelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                             Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            var grupo = GrupoService.GetList(_Parametros_Busca_Navegacao.id_empresa, Guid.Empty, _Parametros_Busca_Grid);
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = grupo.Count(), recordsFiltered = 0, data = grupo });
        }
        #endregion

        #region Cadastro de Grupo
        // Página mestra de abas
        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }

        [Authorize, Route("Sistema_Grupo/grupo-aba")]
        public IActionResult GrupoAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                      Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            var viewGrupo = new GrupoViewModel()
            {
                grupo = GrupoService.GetById(_Parametros_Busca_Navegacao.id),
                listaPermissoesMenus = listaPermissoesMenus
            };
            return View("grupo-aba", viewGrupo);
        }

        [Authorize, HttpPost, Route("Sistema_Grupo/grupo-aba")]
        public IActionResult GrupoOperacoes(Grupo grupo,
                                            Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            bool status = true;
            object data = null;
            try
            {
                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    // Exclui o Grupo
                    var excluir = GrupoService.GetById(grupo.id);
                    excluir.excluido = true;
                    GrupoService.Update(excluir);
                }
                else
                    if (ModelState.IsValid)
                {
                    switch (_Parametros_Busca_Navegacao.acao)
                    {
                        case "I":
                            //Grava grupo na tabela Grupo
                            GrupoService.Add(grupo);
                            break;
                        case "U":
                            // Edita grupo na tabela Grupo   
                            GrupoService.Update(grupo);
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
                Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao()
                {
                    id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa,
                    origem = "Cadastro de Grupo",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = grupo.ToString(),
                    metodo = "grupoController - GrupoOperacoes",
                    usuario = grupo.usuario
                };

                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = grupo.id });
        }

        [Authorize, Route("Sistema_Grupo/grupo-permissao-aba")]
        public IActionResult GrupoPermissaoAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                               Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 100
            };
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);
            List<Retorno_Permissao_Grupo_Pagina> permissaoGrupo = Permissao_GrupoService.GetListPermissaoPagina(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, _Parametros_Busca_Navegacao.id, _Parametros_Busca_Grid).ToList();

            var viewGrupo = new GrupoViewModel()
            {
                permissaoGrupoPaginas = (permissaoGrupo.Count > 0 ? permissaoGrupo[0] : new Retorno_Permissao_Grupo_Pagina()),
                permissaoGrupo = Permissao_GrupoService.GetById(_Parametros_Busca_Navegacao.id),
                listaPermissoesMenus = listaPermissoesMenus,
                listaModulos = ModuloService.GetList(_Parametros_Busca_Grid).ToList()
            };
            return View("grupo-permissao-aba", viewGrupo);
        }

        [Authorize, Route("Sistema_Grupo/grupo-permissao-aba"), HttpPost]
        public JsonResult GrupoPermissaoOperacoes(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                  Parametros_Busca_Grid _Parametros_Busca_Grid,
                                                  List<Guid> paginas_selecionadas,
                                                  Guid menu_select)
        {
            _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 1000
            };

            List<Retorno_Permissao_Grupo_Pagina> paginasAtuais = Permissao_GrupoService.GetListPermissaoPagina(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, menu_select, _Parametros_Busca_Grid).ToList();

            bool status = true;
            object data = null;
            try
            {
                switch (_Parametros_Busca_Navegacao.acao)
                {
                    case "I":
                        foreach (var item in paginas_selecionadas)
                        {
                            if (Permissao_GrupoService.GetList(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, _Parametros_Busca_Navegacao.iditem, _Parametros_Busca_Grid).ToList().Count == 0)
                            {
                                Permissao_Grupo permissoes = new Permissao_Grupo()
                                {
                                    id_grupo = _Parametros_Busca_Navegacao.id_modulo,
                                    ler = false,
                                    incluir = false,
                                    atualizar = false,
                                    deletar = false,
                                    upload = false,
                                    download = false,
                                    outros = false,
                                    usuario = _Parametros_Busca_Navegacao.usuario,
                                    id_pagina = item
                                };

                                Permissao_GrupoService.Add(permissoes);
                            }
                        }
                        break;
                    case "U":
                        foreach (var item in paginasAtuais)
                        {
                            if (!paginas_selecionadas.Any(x => x == (Guid)item.id_pagina))
                            {
                                Permissao_GrupoService.Remove(Permissao_GrupoService.GetById(item.id));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                //Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao();
                //erros.id_pessoa_empresa = id_pessoa_empresa;
                //erros.origem = "Cadastro de Grupo";
                //erros.mensagem = ex.Message + " - " + ex.StackTrace;
                //erros.objeto = grupo.ToString();
                //erros.metodo = "grupoController - GrupoOperacoes";
                //erros.usuario = grupo.usuario;
                //Log_Erro_AplicacaoService.Add(erros);
                //ModelState.AddModelError("1001", "Erro Não Tratado");
                //status = false;
                //data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = "" });
        }

        [Authorize]
        public JsonResult RetornaMenu(Guid valor,
                                      Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                      Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 1000
            };

            List<Guid> valores = new List<Guid>()
            {
                Guid.Empty
            };

            List<string> descricao = new List<string>()
            {
                "Selecione um submenu"
            };

            //descricao.Add("Selecione um submenu");
            List<Menu> menu = MenuService.GetList(valor, Guid.Empty, Guid.Empty, _Parametros_Busca_Grid).ToList();
            foreach (var item in menu)
            {
                valores.Add(item.id);
                descricao.Add(item.nome);
            }
            if (valores.Count == 1)
            {
                descricao = new List<string>();
                valores = new List<Guid>()
                {
                    Guid.Empty
                };

                descricao.Add("Nenhuma submenu cadastrado");
            }
            return Json(new { valores = valores, descricao = descricao });
        }

        [Authorize, Route("Sistema_Grupo/grupo-permissao-area-select")]
        public IActionResult RetornaMenuSub(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                            Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 1000
            };
            var viewGrupo = new GrupoViewModel()
            {
                listaMenuSub = Menu_SubService.GetListPaginaDisponivel(_Parametros_Busca_Navegacao.id, string.Empty, _Parametros_Busca_Grid).ToList(),
                listaPermissaoGrupo = Permissao_GrupoService.GetList(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, Guid.Empty, _Parametros_Busca_Grid).ToList(),
                listaPermissaoGrupoPaginas = Permissao_GrupoService.GetListPermissaoPagina(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, _Parametros_Busca_Navegacao.id, _Parametros_Busca_Grid).ToList()
            };
            return View("grupo-permissao-area-select", viewGrupo);
        }

        [Authorize, Route("Sistema_Grupo/grupo-permissao-area-permissoes")]
        public IActionResult RetornaPermissoesPaginas(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                      Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Permissao_Grupo> listaPermissao = Permissao_GrupoService.GetList(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, _Parametros_Busca_Navegacao.id, _Parametros_Busca_Grid).ToList();
            Permissao_Grupo permissao = (listaPermissao.Count > 0 ? listaPermissao[0] : new Permissao_Grupo());
            return View("grupo-permissao-area-permissoes", permissao);
        }

        [Authorize, Route("Sistema_Grupo/grupo-permissao-area-permissoes"), HttpPost]
        public IActionResult PermissoesPaginasOperacoes(Permissao_Grupo permissoes,
                                                        Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                        Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            bool status = true;
            object data = null;
            try
            {
                permissoes.id_grupo = _Parametros_Busca_Navegacao.id_modulo;
                List<Permissao_Grupo> listaPermissao = Permissao_GrupoService.GetList(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.id_modulo, permissoes.id_pagina, _Parametros_Busca_Grid).ToList();

                if (listaPermissao.Count > 0)
                {
                    // Upideita permissão no grupo   
                    permissoes.id = listaPermissao[0].id;
                    Permissao_GrupoService.Update(permissoes);
                }
                else
                {
                    // Grava permissão no grupo
                    Permissao_GrupoService.Add(permissoes);
                }
            }
            catch (Exception ex)
            {
                Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao()
                {
                    id_pessoa_empresa = _Parametros_Busca_Navegacao.id_empresa,
                    origem = "Permissões de Grupo",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = permissoes.ToString(),
                    metodo = "grupoController - PermissoesPaginasOperacoes",
                    usuario = permissoes.usuario,

                };

                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data });
        }

        [Authorize, HttpPost]
        public IActionResult PermissaoGruposelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                           Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Menu> grupo = Permissao_GrupoService.GetListPermissaoMenu(_Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Navegacao.iditem, _Parametros_Busca_Grid).ToList();
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = grupo.Count(), recordsFiltered = 0, data = grupo });
        }
    }
    #endregion
}
