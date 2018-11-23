using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IComputadorService
    /// </summary>
    public interface IComputadorService : IServiceBase<Computador>
    {
        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista os computadores cadastrados
        /// </summary>
        /// <param name="id_computador"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Computador> GetList(Guid id_computador, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
