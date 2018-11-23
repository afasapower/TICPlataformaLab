using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IGrupoRepository
    /// </summary>
    public interface IGrupoRepository : IRepositoryBase<Grupo>
    {
        /// <summary>
        /// Retorna o total de grupo por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        int Count(Guid id_empresa);

        /// <summary>
        /// Lista os grupos de uma determinada empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="_Parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Grupo> GetList(Guid id_empresa, Guid id_grupo, Busca_Generica.Parametros_Busca_Grid _Parametros_Busca_Grid);
    }
}
