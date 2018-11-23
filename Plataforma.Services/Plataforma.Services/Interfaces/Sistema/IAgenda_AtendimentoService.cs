using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IAgenda_AtendimentoService
    /// </summary>
    public interface IAgenda_AtendimentoService : IServiceBase<Agenda_Atendimento>
    {
        /// <summary>
        /// Lista o total de agendamento passando informações de pessoa e data inicial e data final.
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        int Count(Parametros_Pessoa parametros_Pessoa, string datainicial = "", string datafinal = "");

        /// <summary>
        /// Adiciona uma mensagem ao log do erro.
        /// </summary>
        /// <param name="mensagem"></param>
        void AddtesteLog(string mensagem);
       
        /// <summary>
        /// Lista os Agendamentos passando data inicial e data final
        /// </summary>
        /// <param name="paramentros_Pessoa"></param>
        /// <param name="_Parametros_Busca_Grid"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        IEnumerable<Agenda_Atendimento> GetList(Parametros_Pessoa paramentros_Pessoa, Parametros_Busca_Grid _Parametros_Busca_Grid, string datainicial = "", string datafinal = "");
        
        /// <summary>
        /// Valida a existencia de um agendamento.
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="data"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        bool ValidaAgendamento(Parametros_Pessoa paramentros_Pessoa, DateTime data, DateTime hora);
    }
}
