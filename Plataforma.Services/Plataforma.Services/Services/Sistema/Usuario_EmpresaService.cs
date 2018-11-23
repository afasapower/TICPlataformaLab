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
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Usuario_EmpresaService
    /// </summary>
    public class Usuario_EmpresaService : ServiceBase<Usuario_Empresa>, IUsuario_EmpresaService
    {
        public IUsuario_EmpresaRepository Usuario_EmpresaRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="usuario_EmpresaRepository"></param>
        /// <param name="context"></param>
        public Usuario_EmpresaService(IUsuario_EmpresaRepository usuario_EmpresaRepository, IHttpContextAccessor context) : base(usuario_EmpresaRepository, context)
        {
            Usuario_EmpresaRepository = usuario_EmpresaRepository;
        }

        /// <summary>
        /// Retorna o total de registros da tabela
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_usuario, Guid id_empresa)
        {
            return Usuario_EmpresaRepository.Count(id_usuario, id_empresa);
        }

        /// <summary>
        /// Lista os dados com as empresas que o usuário pode acessar
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Usuario_Empresa> GetList(Guid id_usuario, Guid id_empresa, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return Usuario_EmpresaRepository.GetList(id_usuario, id_empresa, parametros_Busca_Grid);
        }

        /// <summary>
        /// Remove todas as permissões das empresa antiga
        /// </summary>
        /// <param name="id_usuario"></param>
        public void RemoveAll(Guid id_usuario)
        {
            Usuario_EmpresaRepository.RemoveAll(id_usuario);
        }

        /// <summary>
        /// Lista os dados do usuario
        /// </summary>
        /// <param name="pessoaRepository"></param>
        /// <param name="usuario_EmpresaRepository"></param>
        /// <param name="id_usuario_logado"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametrosGrid"></param>
        /// <param name="_Configuracao_Sistema"></param>
        /// <returns></returns>
        public List<Dados_Usuario_Empresa> GetListDadosUsuario(IPessoaService pessoaRepository, IUsuario_EmpresaService usuario_EmpresaRepository, Guid id_usuario_logado, Guid id_empresa, Parametros_Busca_Grid parametrosGrid, Configuracao_Sistema _Configuracao_Sistema)
        {
            List<Dados_Usuario_Empresa> listaDados_Usuario_Empresa = new List<Dados_Usuario_Empresa>();
            Guid idOrgsystem = _Configuracao_Sistema.id_empresa_unifacef;
            Guid idUsuarioAtual = id_usuario_logado;
            Guid idEmpresa = id_empresa;
            int totalRegistro = 0;
            if (id_empresa != idOrgsystem)
            {
                totalRegistro = usuario_EmpresaRepository.Count(idUsuarioAtual, Guid.Empty);
                listaDados_Usuario_Empresa = usuario_EmpresaRepository.GetList(idUsuarioAtual, Guid.Empty, parametrosGrid).ToList().Select(x => new Dados_Usuario_Empresa() { razao_social_nome = x.Pessoa_Empresa.razao_social_nome, id_pessoa_empresa = x.id_pessoa_empresa, totalRegistro = totalRegistro, id_pessoa = x.Pessoa_Empresa.id }).ToList();
            }
            else
            {
                totalRegistro = 3000;
                List<Pessoa> listaPessoas = pessoaRepository.GetListOptions(Guid.Empty, Guid.Empty, false, false, false, true, true, false, true, false, false, false, false, false, true, parametrosGrid).ToList();
                listaDados_Usuario_Empresa = (listaPessoas.Count > 0 ? listaPessoas.Select(x => new Dados_Usuario_Empresa() { razao_social_nome = x.razao_social_nome, id_pessoa_empresa = (Guid)x.id, totalRegistro = totalRegistro, id_pessoa = x.id }).ToList() : new List<Dados_Usuario_Empresa>());
            }
            return listaDados_Usuario_Empresa;
        }
    }
}
