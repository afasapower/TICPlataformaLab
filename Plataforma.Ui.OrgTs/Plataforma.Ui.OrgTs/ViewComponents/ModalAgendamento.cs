using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class ModalAgendamento: ViewComponent
    {
        public IAgenda_AtendimentoService Agenda_AtendimentoService { get; private set; }
        public IPessoaService PessoaService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }

        public ModalAgendamento(IAgenda_AtendimentoService agenda_AtendimentoService,
                                IPessoaService pessoaService,
                                ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                IUsuario_EmpresaService usuario_EmpresaService)
        {
            Agenda_AtendimentoService = agenda_AtendimentoService;
            PessoaService = pessoaService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            Usuario_EmpresaService = usuario_EmpresaService;
        }

        public IViewComponentResult Invoke(Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            _Parametros_Busca_Grid = new Parametros_Busca_Grid
            {
                length = 1000
            };

            // Captura ID do usuario logado
            string id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;
            //usuario_empresa_dal usuarioempresa = new usuario_empresa_dal();

            List<Usuario_Empresa> selectusuarioempresa = Usuario_EmpresaService.GetList(new Guid(id), Guid.Empty, null).ToList();

            Guid id_empresa = (selectusuarioempresa.Count > 0 ? (Guid)selectusuarioempresa[0].id_pessoa_empresa : Guid.Empty);

            string idEmpresa = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id_pessoa_empresa").Value;

            id_empresa = (String.IsNullOrEmpty(idEmpresa) ? Guid.Empty : new Guid(idEmpresa));

            // Retorna na ViewModel          
            var agendaAtend = new AgendaAtendimentoViewModel()
            {
                agendaAtendimento =  new Agenda_Atendimento(),                
                //listaPessoa = PessoaService.GetList(Guid.Empty, id_empresa, "PA").ToList()
                listaPessoa = PessoaService.GetListOptions(Guid.Empty, id_empresa, false, false, false, false, false, false, false, true, false, false, false, false, false, _Parametros_Busca_Grid).ToList()
            };
            return View(agendaAtend);
        }
    }
}