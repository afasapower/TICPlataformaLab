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
    public class Sistema_PermissaoUsuarioEmpresaController : BaseController
    {
        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }      
        public IPessoaService PessoaService { get; private set; }
        public IEmpresa_UsuarioService Empresa_UsuarioService { get; private set; }

        public Sistema_PermissaoUsuarioEmpresaController(IUsuario_EmpresaService usuario_EmpresaService,
                                                         IUsuarioService usuarioService,
                                                         ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                                         IPessoaService pessoaService,
                                                         IEmpresa_UsuarioService empresa_UsuarioService,
                                                         IMapper mapper) : base(mapper)
        {
            Usuario_EmpresaService = usuario_EmpresaService;
            UsuarioService = usuarioService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            PessoaService = pessoaService;
            Empresa_UsuarioService = empresa_UsuarioService;
        }

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
            var usuario = UsuarioService.GetList(_Parametros_Busca_Navegacao.id_empresa, Guid.Empty, _Parametros_Busca_Grid);

            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = usuario.Count(), recordsFiltered = 0, data = usuario });
        }

        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }

        [Authorize, Route("Sistema_PermissaoUsuarioEmpresa/PermissaoUsuarioEmpresa-aba")]
        public IActionResult PermissaoUsuarioEmpresaAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                        Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            Configuracao_Sistema _Configuracao_Sistema = new Configuracao_Sistema()
            {
                id_empresa_unifacef = Startup.id_empresa_unifacef
            };

            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            Usuario usuarioAtual = UsuarioService.GetById(_Parametros_Busca_Navegacao.id_modulo) ?? new Usuario();
            List<Dados_Usuario_Empresa> listaUsuarioEmpresa = Empresa_UsuarioService.GetList(_Parametros_Busca_Navegacao.id_usuario_logado, _Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Grid, _Configuracao_Sistema).ToList();

            var usuario = new PermissaoUsuarioEmpresaViewModel()
            {
                usuarioEmpresa = Usuario_EmpresaService.GetById(_Parametros_Busca_Navegacao.id) ?? new Usuario_Empresa(),
                usuario = usuarioAtual,
                listaDadosUsuarioEmpresa = listaUsuarioEmpresa,
                listaPermissoesMenus = listaPermissoesMenus,
                pessoa = PessoaService.GetById(usuarioAtual.id_pessoa)
            };
            return View("PermissaoUsuarioEmpresa-aba", usuario);
        }
   
        [Authorize, Route("Sistema_PermissaoUsuarioEmpresa/PermissaoUsuarioEmpresa-aba"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult PermissaoUsuarioOperacoes(Usuario_Empresa usuarioEmpresa,
                                                       Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                       Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            bool status = true;
            object data = null;

            try
            {
                usuarioEmpresa.usuario = _Parametros_Busca_Navegacao.usuario;

                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    var excluirUsuario = Usuario_EmpresaService.GetById(usuarioEmpresa.id);
                    excluirUsuario.excluido = true;
                    Usuario_EmpresaService.Update(excluirUsuario);
                }
                else
                    if (ModelState.IsValid)
                {
                    switch (_Parametros_Busca_Navegacao.acao)
                    {
                        case "I":
                            // Verifica se o usuário já esta cadastrado na empresa
                            var existeUsuario = Usuario_EmpresaService.GetList(_Parametros_Busca_Navegacao.id_modulo, usuarioEmpresa.id_pessoa_empresa, _Parametros_Busca_Grid);

                            if (existeUsuario.Count() == 0)
                            {
                                usuarioEmpresa.id_usuario = _Parametros_Busca_Navegacao.id_modulo;
                                usuarioEmpresa.id_pessoa_empresa = usuarioEmpresa.id_pessoa_empresa;
                                Usuario_EmpresaService.Add(usuarioEmpresa);
                            }
                            else
                            {
                                status = false;
                                ModelState.AddModelError("1001", "Usuário já vinculado com a empresa!");
                                data = Formularios.CapturaModelErros(ModelState, true);
                            }                           
                            break;
                        case "U":
                            // Verifica se o usuário já esta cadastrado na empresa
                            var alteracaoExisteUsuario = Usuario_EmpresaService.GetList(_Parametros_Busca_Navegacao.id_modulo, usuarioEmpresa.id_pessoa_empresa, _Parametros_Busca_Grid);

                            if (alteracaoExisteUsuario.Count() == 0)
                            {
                                usuarioEmpresa.id_usuario = _Parametros_Busca_Navegacao.id_modulo;
                                Usuario_EmpresaService.Update(usuarioEmpresa);
                            }
                            else
                            {
                                status = false;
                                ModelState.AddModelError("1001", "Usuário já vinculado com a empresa!");
                                data = Formularios.CapturaModelErros(ModelState, true);
                            }                            
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
                    id_pessoa_empresa = _Parametros_Busca_Navegacao.id_empresa,
                    origem = "Cadastro Permissao Usuario Empresa",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = usuarioEmpresa.ToString(),
                    metodo = "UsuarioController - UsuarioOperacoes",
                    usuario = _Parametros_Busca_Navegacao.usuario
                };
                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = _Parametros_Busca_Navegacao.id });
        }

        [Authorize, HttpPost]
        public IActionResult SelecionarUsuariosTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                     Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Usuario_Empresa> usuarioEmp = Usuario_EmpresaService.GetList(_Parametros_Busca_Navegacao.iditem, Guid.Empty, _Parametros_Busca_Grid).ToList();

            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = usuarioEmp.Count(), recordsFiltered = 0, data = usuarioEmp });
        }

    }
}
