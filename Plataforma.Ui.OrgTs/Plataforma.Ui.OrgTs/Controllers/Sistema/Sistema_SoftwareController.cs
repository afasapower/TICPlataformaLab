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
    public class Sistema_SoftwareController : BaseController
    {
        public ISoftwareService SoftwareService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }

        public Sistema_SoftwareController(ISoftwareService softwareService,
                                          ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                          IUsuarioService usuarioService,
                                          IMapper mapper) : base(mapper)
        {
            SoftwareService = softwareService;
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
            List<Software> software = SoftwareService.GetList(_Parametros_Busca_Navegacao.id, _Parametros_Busca_Grid).ToList();

            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = software.Count(), recordsFiltered = 0, data = software });
        }
        #endregion

        #region Cadastro de Software

        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }


        [Authorize, Route("Sistema_Software/software-aba")]
        public IActionResult SoftwareAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 1000
            };

            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            SoftwareViewModel software = new SoftwareViewModel()
            {
                software = SoftwareService.GetById(_Parametros_Busca_Navegacao.id) ?? new Software(),
                listaPermissoesMenus = listaPermissoesMenus
            };

            return View("software-aba", software);
        }

       
        [Authorize, Route("Sistema_Software/software-aba"), HttpPost]
        public IActionResult SoftwareAbaOperacoes(CadastroSoftwareViewModel cadastroSoftware,
                                                  Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Software software = new Software()
            {
                id_software = _Parametros_Busca_Navegacao.id,
                nome_software = cadastroSoftware.nome_software,
                fabricante = cadastroSoftware.fabricante,
                versao_software = cadastroSoftware.versao_software,
                open_source = cadastroSoftware.open_source,
                fg_ativo = cadastroSoftware.fg_ativo
            };

            bool status = true;
            object data = null;
            try
            {
              if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    // Exclui o Laboratorio
                    var excluir = SoftwareService.GetById(_Parametros_Busca_Navegacao.id);
                    excluir.fg_ativo = true;
                    SoftwareService.Update(excluir);
                }
                else
                    if (ModelState.IsValid)
                    {
                        switch (_Parametros_Busca_Navegacao.acao)
                        {
                            case "I":
                                //Grava laboratorio na tabela laboratorio
                                software.data_aquisicao = cadastroSoftware.data_aquisicao;
                                software.data_vencimento = cadastroSoftware.data_vencimento;
                                SoftwareService.Add(software);
                                break;
                            case "U":
                                // Edita laboratorio na tabela laboratorio
                                software.data_aquisicao = cadastroSoftware.data_aquisicao;
                                software.data_vencimento = cadastroSoftware.data_vencimento;
                                SoftwareService.Update(software);
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
                erros.origem = "Cadastro de Software";
                erros.mensagem = ex.Message + " - " + ex.StackTrace;
                erros.objeto = software.ToString();
                erros.metodo = "Sistema_SoftwareController - SoftwareAbaOperacoes";
                Log_Erro_AplicacaoService.Add(erros);
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = software.id_software });
        }
        #endregion
    }
}
