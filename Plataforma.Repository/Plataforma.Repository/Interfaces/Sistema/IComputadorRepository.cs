using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IComputadorRepository
    /// </summary>
    public interface IComputadorRepository : IRepositoryBase<Computador>
    {
        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <param name="id_computador"></param>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista os Computadores cadastrados
        /// </summary>
        /// <param name="id_computador"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Computador> GetList(Guid id_computador, Parametros_Busca_Grid parametros_Busca_Grid);

    }
}
