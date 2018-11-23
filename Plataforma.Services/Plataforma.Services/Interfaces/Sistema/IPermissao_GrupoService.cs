using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IPermissao_GrupoService
    /// </summary>
    public interface IPermissao_GrupoService : IServiceBase<Permissao_Grupo>
    {
        /*
            LEMBRAR DE REFAZER QUEM FOR MEXER 
        */

        /// <summary>
        /// Retorna o total de permissões do grupo
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <returns></returns>
        int Count(Guid id_empresa, Guid id_grupo);

        /// <summary>
        /// Retorna as paginas com permissão para o grupo
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametrosbuscagrid"></param>
        /// <returns></returns>
        IEnumerable<Permissao_Grupo> GetList(Guid id_empresa, Guid id_grupo, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Lista as permissões de um determinando Grupo de usuários
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="id_menu"></param>
        /// <param name="parametrosbuscagrid"></param>
        /// <returns></returns>
        IEnumerable<Retorno_Permissao_Grupo_Pagina> GetListPermissaoPagina(Guid id_empresa, Guid id_grupo, Guid id_menu, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Lista as permissões de módulos e menus de um determinado grupo
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametrosbuscagrid"></param>
        /// <returns></returns>
        IEnumerable<Retorno_Permissao_Grupo_Menu> GetListPermissaoMenu(Guid id_empresa, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
