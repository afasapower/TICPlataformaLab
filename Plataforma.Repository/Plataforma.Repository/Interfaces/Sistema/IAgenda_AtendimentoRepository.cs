using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IAgenda_AtendimentoRepository
    /// </summary>
    public interface IAgenda_AtendimentoRepository : IRepositoryBase<Agenda_Atendimento>
    {
        /// <summary>
        /// Retorna o total de agendamento para uma empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        int Count(Parametros_Pessoa paramentros_Pessoa, string datainicial = "", string datafinal = "");

        void AddtesteLog(string mensagem);

        /// <summary>
        /// Lista os agendamentos de uma determinada empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_pessoa"></param>
        /// <param name="_Parametros_Busca_Grid"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        IEnumerable<Agenda_Atendimento> GetList(Parametros_Pessoa paramentros_Pessoa, Parametros_Busca_Grid parametros_Busca_Grid, string datainicial = "", string datafinal = "");

        /// <summary>
        /// Valida se existe ou não agendamento em uma determinada data e hora
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="data"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        bool ValidaAgendamento(Parametros_Pessoa paramentros_Pessoa, DateTime data, DateTime hora);

    }
}