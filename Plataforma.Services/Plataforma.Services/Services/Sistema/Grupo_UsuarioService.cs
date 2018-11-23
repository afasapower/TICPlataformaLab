using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Grupo_UsuarioService
    /// </summary>
    public class Grupo_UsuarioService : ServiceBase<Grupo_Usuario>, IGrupo_UsuarioService
    {
        public IGrupo_UsuarioRepository Grupo_UsuarioRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="grupo_UsuarioRepository"></param>
        /// <param name="context"></param>
        public Grupo_UsuarioService(IGrupo_UsuarioRepository grupo_UsuarioRepository, IHttpContextAccessor context) : base(grupo_UsuarioRepository, context)
        {
            Grupo_UsuarioRepository = grupo_UsuarioRepository;
        }

        /// <summary>
        /// Retorna o total de registros ativos da tabela Grupo_Usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public int Count(Parametros_Pessoa parametros_Pessoa)
        {
            return Grupo_UsuarioRepository.Count(parametros_Pessoa);
        }

        /// <summary>
        /// Lista os registros ativos da tabela Grupo_Usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Grupo_Usuario> GetList(Guid id_usuario, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Grupo_UsuarioRepository.GetList(id_usuario, id_grupo, parametros_Busca_Grid);
        }

        /// <summary>
        /// Realiza exclusão dos grupos do usuário em massa da tabela Grupo_Usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        public void RemoveAll(Guid id_usuario)
        {
            Grupo_UsuarioRepository.RemoveAll(id_usuario);
        }
    }
}
