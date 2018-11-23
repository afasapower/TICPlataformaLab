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

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de GrupoService
    /// </summary>
    public class GrupoService : ServiceBase<Grupo>, IGrupoService
    {
        /// <summary>
        /// Instancia da Interface IGrupoRepository
        /// </summary>
        public IGrupoRepository GrupoRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="grupoRepository"></param>
        /// <param name="context"></param>
        public GrupoService(IGrupoRepository grupoRepository, IHttpContextAccessor context) : base(grupoRepository, context)
        {
            GrupoRepository = grupoRepository;
        }

        /// <summary>
        /// Conta total de registros por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa)
        {
            return GrupoRepository.Count(id_empresa);
        }

        /// <summary>
        /// Lista Os Grupos disponíveis por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Grupo> GetList(Guid id_empresa, Guid id_grupo, Busca_Generica.Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return GrupoRepository.GetList(id_empresa, id_grupo, parametros_Busca_Grid);
        }
    }
}
