using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IUsuario_EmpresaService
    /// </summary>
    public interface IUsuario_EmpresaService : IServiceBase<Usuario_Empresa>
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

        /// <summary>
        /// Lista os dados dos usuarios
        /// </summary>
        /// <param name="pessoaRepository"></param>
        /// <param name="usuario_EmpresaRepository"></param>
        /// <param name="id_usuario_logado"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametrosGrid"></param>
        /// <param name="_Configuracao_Sistema"></param>
        /// <returns></returns>
        List<Dados_Usuario_Empresa> GetListDadosUsuario(IPessoaService pessoaRepository, IUsuario_EmpresaService usuario_EmpresaRepository, Guid id_usuario_logado, Guid id_empresa, Parametros_Busca_Grid parametrosGrid, Configuracao_Sistema _Configuracao_Sistema);
    }
}
