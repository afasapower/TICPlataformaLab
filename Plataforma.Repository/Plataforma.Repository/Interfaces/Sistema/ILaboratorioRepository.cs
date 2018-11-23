using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de ILaboratorioRepository
    /// </summary>
    public interface ILaboratorioRepository : IRepositoryBase<Laboratorio>
    {
        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista os Laboratorios cadastrados
        /// </summary>
        /// <param name="id_laboratorio"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Laboratorio> GetList(Guid id_laboratorio, Parametros_Busca_Grid parametros_Busca_Grid);

    }
}
