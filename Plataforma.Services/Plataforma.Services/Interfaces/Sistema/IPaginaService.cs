using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IPaginaService
    /// </summary>
    public interface IPaginaService : IServiceBase<Pagina>
    {
        /// <summary>
        /// Retorna o tatal de registro da tabela excluido = false
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista as paginas cadastradas
        /// </summary>
        /// <param name="id_pagina"></param>
        /// <param name="length"></param>
        /// <param name="search"></param>
        /// <param name="draw"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IEnumerable<Pagina> GetList(Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}