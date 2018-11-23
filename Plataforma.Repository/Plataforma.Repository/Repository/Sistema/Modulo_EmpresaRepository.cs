using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Modulo_EmpresaRepository : RepositoryBase<Modulo_Empresa>, IModulo_EmpresaRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Modulo_EmpresaRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro na tabela
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_pessoa_empresa)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Modulo_Empresa in db.Modulo_Empresa
                              where _Modulo_Empresa.excluido == false
                              && _Modulo_Empresa.id_pessoa_empresa == id_pessoa_empresa
                              select _Modulo_Empresa.id);

                return lQuery.ToList().Count();
            }
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
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Modulo_Empresa in db.Modulo_Empresa
                                join _Modulo in db.Modulo on _Modulo_Empresa.id_modulo equals _Modulo.id
                                join _Pessoa_Empresa in db.Pessoa on _Modulo_Empresa.id_pessoa_empresa equals _Pessoa_Empresa.id

                                // Condições
                                where _Modulo_Empresa.excluido == false
                                && _Modulo.excluido == false
                                && _Pessoa_Empresa.excluido == false
                                && _Modulo_Empresa.id_pessoa_empresa == id_pessoa_empresa
                                && ((id_modulo == Guid.Empty) || (_Modulo_Empresa.id_modulo == id_modulo))

                                // Ordenação
                                orderby _Modulo_Empresa.data_validade
                                select new
                                {
                                    id = _Modulo_Empresa.id,

                                    // Pessoa
                                    id_pessoa_empresa = _Modulo_Empresa.id_pessoa_empresa,
                                    nome_fantasia_apelido = _Pessoa_Empresa.nome_fantasia_apelido,
                                    razao_social_nome = _Pessoa_Empresa.razao_social_nome,
                                    cnpj_cpf = _Pessoa_Empresa.cnpj_cpf,

                                    // Modulo
                                    id_modulo = _Modulo_Empresa.id_modulo,
                                    nome_modulo = _Modulo.nome,

                                    data_validade = _Modulo_Empresa.data_validade,
                                    data_inclusao = _Modulo_Empresa.data_inclusao,
                                    usuario = _Modulo_Empresa.usuario

                                }).ToList(); //.Skip(parametrosbuscagrid.page * parametrosbuscagrid.length).Take(parametrosbuscagrid.length).ToList();

                IList<Modulo_Empresa> modulo_empresa = new List<Modulo_Empresa>();

                foreach (var item in lQuery)
                {
                    Modulo_Empresa _modulo_Empresa = new Modulo_Empresa
                    {
                        id = item.id,
                        id_pessoa_empresa = item.id_pessoa_empresa,
                        id_modulo = item.id_modulo,
                        // Pessoa Empresa
                        Pessoa = new Pessoa()
                        {
                            id = item.id,
                            nome_fantasia_apelido = item.nome_fantasia_apelido,
                            razao_social_nome = item.razao_social_nome,
                            cnpj_cpf = item.cnpj_cpf
                        },

                        // Modulos
                        Modulo = new Modulo()
                        {
                            id = item.id_modulo,
                            nome = item.nome_modulo
                        },

                        data_inclusao = item.data_inclusao,
                        usuario = item.usuario
                    };

                    modulo_empresa.Add(_modulo_Empresa);
                }
                return modulo_empresa;
            }
        }        
    }
}
