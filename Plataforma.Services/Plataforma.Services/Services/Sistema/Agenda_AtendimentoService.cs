using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Agenda_AtendimentoService
    /// </summary>
    public class Agenda_AtendimentoService : ServiceBase<Agenda_Atendimento>, IAgenda_AtendimentoService
    {
        public IAgenda_AtendimentoRepository Agenda_AtendimentoRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="agenda_AtendimentoRepository"></param>
        /// <param name="context"></param>
        public Agenda_AtendimentoService(IAgenda_AtendimentoRepository agenda_AtendimentoRepository, IHttpContextAccessor context) : base(agenda_AtendimentoRepository, context)
        {
            Agenda_AtendimentoRepository = agenda_AtendimentoRepository;
        }

        /// <summary>
        /// Adiciona uma mensagem ao log do erro
        /// </summary>
        /// <param name="mensagem"></param>
        public void AddtesteLog(string mensagem)
        {
            Agenda_AtendimentoRepository.AddtesteLog(mensagem);
        }

        /// <summary>
        /// Lista os Agendamentos passando data inicial e data final
        /// </summary>
        /// <param name="paramentros_Pessoa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        public IEnumerable<Agenda_Atendimento> GetList(Parametros_Pessoa paramentros_Pessoa, Parametros_Busca_Grid parametros_Busca_Grid, string datainicial = "", string datafinal = "")
        {
            return Agenda_AtendimentoRepository.GetList(paramentros_Pessoa, parametros_Busca_Grid, datainicial, datafinal);
        }

        /// <summary>
        /// Lista o total de agendamento passando informações de pessoa e data inicial e data final
        /// </summary>
        /// <param name="paramentros_Pessoa"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        public int Count(Parametros_Pessoa paramentros_Pessoa, string datainicial = "", string datafinal = "")
        {
            return Agenda_AtendimentoRepository.Count(paramentros_Pessoa, datainicial, datafinal);
        }

        /// <summary>
        /// Operações de ação do objeto
        /// </summary>
        /// <param name="agenda_Atendimento"></param>
        /// <param name="paramentros_Pessoa"></param>
        /// <param name="parametros_Busca_Navegacao"></param>
        /// <returns></returns>
        public bool Add(Agenda_Atendimento agenda_Atendimento, Parametros_Pessoa paramentros_Pessoa, Parametros_Busca_Navegacao parametros_Busca_Navegacao)
        {
            try
            {
                if (parametros_Busca_Navegacao.acao == "D")
                {
                    // Busca por Id
                    var _excluir = Agenda_AtendimentoRepository.GetById(agenda_Atendimento.id);
                    _excluir.excluido = true;

                    // Atualiza o registro para excluido
                    Agenda_AtendimentoRepository.Update(agenda_Atendimento);
                }
                else
                {
                    switch (parametros_Busca_Navegacao.acao)
                    {
                        // Ação para inclusão de registro
                        case "I":
                            Agenda_AtendimentoRepository.Add(agenda_Atendimento);
                            break;
                        // Ação para edição de registro
                        case "U":
                            Agenda_AtendimentoRepository.Update(agenda_Atendimento);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        /// <summary>
        ///  Valida a existencia de um agendamento
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="data"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        public bool ValidaAgendamento(Parametros_Pessoa paramentros_Pessoa, DateTime data, DateTime hora)
        {
            return Agenda_AtendimentoRepository.ValidaAgendamento(paramentros_Pessoa, data, hora);
        }
    }
}
