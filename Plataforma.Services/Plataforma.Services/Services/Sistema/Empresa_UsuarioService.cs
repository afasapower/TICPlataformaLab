using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plataforma.Services.Services.Sistema
{
    public class Empresa_UsuarioService : IEmpresa_UsuarioService
    {
        public IPessoaRepository PessoaRepository { get; private set; }
        public IUsuario_EmpresaRepository Usuario_EmpresaRepository { get; private set; }

        public Empresa_UsuarioService(IPessoaRepository pessoaRepository, IUsuario_EmpresaRepository usuario_EmpresaRepository, IHttpContextAccessor context)
        {
            PessoaRepository = pessoaRepository;
            Usuario_EmpresaRepository = usuario_EmpresaRepository;
        }

        /// <summary>
        /// Retorna os usuarios por Empresa
        /// </summary>
        /// <param name="id_usuario_logado"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <param name="_Configuracao_Sistema"></param>
        /// <returns></returns>
        public IList<Dados_Usuario_Empresa> GetList(Guid id_usuario_logado, Guid id_empresa, Busca_Generica.Parametros_Busca_Grid parametros_Busca_Grid, Configuracao_Sistema _Configuracao_Sistema)
        {
            List<Dados_Usuario_Empresa> listaDadosUsuarioEmpresa = new List<Dados_Usuario_Empresa>();
            Guid idOrgsystem = _Configuracao_Sistema.id_empresa_unifacef;
            Guid idUsuarioAtual = id_usuario_logado;
            Guid idEmpresa = id_empresa;
            int totalRegistro = 0;
            if (id_empresa != idOrgsystem)
            {
                totalRegistro = Usuario_EmpresaRepository.Count(idUsuarioAtual, Guid.Empty);
                listaDadosUsuarioEmpresa = Usuario_EmpresaRepository.GetList(idUsuarioAtual, Guid.Empty, parametros_Busca_Grid).ToList().Select(x => new Dados_Usuario_Empresa() { razao_social_nome = x.Pessoa_Empresa.razao_social_nome, id_pessoa_empresa = x.id_pessoa_empresa, totalRegistro = totalRegistro, id_pessoa = x.Pessoa_Empresa.id }).ToList();
            }
            else
            {
                totalRegistro = 3000;
                List<Pessoa> listaPessoas = PessoaRepository.GetListOptions(Guid.Empty, Guid.Empty, false, false, false, true, true, false, true, false, false, false, false, false, true, parametros_Busca_Grid).ToList();
                listaDadosUsuarioEmpresa = (listaPessoas.Count > 0 ? listaPessoas.Select(x => new Dados_Usuario_Empresa() { razao_social_nome = x.razao_social_nome, id_pessoa_empresa = (Guid)x.id, totalRegistro = totalRegistro, id_pessoa = x.id }).ToList() : new List<Dados_Usuario_Empresa>());
            }
            return listaDadosUsuarioEmpresa;
        }
    }
}
