using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using Plataforma.Domain.Entities.NotMapped;

namespace Plataforma.Repository.Repository.Sistema
{
    public class GrupoRepository : RepositoryBase<Grupo>, IGrupoRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public GrupoRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Grupo in db.Grupo
                              where _Grupo.id_pessoa_empresa == id_empresa
                              && _Grupo.excluido == false
                              select _Grupo.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todos os grupos que não estão excluidos de acordo com os parametros
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_grupo"></param>
        /// <param name="_Parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Grupo> GetList(Guid id_empresa, Guid id_grupo, Busca_Generica.Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Grupo in db.Grupo
                              join _Situacao_Cadastral in db.Situacao_Cadastral on _Grupo.id_situacao_cadastral equals _Situacao_Cadastral.id
                              where _Grupo.id_pessoa_empresa == id_empresa
                              && ((id_grupo == Guid.Empty) || (_Grupo.id == id_grupo))
                              && _Grupo.excluido == false
                              orderby _Grupo.id, _Grupo.nome
                              select new
                              {
                                  id = _Grupo.id,
                                  id_pessoa_empresa = _Grupo.id_pessoa_empresa,
                                  nome = _Grupo.nome,
                                  id_situacao_cadastral = _Grupo.id_situacao_cadastral,
                                  nome_situacao = _Situacao_Cadastral.descricao
                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Grupo> grupo = new List<Grupo>();

                foreach (var item in lQuery)
                {
                    Grupo _Grupo = new Grupo();

                    _Grupo.id = item.id;
                    _Grupo.id_pessoa_empresa = item.id_pessoa_empresa;
                    _Grupo.nome = item.nome;
                    _Grupo.id_situacao_cadastral = item.id_situacao_cadastral;
                    _Grupo.Situacao_Cadastral = new Situacao_Cadastral()
                    {
                        id = item.id_situacao_cadastral,
                        descricao = item.nome_situacao
                    };

                    grupo.Add(_Grupo);
                }

                return grupo.OrderBy(m => m.nome);
            }
        }
    }
}
