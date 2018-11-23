using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class LaboratorioRepository : RepositoryBase<Laboratorio>, ILaboratorioRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public LaboratorioRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos da tabela Laboratorio
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Laboratorio in db.Laboratorio
                              where _Laboratorio.fg_ativo == false
                              select _Laboratorio.id_laboratorio);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todas os Laboratorios que não estão excluidas
        /// </summary>
        /// <param name="id_laboratorio"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Laboratorio> GetList(Guid id_laboratorio, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Laboratorio in db.Laboratorio
                              where ((id_laboratorio == Guid.Empty) || (_Laboratorio.id_laboratorio == id_laboratorio))
                              && _Laboratorio.fg_ativo == false
                              orderby _Laboratorio.nome
                              select new
                              {
                                  id_laboratorio = _Laboratorio.id_laboratorio,
                                  nome = _Laboratorio.nome,
                                  unidade = _Laboratorio.unidade

                              }).ToList();

                IList<Laboratorio> laboratorio = new List<Laboratorio>();

                foreach (var item in lQuery)
                {
                    Laboratorio _Laboratorio = new Laboratorio();

                    _Laboratorio.id_laboratorio = item.id_laboratorio;
                    _Laboratorio.nome = item.nome;
                    _Laboratorio.unidade = item.unidade;

                    laboratorio.Add(_Laboratorio);
                }

                return laboratorio.OrderBy(m => m.nome);
            }
        }
    }
}