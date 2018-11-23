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
using System.Globalization;
using System.Linq;
using System.Text;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;
using Parametros_Busca_NavegacaoAlias = Plataforma.Domain.Entities.NotMapped.Busca_Generica.Parametros_Busca_Navegacao;

namespace Plataforma.Ui.OrgTs.Controllers.Sistema
{ 
    public class Sistema_AgendaAtendimentoController : BaseController
    {
        public IAgenda_AtendimentoService Agenda_AtendimentoService { get; private set; }
        public IPessoaService PessoaService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuarioService UsuarioService { get; private set; }
        private IHostingEnvironment hostingEnv;

        public Sistema_AgendaAtendimentoController(IAgenda_AtendimentoService agenda_AtendimentoService,
                                                   IPessoaService pessoaService,
                                                   IUsuarioService usuarioService,
                                                   ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                                   IHostingEnvironment env,
                                                   IMapper mapper) : base(mapper)
        {
            Agenda_AtendimentoService = agenda_AtendimentoService;
            PessoaService = pessoaService;
            UsuarioService = usuarioService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            this.hostingEnv = env;
        }

        [Authorize]
        public IActionResult Index(Parametros_Busca_NavegacaoAlias _Parametros_Busca_Navegacao)
        {
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, new Parametros_Busca_Grid());
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, new Parametros_Busca_Grid());
            return View(listaPermissoesMenus);
        }

        [Authorize, HttpPost]
        public IActionResult SelecionarTodos(Parametros_Busca_NavegacaoAlias _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 1000
            };

            Parametros_Pessoa parametros_Pessoa = new Parametros_Pessoa() { id_pessoa_empresa = _Parametros_Busca_Navegacao.id_empresa };
            List<Agenda_Atendimento> agendaatendimento = Agenda_AtendimentoService.GetList(parametros_Pessoa, _Parametros_Busca_Grid).ToList();

            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = agendaatendimento.Count(), recordsFiltered = 0, data = agendaatendimento });
        }

        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }

        [Authorize, Route("Sistema_AgendaAtendimento/AgendaAtendimento-aba")]
        public IActionResult AgendaAtendimentoAba(Parametros_Busca_NavegacaoAlias _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 1000
            };

            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, new Parametros_Busca_Grid());
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, new Parametros_Busca_Grid());
            Agenda_Atendimento agendaAtendimento = Agenda_AtendimentoService.GetById(_Parametros_Busca_Navegacao.id);
            var agendaAtend = new AgendaAtendimentoViewModel()
            {
                agendaAtendimento = agendaAtendimento ?? new Agenda_Atendimento(),
                listaPessoa = PessoaService.GetListOptions(Guid.Empty, _Parametros_Busca_Navegacao.id_empresa, false, false, false, false, false, false, false, true, false, false, false, false, false).ToList(),
                listaPermissoesMenus = listaPermissoesMenus
            };
            return View("AgendaAtendimento-aba", agendaAtend);
        }

        
        [Authorize, HttpPost, Route("Sistema_AgendaAtendimento/AgendaAtendimento-aba"), ValidateAntiForgeryToken]
        public IActionResult AgendaAtendimentoAbaOperacoes(Agenda_Atendimento agendAtendimento, Guid id_modulo, Guid id_empresa, String acao, string usuario)
        {
            Parametros_Pessoa _Parametros_Pessoa = new Parametros_Pessoa()
            {
                id_pessoa_empresa = id_empresa
            };

            Agenda_AtendimentoService.AddtesteLog(agendAtendimento.data.ToString());
            bool status = true;
            object data = null;
            try
            {
                if (acao == "D")
                {
                    Agenda_Atendimento excluirAgenda = Agenda_AtendimentoService.GetById(id_modulo);
                    excluirAgenda.excluido = true;
                    Agenda_AtendimentoService.Update(excluirAgenda);
                }
                else
                    if (ModelState.IsValid)
                {
                    switch (acao)
                    {
                        case "I":
                            if (Agenda_AtendimentoService.ValidaAgendamento(_Parametros_Pessoa, Convert.ToDateTime(agendAtendimento.data), Convert.ToDateTime(agendAtendimento.hora)))
                            {
                                ModelState.AddModelError("1002", "Já existe um agendamento dentro desse período!");
                                data = Formularios.CapturaModelErros(ModelState);
                                status = false;
                                break;
                            }

                            DateTime dateTime = DateTime.UtcNow;
                            TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");
                            DateTime dateTimeUsa = TimeZoneInfo.ConvertTime(dateTime, hrBrasilia);

                            string anoAtual = DateTime.Now.Year.ToString();

                            int totalAgendamentos = Agenda_AtendimentoService.Count(_Parametros_Pessoa, "", "");
                            agendAtendimento.protocolo = totalAgendamentos.ToString() + dateTimeUsa.Day.ToString() + dateTimeUsa.Month.ToString() + dateTimeUsa.Year.ToString();
                            agendAtendimento.usuario = usuario;

                            Agenda_AtendimentoService.Add(agendAtendimento);
                            data = new { retornoAplicacao = agendAtendimento.protocolo };
                            break;
                        case "U":
                            var updateAgenda = Agenda_AtendimentoService.GetById(id_modulo);
                            agendAtendimento.protocolo = updateAgenda.protocolo;
                            agendAtendimento.usuario = usuario;

                            // Update por movimentação no calendario
                            if (agendAtendimento.id_pessoa_empresa == null && agendAtendimento.id_pessoa_responsavel == null && String.IsNullOrEmpty(agendAtendimento.nome_solicitante) && String.IsNullOrEmpty(agendAtendimento.observacao) && String.IsNullOrEmpty(agendAtendimento.hora))
                            {
                                DateTime dataUpdate = Convert.ToDateTime(agendAtendimento.data);
                                agendAtendimento = updateAgenda;
                                agendAtendimento.data = dataUpdate;
                            }

                            if (Agenda_AtendimentoService.ValidaAgendamento(_Parametros_Pessoa, Convert.ToDateTime(agendAtendimento.data), Convert.ToDateTime(agendAtendimento.hora)))
                            {
                                ModelState.AddModelError("1002", "Já existe um agendamento dentro desse período!");
                                data = Formularios.CapturaModelErros(ModelState);
                                status = false;
                                break;
                            }
                            Agenda_AtendimentoService.Update(agendAtendimento);
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
                Log_Erro_AplicacaoService.Add(new Log_Erro_Aplicacao()
                {
                    id_pessoa_empresa = id_empresa,
                    origem = "Agenda Atendimento ",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = agendAtendimento.ToString(),
                    metodo = "Sistema_AgendaAtendimentoController - AgendaAtendimentoAbaOperacoes",
                    usuario = agendAtendimento.usuario
                });
               
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = agendAtendimento.id });
        }

        [Authorize]
        public IActionResult CalendarioAgendamento(Guid id_empresa, string end,  string start)
        {
            Parametros_Pessoa _Parametros_Pessoa = new Parametros_Pessoa()
            {
                id_pessoa_empresa = id_empresa
            };

            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
               length = 1000
            };

            List<object> lista = new List<object>();
            string montaData = string.Empty;
            StringBuilder teste = new StringBuilder();
            try
            {
                List<Agenda_Atendimento> listaAgendamentos = Agenda_AtendimentoService.GetList(_Parametros_Pessoa, _Parametros_Busca_Grid).ToList();
                foreach (Agenda_Atendimento itens in listaAgendamentos)
                {
                    DateTime data = DateTime.Parse(itens.data.ToString());
                    CultureInfo culture = new CultureInfo("en-US");                    
                    lista.Add(new { id = itens.id, title = itens.nome_solicitante, start = String.Format(culture, "{0}/{1}/{2}", data.Year, data.Month, data.Day), observacao = (String.IsNullOrEmpty(itens.observacao) ? "Nenhuma observação" : itens.observacao), nomeResponsavel = itens.Pessoa_Responsavel.nome_fantasia_apelido, hora = itens.hora });
                }
            }
            catch (Exception ex)
            {
                Log_Erro_AplicacaoService.Add(new Log_Erro_Aplicacao()
                {
                    id_pessoa_empresa = id_empresa,
                    origem = "Agenda Atendimento ",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = id_empresa.ToString() + " - end: " + end + " - start: " + start + " - montaData: " + montaData,
                    metodo = "Sistema_AgendaAtendimentoController - CalendarioAgendamento",
                    usuario = ""
                });                
                ModelState.AddModelError("1001", "Erro Não Tratado");              
                throw;
            }
            return Json(lista);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public IActionResult CalendarioAgendamentoOperacoes(Agenda_Atendimento agendAtendimento, Guid id_modulo, Guid id_empresa, String acao, string usuario)
        {
            Parametros_Pessoa _Parametros_Pessoa = new Parametros_Pessoa()
            {
                id_pessoa_empresa = id_empresa
            };

            bool status = true;
            object data = null;
            try
            {                
                var updateAgenda = Agenda_AtendimentoService.GetById(id_modulo);
                agendAtendimento.protocolo = updateAgenda.protocolo;
                agendAtendimento.usuario = usuario;

                // Update por movimentação no calendario
                if (agendAtendimento.id_pessoa_empresa == null && agendAtendimento.id_pessoa_responsavel == null && String.IsNullOrEmpty(agendAtendimento.nome_solicitante) && String.IsNullOrEmpty(agendAtendimento.observacao) && String.IsNullOrEmpty(agendAtendimento.hora))
                {
                    DateTime dataUpdate = Convert.ToDateTime(agendAtendimento.data);
                    agendAtendimento = updateAgenda;
                    agendAtendimento.data = dataUpdate;
                }

                if (Agenda_AtendimentoService.ValidaAgendamento(_Parametros_Pessoa, Convert.ToDateTime(agendAtendimento.data), Convert.ToDateTime(agendAtendimento.hora)))
                {
                    ModelState.AddModelError("1002", "Já existe um agendamento dentro desse período!");
                    data = Formularios.CapturaModelErros(ModelState);
                    status = false;
                }
                Agenda_AtendimentoService.Update(agendAtendimento);
                
            }
            catch (Exception ex)
            {
                Log_Erro_AplicacaoService.Add(new Log_Erro_Aplicacao()
                {
                    id_pessoa_empresa = id_empresa,
                    origem = "Agenda Atendimento ",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = agendAtendimento.ToString(),
                    metodo = "Sistema_AgendaAtendimentoController - CalendarioAgendamentoOperacoes",
                    usuario = agendAtendimento.usuario
                });
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = agendAtendimento.id });
        }
    }
}