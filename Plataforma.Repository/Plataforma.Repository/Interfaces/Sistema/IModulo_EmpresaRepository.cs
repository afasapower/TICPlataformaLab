using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IModulo_EmpresaRepository
    /// </summary>
    public interface IModulo_EmpresaRepository : IRepositoryBase<Modulo_Empresa>
    {
        /// <summary>
        /// Retorna o total de registro na tabela
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <returns></returns>
        int Count(Guid id_pessoa_empresa);

        /// <summary>
        /// Retorna uma lista de Modulos que a empresa tem acesso
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <param name="id_modulo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Modulo_Empresa> GetList(Guid id_pessoa_empresa, Guid id_modulo, Parametros_Busca_Grid parametros_Busca_Grid);

    }
}
