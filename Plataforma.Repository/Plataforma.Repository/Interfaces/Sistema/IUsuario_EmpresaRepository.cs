using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IUsuario_EmpresaRepository
    /// </summary>
    public interface IUsuario_EmpresaRepository : IRepositoryBase<Usuario_Empresa>
    {
        /// <summary>
        /// Retorna o total de registros da tabela
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        int Count(Guid id_usuario, Guid id_empresa);

        /// <summary>
        /// Lista os dados com as empresas que o usuário pode acessar
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Usuario_Empresa> GetList(Guid id_usuario, Guid id_empresa, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Remove todas as permissões das empresa antiga
        /// </summary>
        /// <param name="id_usuario"></param>
        void RemoveAll(Guid id_usuario);
        
    }
}