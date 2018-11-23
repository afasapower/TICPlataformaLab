using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class SoftwareRepository : RepositoryBase<Software>, ISoftwareRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public SoftwareRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos da tabela Software
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Software in db.Software
                              where _Software.fg_ativo == false
                              select _Software.id_software);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todas os Softwares que não estão excluidas
        /// </summary>
        /// <param name="id_laboratorio"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Software> GetList(Guid id_software, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Software in db.Software
                              where ((id_software == Guid.Empty) || (_Software.id_software == id_software))
                              && _Software.fg_ativo == false
                              orderby _Software.nome_software
                              select new
                              {
                                  id_software = _Software.id_software,
                                  fabricante = _Software.fabricante,
                                  open_source = _Software.open_source,
                                  versao_software = _Software.versao_software,
                                  data_aquisicao = _Software.data_aquisicao,
                                  data_vencimento = _Software.data_vencimento,
                                  nome_software = _Software.nome_software
                              }).ToList();

                IList<Software> software = new List<Software>();

                foreach (var item in lQuery)
                {
                    Software _Software = new Software();

                    _Software.id_software = item.id_software;
                    _Software.fabricante = item.fabricante;
                    _Software.open_source = item.open_source;
                    _Software.versao_software = item.versao_software;
                    _Software.data_aquisicao = item.data_aquisicao;
                    _Software.data_vencimento = item.data_vencimento;
                    _Software.nome_software = item.nome_software;

                    software.Add(_Software);
                }

                return software.OrderBy(m => m.nome_software);
            }
        }
    }
}