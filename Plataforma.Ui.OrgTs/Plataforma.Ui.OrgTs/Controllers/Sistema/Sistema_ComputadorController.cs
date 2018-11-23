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
    public class Sistema_ComputadorController : BaseController
    {
        public IComputadorService ComputadorService { get; private set; }
        public ISoftwareService SoftwareService { get; private set; }
        public ILaboratorioService LaboratorioService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="computadorService"></param>
        /// <param name="log_Erro_AplicacaoService"></param>
        /// <param name="usuarioService"></param>
        /// <param name="mapper"></param>
        public Sistema_ComputadorController(IComputadorService computadorService,
                                            ISoftwareService softwareService,
                                            ILaboratorioService laboratorioService,
                                            ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                            IUsuarioService usuarioService,
                                            IMapper mapper) : base(mapper)
        {
            ComputadorService = computadorService;
            SoftwareService = softwareService;
            LaboratorioService = laboratorioService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            UsuarioService = usuarioService;
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
            var computador = ComputadorService.GetList(Guid.Empty, _Parametros_Busca_Grid);
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = computador.Count(), recordsFiltered = 0, data = computador });
        }

        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }

        [Authorize, Route("Sistema_Computador/computador-aba")]
        public IActionResult ComputadorAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                           Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, _Parametros_Busca_Grid);
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, _Parametros_Busca_Grid);

            ComputadorViewModel computador = new ComputadorViewModel()
            {
                computador = ComputadorService.GetById(_Parametros_Busca_Navegacao.id) ?? new Computador(),
                listaLaboratorio = LaboratorioService.GetList(Guid.Empty, _Parametros_Busca_Grid).ToList(),
                listaSoftware = SoftwareService.GetList(Guid.Empty, _Parametros_Busca_Grid).ToList(),
                listaPermissoesMenus = listaPermissoesMenus
            };         
            return View("computador-aba", computador);
        }
        
        [Authorize, Route("Sistema_Computador/computador-aba"), HttpPost]        
        public IActionResult PaisAbaOperacoes(CadastroComputadorViewModel cadastroComputador, Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Computador computador = new Computador()
            {
                id_laboratorio = cadastroComputador.id_laboratorio,
                numero_computador = cadastroComputador.numero_computador,
                fg_ativo = cadastroComputador.fg_ativo
            };
            return null;
        }
        
    }
}