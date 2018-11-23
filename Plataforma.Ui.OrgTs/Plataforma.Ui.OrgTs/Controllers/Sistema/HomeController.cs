using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.Controllers.Sistema
{
    public class HomeController : BaseController
    {
        public IAgenda_AtendimentoService Agenda_AtendimentoService { get; private set; }
        public IPessoaService PessoaService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        private IHostingEnvironment hostingEnv;


        public Guid idEmpresa { get; private set; }
        public Guid idPessoa { get; private set; }


        public HomeController(IAgenda_AtendimentoService agenda_AtendimentoService,
                              IPessoaService pessoaService,
                              ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                              IHostingEnvironment env,
                              IMapper mapper) : base(mapper)
        {
            Agenda_AtendimentoService = agenda_AtendimentoService;
            PessoaService = pessoaService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            this.hostingEnv = env;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Claim> dados = ((ClaimsIdentity)User.Identity).Claims.ToList();
            Parametros_Busca_Navegacao _Parametros_Busca_Navegacao = new Parametros_Busca_Navegacao()
            {
                id_empresa = new Guid(dados.FirstOrDefault(x => x.Type == "id_empresa_atual").Value),
                id_pessoa = new Guid(dados.FirstOrDefault(x => x.Type == "id_pessoa").Value),
            };

            idEmpresa = _Parametros_Busca_Navegacao.id_empresa;
            idPessoa = _Parametros_Busca_Navegacao.id_pessoa;

            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 10000
            };

            ViewBag.bancoDadosDev = this.hostingEnv.IsDevelopment();
            ViewBag.bancoDadosProd = this.hostingEnv.IsProduction();
            return View("Index");
        }

        [Authorize]
        public IActionResult Erro(int erroCode)
        {
            return View(erroCode);
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        public IActionResult AcessoNegadoAba()
        {
            return View();
        }
    }
}