using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IUsuarioService
    /// </summary>
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        /// <summary>
        /// Conta o total de registros por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        int Count(Guid id_empresa);

        /// <summary>
        /// Valida se o usuário existe dentro de uma empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="puser"></param>
        /// <returns></returns>
        Boolean ValidarUsuario(Guid id_empresa, string puser);

        /// <summary>
        /// Autenticação do usuário no banco
        /// </summary>
        /// <param name="puser"></param>
        /// <param name="ppass"></param>
        /// <returns></returns>
        IEnumerable<Usuario> Autenticar(string puser, string ppass);

        /// <summary>
        /// Valida as permissões do Usuário/Grupos nas páginas
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Retorno_Permissao_Grupo_Usuario> ValidaPermissao(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Lista todos os usuário de uma empresa
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Usuario> GetList(Guid id_pessoa_empresa, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="idPagina"></param>
        /// <param name="contexto"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <param name="aba"></param>
        void ValidaPagina(ClaimsPrincipal claimsIdentity, Guid idPagina, HttpResponse contexto, Parametros_Busca_Grid parametros_Busca_Grid, bool aba = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="idPagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        List<Retorno_Permissao_Grupo_Usuario> ValidaMenusUtilitarios(ClaimsPrincipal claimsIdentity, Guid idPagina, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Lista todos os usuário que estão ativos na empresa
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Usuario> GetListGrid(Guid id_pessoa_empresa, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}
