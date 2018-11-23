using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de SoftwareService
    /// </summary>
    public class SoftwareService : ServiceBase<Software>, ISoftwareService
    {
        public ISoftwareRepository SoftwareRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="softwareRepository"></param>
        /// <param name="context"></param>
        public SoftwareService(ISoftwareRepository softwareRepository, IHttpContextAccessor context) : base(softwareRepository, context)
        {
            SoftwareRepository = softwareRepository;
        }

        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return SoftwareRepository.Count();
        }

        /// <summary>
        /// Lista os Software cadastrados
        /// </summary>
        /// <param name="id_software"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Software> GetList(Guid id_software, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return SoftwareRepository.GetList(id_software, parametros_Busca_Grid);
        }
    }
}
