using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Permissao_Grupo_EtapaRepository : RepositoryBase<Permissao_Grupo_Etapa>, IPermissao_Grupo_EtapaRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Permissao_Grupo_EtapaRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos
        /// </summary>
        /// <param name="id_grupo"></param>
        /// <returns></returns>
        public int Count(Guid id_grupo)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Permissao_Grupo_Etapa in db.Permissao_Grupo_Etapa
                              where _Permissao_Grupo_Etapa.id_grupo == id_grupo
                              select _Permissao_Grupo_Etapa.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todos as permissoes de grupo por etapa que não estão excluidas
        /// </summary>
        /// <param name="id_grupo"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <param name="codigo_etapa"></param>
        /// <returns></returns>
        public IEnumerable<Permissao_Grupo_Etapa> GetList(Guid id_grupo, int codigo_etapa = -1, Parametros_Busca_Grid parametros_Busca_Grid = null)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Permissao_Grupo_Etapa in db.Permissao_Grupo_Etapa
                              // Grupos
                              join _Grupo in db.Grupo on _Permissao_Grupo_Etapa.id_grupo equals _Grupo.id
                              // Condiçoes
                              where _Permissao_Grupo_Etapa.id_grupo == id_grupo
                              && _Permissao_Grupo_Etapa.excluido == false
                              select new
                              {
                                  id = _Permissao_Grupo_Etapa.id,
                                  // Grupo
                                  id_grupo = _Permissao_Grupo_Etapa.id_grupo,
                                  nome_grupo = _Grupo.nome,
                                  // Etapa_Processo

                                  upload = _Permissao_Grupo_Etapa.upload,
                                  download = _Permissao_Grupo_Etapa.download,
                                  outros = _Permissao_Grupo_Etapa.outros,
                                  data_inclusao = _Permissao_Grupo_Etapa.data_inclusao
                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Permissao_Grupo_Etapa> _permissao_grupo_etapa = new List<Permissao_Grupo_Etapa>();

                foreach (var item in lQuery)
                {
                    Permissao_Grupo_Etapa permissao_grupo_etapa = new Permissao_Grupo_Etapa();

                    permissao_grupo_etapa.id = item.id;
                    permissao_grupo_etapa.id_grupo = item.id_grupo;
                    permissao_grupo_etapa.upload = item.upload;
                    permissao_grupo_etapa.download = item.download;
                    permissao_grupo_etapa.outros = item.outros;
                    permissao_grupo_etapa.data_inclusao = item.data_inclusao;

                    // Informações do grupo
                    permissao_grupo_etapa.Grupo = new Grupo()
                    {
                        id = item.id_grupo,
                        nome = item.nome_grupo
                    };

                    _permissao_grupo_etapa.Add(permissao_grupo_etapa);
                }

                return _permissao_grupo_etapa;
            }
        }
    }
}