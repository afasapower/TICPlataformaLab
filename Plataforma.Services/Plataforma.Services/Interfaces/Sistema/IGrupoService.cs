using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IGrupoService
    /// </summary>
    public interface IGrupoService : IServiceBase<Grupo>
    {
        /// <summary>
        /// Conta total de registros por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        int Count(Guid id_empresa);

        /// <summary>
        /// Lista Os Grupos disponíveis por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Grupo> GetList(Guid id_empresa, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}