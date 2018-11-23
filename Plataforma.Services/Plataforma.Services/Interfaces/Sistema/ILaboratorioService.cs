using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de ILaboratorioService
    /// </summary>
    public interface ILaboratorioService : IServiceBase<Laboratorio>
    {
        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista os Laboratorio cadastrados
        /// </summary>
        /// <param name="id_computador"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Laboratorio> GetList(Guid id_laboratorio, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
