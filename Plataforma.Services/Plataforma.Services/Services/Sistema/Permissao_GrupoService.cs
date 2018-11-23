using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Permissao_GrupoService
    /// </summary>
    public class Permissao_GrupoService : ServiceBase<Permissao_Grupo>, IPermissao_GrupoService
    {
        public IPermissao_GrupoRepository Permissao_GrupoRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="permissao_GrupoRepository"></param>
        /// <param name="context"></param>
        public Permissao_GrupoService(IPermissao_GrupoRepository permissao_GrupoRepository, IHttpContextAccessor context) : base(permissao_GrupoRepository, context)
        {
            Permissao_GrupoRepository = permissao_GrupoRepository;
        }

        /// <summary>
        /// Retorna o total de permissões do grupo
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa, Guid id_grupo)
        {
            return Permissao_GrupoRepository.Count(id_empresa, id_grupo);
        }

        /// <summary>
        /// Retorna as paginas com permissão para o grupo
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Permissao_Grupo> GetList(Guid id_empresa, Guid id_grupo, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Permissao_GrupoRepository.GetList(id_empresa, id_grupo, id_pagina, parametros_Busca_Grid);
        }

        /// <summary>
        /// Lista as permissões de um determinando Grupo de usuários
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="id_menu"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Retorno_Permissao_Grupo_Pagina> GetListPermissaoPagina(Guid id_empresa, Guid id_grupo, Guid id_menu, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Permissao_GrupoRepository.GetListPermissaoPagina(id_empresa, id_grupo, id_menu, parametros_Busca_Grid);
        }

        /// <summary>
        /// Lista as permissões de módulos e menus de um determinado grupo
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Retorno_Permissao_Grupo_Menu> GetListPermissaoMenu(Guid id_empresa, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Permissao_GrupoRepository.GetListPermissaoMenu(id_empresa, id_grupo, parametros_Busca_Grid);
        }
    }
}