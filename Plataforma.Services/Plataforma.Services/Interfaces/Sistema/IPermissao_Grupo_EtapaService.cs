using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IPermissao_Grupo_EtapaService
    /// </summary>
    public interface IPermissao_Grupo_EtapaService : IServiceBase<Permissao_Grupo_Etapa>
    {
        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <param name="id_grupo"></param>
        /// <returns></returns>
        int Count(Guid id_grupo);

        /// <summary>
        /// Lista as permissões do grupo nas etapas do processo
        /// </summary>
        /// <param name="id_grupo"></param>
        /// <param name="codigo_etapa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Permissao_Grupo_Etapa> GetList(Guid id_grupo, int codigo_etapa = -1, Parametros_Busca_Grid parametros_Busca_Grid = null);
    }
}