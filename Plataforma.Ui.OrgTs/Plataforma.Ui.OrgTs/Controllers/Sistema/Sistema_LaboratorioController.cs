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
    public class Sistema_LaboratorioController : BaseController
    {
        public ILaboratorioService LaboratorioService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }

        public Sistema_LaboratorioController(ILaboratorioService laboratorioService,
                                             ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                             IUsuarioService usuarioService,
                                             IMapper mapper) : base(mapper)
        {
            LaboratorioService = laboratorioService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            UsuarioService = usuarioService;
        }

        #region  Cadastro de Grid
        [Authorize]
        public IActionResult Index(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                   Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            return View(listaPermissoesMenus);
        }

        [Authorize, HttpPost]
        public IActionResult selecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid();
            List<Laboratorio> laboratorio = LaboratorioService.GetList(_Parametros_Busca_Navegacao.id, _Parametros_Busca_Grid).ToList();

            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = laboratorio.Count(), recordsFiltered = 0, data = laboratorio });
        }
        #endregion

        #region Cadastro de Cidade

        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }


        [Authorize, Route("Sistema_Laboratorio/laboratorio-aba")]
        public IActionResult LaboratorioAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 1000
            };

            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            LaboratorioViewModel laboratorio = new LaboratorioViewModel()
            {
                laboratorio = LaboratorioService.GetById(_Parametros_Busca_Navegacao.id) ?? new Laboratorio(),
                listaPermissoesMenus = listaPermissoesMenus
            };

            return View("laboratorio-aba", laboratorio);
        }

       
        [Authorize, Route("Sistema_Laboratorio/laboratorio-aba"), HttpPost]
        public IActionResult LaboratorioAbaOperacoes(CadastroLaboratorioViewModel cadastroLaboratorio,
                                                     Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Laboratorio laboratorio = new Laboratorio()
            {
                id_laboratorio = _Parametros_Busca_Navegacao.id,
                nome = cadastroLaboratorio.nome,
                unidade = cadastroLaboratorio.unidade,
                fg_ativo = cadastroLaboratorio.fg_ativo
            };

            bool status = true;
            object data = null;
            try
            {
              if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    // Exclui o Laboratorio
                    var excluir = LaboratorioService.GetById(_Parametros_Busca_Navegacao.id);
                    excluir.fg_ativo = true;
                    LaboratorioService.Update(excluir);
                }
                else
                    if (ModelState.IsValid)
                    {
                        switch (_Parametros_Busca_Navegacao.acao)
                        {
                            case "I":
                                //Grava laboratorio na tabela laboratorio
                                LaboratorioService.Add(laboratorio);
                                break;
                            case "U":
                                // Edita laboratorio na tabela laboratorio   
                                LaboratorioService.Update(laboratorio);
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
                Log_Erro_Aplicacao erros = new Log_Erro_Aplicacao();
                erros.id_pessoa_empresa = _Parametros_Busca_Navegacao.id_empresa;
                erros.origem = "Cadastro de Laboratorio";
                erros.mensagem = ex.Message + " - " + ex.StackTrace;
                erros.objeto = laboratorio.ToString();
                erros.metodo = "Sistema_LaboratorioController - LaboratorioAbaOperacoes";
                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = laboratorio.id_laboratorio });
        }
        #endregion
    }
}
