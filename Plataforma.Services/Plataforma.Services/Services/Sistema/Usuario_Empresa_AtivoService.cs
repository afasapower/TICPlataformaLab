using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Usuario_Empresa_AtivoService
    /// </summary>
    public class Usuario_Empresa_AtivoService : ServiceBase<Usuario_Empresa_Ativo>, IUsuario_Empresa_AtivoService
    {
        public IUsuario_Empresa_AtivoRepository Usuario_Empresa_AtivoRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="usuario_Empresa_AtivoRepository"></param>
        /// <param name="context"></param>
        public Usuario_Empresa_AtivoService(IUsuario_Empresa_AtivoRepository usuario_Empresa_AtivoRepository, IHttpContextAccessor context) : base(usuario_Empresa_AtivoRepository, context)
        {
            Usuario_Empresa_AtivoRepository = usuario_Empresa_AtivoRepository;
        }

        /// <summary>
        /// Retorna Empresa ativa na navegação do usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Usuario_Empresa_Ativo> GetList_Empresa_Ativa(Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Usuario_Empresa_AtivoRepository.GetList_Empresa_Ativa(id_usuario, parametros_Busca_Grid);
        }
    }
}
