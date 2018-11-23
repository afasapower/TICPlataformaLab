using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IPermissao_UsuarioService
    /// </summary>
    public interface IPermissao_UsuarioService : IServiceBase<Permissao_Usuario>
    {
        /// <summary>
        /// Lista o total de usuarios
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        int Count(Guid id_empresa, Guid id_usuario);

        /// <summary>
        /// Lista as Permissões do usuário
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Permissao_Usuario> GetList(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
