using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class ComputadorRepository : RepositoryBase<Computador>, IComputadorRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public ComputadorRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos da tabela de Computador
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Computador in db.Computador
                              where _Computador.fg_ativo == false
                              select _Computador.id_computador);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todas os Computador que não estão excluidas
        /// </summary>
        /// <param name="id_computador"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Computador> GetList(Guid id_computador, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Computador in db.Computador
                              where ((id_computador == Guid.Empty) || (_Computador.id_computador == id_computador))
                              && _Computador.fg_ativo == false
                              orderby _Computador.numero_computador
                              select new
                              {
                                  id_computador = _Computador.id_computador,
                                  id_laboratorio = _Computador.id_laboratorio,
                                  numero_computador = _Computador.numero_computador

                              }).ToList();

                IList<Computador> computador = new List<Computador>();

                foreach (var item in lQuery)
                {
                    Computador _Computador = new Computador();

                    _Computador.id_computador = item.id_computador;
                    _Computador.id_laboratorio = item.id_laboratorio;
                    _Computador.numero_computador = item.numero_computador;

                    computador.Add(_Computador);
                }

                return computador.OrderBy(m => m.numero_computador);
            }
        }
    }
}