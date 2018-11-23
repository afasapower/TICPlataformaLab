using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IPaginaRepository
    /// </summary>
    public interface IPaginaRepository : IRepositoryBase<Pagina>
    {
        /// <summary>
        /// Retorna o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista as paginas
        /// </summary>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Pagina> GetList(Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}