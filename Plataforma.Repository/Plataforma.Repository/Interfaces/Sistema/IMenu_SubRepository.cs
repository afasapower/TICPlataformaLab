using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IMenu_SubRepository
    /// </summary>
    public interface IMenu_SubRepository : IRepositoryBase<Menu_Sub>
    {
        /// <summary>
        /// Retorna uma lista de Sub-Menu para o Front do sistema
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        IEnumerable<Menu_Sub> GetListFront(Guid id_usuario);

        /// <summary>
        /// Retorna uma lista de Menus para a Administração de Sub-Menus
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="tipo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Menu_Sub> GetList(Guid id_menu, string tipo, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Retorna as abas do menu pagina
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        IEnumerable<Menu_Sub> GetListAba(Guid id_menu, Guid parent);

        /// <summary>
        /// Lista as paginas disponiveis para o menu
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="tipo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Menu_Sub> GetListPaginaDisponivel(Guid id_menu, string tipo, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}