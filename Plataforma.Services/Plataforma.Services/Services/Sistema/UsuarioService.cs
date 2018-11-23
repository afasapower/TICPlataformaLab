using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de UsuarioService
    /// </summary>
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        public IUsuarioRepository UsuarioRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="usuarioRepository"></param>
        /// <param name="context"></param>
        public UsuarioService(IUsuarioRepository usuarioRepository, IHttpContextAccessor context) : base(usuarioRepository, context)
        {
            UsuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Conta o total de registros por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa)
        {
            return UsuarioRepository.Count(id_empresa);
        }

        /// <summary>
        /// Valida se o usuário existe dentro de uma empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="puser"></param>
        /// <returns></returns>
        public bool ValidarUsuario(Guid id_empresa, string puser)
        {
            return UsuarioRepository.ValidarUsuario(id_empresa, puser);
        }

        /// <summary>
        /// Autenticação do usuário no banco
        /// </summary>
        /// <param name="puser"></param>
        /// <param name="ppass"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> Autenticar(string puser, string ppass)
        {
            return UsuarioRepository.Autenticar(puser, ppass);
        }

        /// <summary>
        /// Valida as permissões do Usuário/Grupos nas páginas
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Retorno_Permissao_Grupo_Usuario> ValidaPermissao(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return UsuarioRepository.ValidaPermissao(id_empresa, id_usuario, id_pagina, parametros_Busca_Grid);
        }

        /// <summary>
        /// Lista todos os usuário de uma empresa
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> GetList(Guid id_pessoa_empresa, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return UsuarioRepository.GetList(id_pessoa_empresa, id_usuario, parametros_Busca_Grid);
        }

        public void ValidaPagina(ClaimsPrincipal claimsIdentity, Guid idPagina, HttpResponse contexto, Parametros_Busca_Grid parametros_Busca_Grid, bool aba = false)
        {
            string urlAcesso = (!aba ? "AcessoNegado" : "AcessoNegadoAba");
            if (idPagina != Guid.Empty)
            {
                Guid idEmpresa = new Guid(claimsIdentity.Claims.FirstOrDefault(x => x.Type == "id_pessoa_empresa").Value);
                Guid idUsuario = new Guid(claimsIdentity.Claims.FirstOrDefault(x => x.Type == "id").Value);
                List<Retorno_Permissao_Grupo_Usuario> PermissaoGrupoUsuario = new List<Retorno_Permissao_Grupo_Usuario>();

                PermissaoGrupoUsuario = UsuarioRepository.ValidaPermissao(idEmpresa, idUsuario, idPagina, parametros_Busca_Grid).ToList();

                if (PermissaoGrupoUsuario.Count == 0) contexto.Redirect($"{contexto.HttpContext.Request.PathBase}/home/{urlAcesso}");
            }
            else
                contexto.Redirect($"{contexto.HttpContext.Request.PathBase}/home/{urlAcesso}");
        }

        public List<Retorno_Permissao_Grupo_Usuario> ValidaMenusUtilitarios(ClaimsPrincipal claimsIdentity, Guid idPagina,Parametros_Busca_Grid parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Usuario> PermissaoGrupoUsuario = new List<Retorno_Permissao_Grupo_Usuario>();
            try
            {
                if (idPagina != Guid.Empty)
                {
                    Guid idEmpresa = new Guid(claimsIdentity.Claims.FirstOrDefault(x => x.Type == "id_pessoa_empresa").Value);
                    Guid idUsuario = new Guid(claimsIdentity.Claims.FirstOrDefault(x => x.Type == "id").Value);

                    PermissaoGrupoUsuario = UsuarioRepository.ValidaPermissao(idEmpresa, idUsuario, idPagina, parametros_Busca_Grid).ToList();
                }
            }
            catch (Exception)
            {

            }
            return PermissaoGrupoUsuario;
        }

        public IEnumerable<Usuario> GetListGrid(Guid id_pessoa_empresa, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return UsuarioRepository.GetListGrid(id_pessoa_empresa, id_usuario, parametros_Busca_Grid);
        }
    }
}
