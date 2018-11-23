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
    /// Service de MenuService
    /// </summary>
    public class MenuService : ServiceBase<Menu>, IMenuService
    {
        public IMenuRepository MenuRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="menuRepository"></param>
        /// <param name="context"></param>
        public MenuService(IMenuRepository menuRepository, IHttpContextAccessor context) : base(menuRepository, context)
        {
            MenuRepository = menuRepository;
        }

        /// <summary>
        /// Lista todos os menus
        /// </summary>
        /// <param name="id_modulo"></param>
        /// <param name="id_menu"></param>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetList(Guid id_modulo, Guid id_menu, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return MenuRepository.GetList(id_modulo, id_menu, id_usuario, parametros_Busca_Grid);
        }
    }
}
