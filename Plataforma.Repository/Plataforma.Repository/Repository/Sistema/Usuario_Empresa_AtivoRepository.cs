using Microsoft.EntityFrameworkCore;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;
using System.Data.SqlClient;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Usuario_Empresa_AtivoRepository : RepositoryBase<Usuario_Empresa_Ativo>, IUsuario_Empresa_AtivoRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Usuario_Empresa_AtivoRepository(PlataformaContext context) : base(context)
        {
        }       

        /// <summary>
        /// Retorna Empresa ativa na navegação do usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Usuario_Empresa_Ativo> GetList_Empresa_Ativa(Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario_Empresa_Ativo in db.Usuario_Empresa_Ativo
                              join _Pessoa_Empresa in db.Pessoa on _Usuario_Empresa_Ativo.id_pessoa_empresa equals _Pessoa_Empresa.id
                              where _Usuario_Empresa_Ativo.id_usuario == id_usuario
                              select new
                              {
                                  id = _Usuario_Empresa_Ativo.id,
                                  id_usuario = _Usuario_Empresa_Ativo.id_usuario,
                                  id_pessoa_empresa = _Usuario_Empresa_Ativo.id_pessoa_empresa,
                                  nome_fantasia_apelido = _Pessoa_Empresa.nome_fantasia_apelido,
                                  razao_social_nome = _Pessoa_Empresa.razao_social_nome,
                                  cnpj_cpf = _Pessoa_Empresa.cnpj_cpf,
                                  data_inclusao = _Usuario_Empresa_Ativo.data_inclusao
                              }).ToList();

                List<Usuario_Empresa_Ativo> usuario_Empresa_Ativo = new List<Usuario_Empresa_Ativo>();

                foreach (var item in lQuery)
                {
                    Usuario_Empresa_Ativo _usuario_Empresa_Ativo = new Usuario_Empresa_Ativo();

                    _usuario_Empresa_Ativo.id = item.id;
                    _usuario_Empresa_Ativo.id_usuario = item.id_usuario;
                    _usuario_Empresa_Ativo.id_pessoa_empresa = item.id_pessoa_empresa;

                    // Dados de Pessoa
                    _usuario_Empresa_Ativo.Pessoa_Empresa = new Pessoa()
                    {
                        id = (Guid)item.id_pessoa_empresa,
                        razao_social_nome = item.razao_social_nome,
                        nome_fantasia_apelido = item.nome_fantasia_apelido,
                        cnpj_cpf = item.cnpj_cpf
                    };

                    _usuario_Empresa_Ativo.data_inclusao = item.data_inclusao;

                    usuario_Empresa_Ativo.Add(_usuario_Empresa_Ativo);
                }

                return usuario_Empresa_Ativo;
            }
        }
    }
}
