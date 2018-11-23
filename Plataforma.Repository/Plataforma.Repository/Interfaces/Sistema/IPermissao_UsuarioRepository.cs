using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IPermissao_UsuarioRepository
    /// </summary>
    public interface IPermissao_UsuarioRepository : IRepositoryBase<Permissao_Usuario>
    {
        /// <summary>
        /// Lista as permissoes dos usuarios por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        int Count(Guid id_empresa, Guid id_usuario);

        /// <summary>
        /// Lista as permissões de usuarios
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Permissao_Usuario> GetList(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
