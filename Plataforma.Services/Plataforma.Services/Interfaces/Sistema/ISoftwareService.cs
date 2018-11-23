using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de ISoftwareService
    /// </summary>
    public interface ISoftwareService : IServiceBase<Software>
    {
        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista os Software cadastrados
        /// </summary>
        /// <param name="id_software"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Software> GetList(Guid id_software, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
