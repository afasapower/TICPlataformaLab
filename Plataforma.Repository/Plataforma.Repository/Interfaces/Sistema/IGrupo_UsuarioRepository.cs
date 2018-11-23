using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IGrupo_UsuarioRepository
    /// </summary>
    public interface IGrupo_UsuarioRepository : IRepositoryBase<Grupo_Usuario>
    {
        /// <summary>
        /// Retorna o total de registros ativos da tabela Grupo_Usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        int Count(Parametros_Pessoa paramentros_Pessoa);

        /// <summary>
        /// Lista os registros ativos da tabela Grupo_Usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Grupo_Usuario> GetList(Guid id_usuario, Guid id_grupo, Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Realiza a exclusão dos grupos do usuário da tabela Grupo_Usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        void RemoveAll(Guid id_usuario);
    }
}
