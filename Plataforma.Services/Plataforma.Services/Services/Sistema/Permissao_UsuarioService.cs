using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Permissao_UsuarioService
    /// </summary>
    public class Permissao_UsuarioService : ServiceBase<Permissao_Usuario>, IPermissao_UsuarioService
    {
        public IPermissao_UsuarioRepository Permissao_UsuarioRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="permissao_UsuarioRepository"></param>
        /// <param name="context"></param>
        public Permissao_UsuarioService(IPermissao_UsuarioRepository permissao_UsuarioRepository, IHttpContextAccessor context) : base(permissao_UsuarioRepository, context)
        {
            Permissao_UsuarioRepository = permissao_UsuarioRepository;
        }

        /// <summary>
        /// Lista o total de usuarios
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa, Guid id_usuario)
        {
            return Permissao_UsuarioRepository.Count(id_empresa, id_usuario);
        }

        /// <summary>
        ///  Lista as Permissões do usuário
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Permissao_Usuario> GetList(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Permissao_UsuarioRepository.GetList(id_empresa, id_usuario, id_pagina, parametros_Busca_Grid);
        }
    }
}