using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Menu_SubService
    /// </summary>
    public class Menu_SubService : ServiceBase<Menu_Sub>, IMenu_SubService
    {
        public IMenu_SubRepository Menu_SubRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="menu_SubRepository"></param>
        /// <param name="context"></param>
        public Menu_SubService(IMenu_SubRepository menu_SubRepository, IHttpContextAccessor context) : base(menu_SubRepository, context)
        {
            Menu_SubRepository = menu_SubRepository;
        }

        /// <summary>
        /// Retorna uma lista de Sub-Menu para o Front do sistema
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetListFront(Guid id_usuario)
        {
            return Menu_SubRepository.GetListFront(id_usuario);
        }

        /// <summary>
        /// Retorna uma lista de Menus para a Administração de Sub-Menus
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="tipo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetList(Guid id_menu, string tipo = "", Parametros_Busca_Grid parametros_Busca_Grid = null)
        {
            return Menu_SubRepository.GetList(id_menu, tipo, parametros_Busca_Grid);
        }

        /// <summary>
        /// Retorna as abas do menu pagina
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetListAba(Guid id_menu, Guid parent)
        {
            return Menu_SubRepository.GetListAba(id_menu, parent);
        }

        /// <summary>
        /// Lista as paginas disponiveis para o menu
        /// </summary>
        /// <param name="id_menu"></param>
        /// <param name="tipo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Menu_Sub> GetListPaginaDisponivel(Guid id_menu, string tipo = "", Parametros_Busca_Grid parametros_Busca_Grid = null)
        {
            return Menu_SubRepository.GetListPaginaDisponivel(id_menu, tipo, parametros_Busca_Grid);
        }
    }
}
