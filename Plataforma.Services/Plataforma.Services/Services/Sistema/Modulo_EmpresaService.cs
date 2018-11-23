using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Modulo_EmpresaService
    /// </summary>
    public class Modulo_EmpresaService : ServiceBase<Modulo_Empresa>, IModulo_EmpresaService
    {
        public IModulo_EmpresaRepository Modulo_EmpresaRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="modulo_EmpresaRepository"></param>
        /// <param name="context"></param>
        public Modulo_EmpresaService(IModulo_EmpresaRepository modulo_EmpresaRepository, IHttpContextAccessor context) : base(modulo_EmpresaRepository, context)
        {
            Modulo_EmpresaRepository = modulo_EmpresaRepository;
        }

        /// <summary>
        /// Retorna o total de registro na tabela
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_pessoa_empresa)
        {
            return Modulo_EmpresaRepository.Count(id_pessoa_empresa);
        }

        /// <summary>
        /// Retorna uma lista de Modulos que a empresa tem acesso
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <param name="id_modulo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Modulo_Empresa> GetList(Guid id_pessoa_empresa, Guid id_modulo, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Modulo_EmpresaRepository.GetList(id_pessoa_empresa, id_modulo, parametros_Busca_Grid);
        }
    }
}
