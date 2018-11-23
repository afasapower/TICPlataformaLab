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
    /// Service de Permissao_Grupo_EtapaService
    /// </summary>
    public class Permissao_Grupo_EtapaService : ServiceBase<Permissao_Grupo_Etapa>, IPermissao_Grupo_EtapaService
    {
        public IPermissao_Grupo_EtapaRepository Permissao_Grupo_EtapaRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="permissao_Grupo_EtapaRepository"></param>
        /// <param name="context"></param>
        public Permissao_Grupo_EtapaService(IPermissao_Grupo_EtapaRepository permissao_Grupo_EtapaRepository, IHttpContextAccessor context) : base(permissao_Grupo_EtapaRepository, context)
        {
            Permissao_Grupo_EtapaRepository = permissao_Grupo_EtapaRepository;
        }

        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <param name="id_grupo"></param>
        /// <returns></returns>
        public int Count(Guid id_grupo)
        {
            return Permissao_Grupo_EtapaRepository.Count(id_grupo);
        }

        /// <summary>
        /// Lista as permissões do grupo nas etapas do processo
        /// </summary>
        /// <param name="id_grupo"></param>
        /// <param name="codigo_etapa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Permissao_Grupo_Etapa> GetList(Guid id_grupo, int codigo_etapa = -1, Parametros_Busca_Grid parametros_Busca_Grid = null)
        {
            return Permissao_Grupo_EtapaRepository.GetList(id_grupo, codigo_etapa, parametros_Busca_Grid);
        }
    }
}