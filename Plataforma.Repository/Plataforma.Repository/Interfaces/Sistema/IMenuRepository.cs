using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IMenuRepository
    /// </summary>
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        /// <summary>
        /// Retorna os menus ativos do módulo
        /// </summary>
        /// <param name="id_modulo"></param>
        /// <param name="id_menu"></param>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Menu> GetList(Guid id_modulo, Guid id_menu, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}